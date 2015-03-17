using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Object.Net.Utilities;
using System.Linq;
using ICSharpCode.NRefactory.Semantics;
using Bridge.Contract;

namespace Bridge.NET
{
    public partial class Inspector : Visitor
    {
        public override void VisitSyntaxTree(SyntaxTree node)
        {
            node.AcceptChildren(this);
        }

        public override void VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            if (!this.Usings.Contains(usingDeclaration.Namespace))
            {
                this.Usings.Add(usingDeclaration.Namespace);
            }
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            if (!String.IsNullOrEmpty(this.Namespace))
            {
                throw (Exception)this.CreateException(namespaceDeclaration, "Nested namespaces are not supported");
            }

            var prevNamespace = this.Namespace;
            var prevUsings = this.Usings;

            this.Namespace = namespaceDeclaration.Name;
            this.Usings = new HashSet<string>(prevUsings);

            namespaceDeclaration.AcceptChildren(this);

            this.Namespace = prevNamespace;
            this.Usings = prevUsings;
        }

        public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {            
            if (this.CurrentType != null)
            {
                this.NestedTypes = this.NestedTypes ?? new List<Tuple<TypeDeclaration, ITypeInfo>>();
                this.NestedTypes.Add(new Tuple<TypeDeclaration, ITypeInfo>(typeDeclaration, this.CurrentType));
                return;
            }

            if (this.HasIgnore(typeDeclaration))
            {
                return;
            }

            var fullName = this.Namespace + "." + Helpers.GetScriptName(typeDeclaration, false);
            var partialType = this.Types.FirstOrDefault(t => t.FullName == fullName);
            var add = true;

            if (partialType == null)
            {
                this.CurrentType = new TypeInfo()
                {
                    TypeDeclaration = typeDeclaration,
                    ParentType = this.ParentType,
                    Name = Helpers.GetScriptName(typeDeclaration, false),
                    GenericName = Helpers.GetScriptName(typeDeclaration, true),
                    ClassType = typeDeclaration.ClassType,
                    Namespace = this.Namespace,
                    Usings = new HashSet<string>(Usings),
                    IsEnum = typeDeclaration.ClassType == ClassType.Enum,
                    IsStatic = typeDeclaration.ClassType == ClassType.Enum || typeDeclaration.HasModifier(Modifiers.Static)
                };
            }
            else
            {
                this.CurrentType = partialType;
                add = false;
            }            

            if (typeDeclaration.ClassType != ClassType.Interface)
            {
                typeDeclaration.AcceptChildren(this);
            }

            if (add)
            {
                this.Types.Add(this.CurrentType);
            }

            this.CurrentType = null;

            while (this.NestedTypes != null && this.NestedTypes.Count > 0)
            {
                var types = this.NestedTypes;
                this.NestedTypes = null;
                foreach (var nestedType in types)
                {
                    this.ParentType = nestedType.Item2;
                    this.VisitTypeDeclaration(nestedType.Item1);
                    this.ParentType = null;
                }
            }
        }

        public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            bool isStatic = this.CurrentType.ClassType == ClassType.Enum
                || fieldDeclaration.HasModifier(Modifiers.Static) 
                || fieldDeclaration.HasModifier(Modifiers.Const);

            foreach (var item in fieldDeclaration.Variables)
            {
                Expression initializer = item.Initializer;

                if (initializer.IsNull)
                {
                    if (this.CurrentType.ClassType == ClassType.Enum)
                    {
                        throw (Exception)this.CreateException(fieldDeclaration, "Enum items must be explicitly numbered");
                    }

                    initializer = this.GetDefaultFieldInitializer(fieldDeclaration.ReturnType);
                }

                this.CurrentType.FieldsDeclarations.Add(item.Name, fieldDeclaration);

                if (isStatic)
                {
                    if (fieldDeclaration.HasModifier(Modifiers.Const))
                    {
                        this.CurrentType.Consts.Add(item.Name, initializer);
                    }
                    else
                    {
                        this.CurrentType.StaticFields.Add(item.Name, initializer);
                    }
                }
                else
                {
                    this.CurrentType.InstanceFields.Add(item.Name, initializer);
                }
            }
        }

        public override void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            bool isStatic = constructorDeclaration.HasModifier(Modifiers.Static);

            this.FixMethodParameters(constructorDeclaration.Parameters, constructorDeclaration.Body);

            if (isStatic)
            {
                this.CurrentType.StaticCtor = constructorDeclaration;
            }
            else
            {
                this.CurrentType.Ctors.Add(constructorDeclaration);
            }
        }

        
        public override void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            if (this.HasInline(operatorDeclaration))
            {
                return;
            }

            this.FixMethodParameters(operatorDeclaration.Parameters, operatorDeclaration.Body);

            bool isStatic = operatorDeclaration.HasModifier(Modifiers.Static);

            Dictionary<OperatorType, List<OperatorDeclaration>> dict = this.CurrentType.Operators;

            var key = operatorDeclaration.OperatorType;
            if (dict.ContainsKey(key))
            {
                dict[key].Add(operatorDeclaration);
            }
            else
            {
                dict.Add(key, new List<OperatorDeclaration>(new[] { operatorDeclaration }));
            }
        }

        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {           
            if (methodDeclaration.HasModifier(Modifiers.Abstract) || this.HasInline(methodDeclaration))
            {
                return;
            }

            this.FixMethodParameters(methodDeclaration.Parameters, methodDeclaration.Body);
            
            bool isStatic = methodDeclaration.HasModifier(Modifiers.Static);

            Dictionary<string, List<MethodDeclaration>> dict = isStatic
                ? CurrentType.StaticMethods
                : CurrentType.InstanceMethods;

            var key = Helpers.GetScriptName(methodDeclaration, false);

            if (dict.ContainsKey(key))
            {                
                dict[key].Add(methodDeclaration);
            }
            else 
            {
                dict.Add(key, new List<MethodDeclaration>(new []{methodDeclaration}));
            }
        }

        public override void VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            if (customEventDeclaration.HasModifier(Modifiers.Abstract))
            {
                return;
            }

            bool isStatic = customEventDeclaration.HasModifier(Modifiers.Static);

            IDictionary<string, EntityDeclaration> dict = isStatic
                ? CurrentType.StaticProperties
                : CurrentType.InstanceProperties;

            var key = customEventDeclaration.Name;

            dict.Add(key, customEventDeclaration);

            if (!customEventDeclaration.AddAccessor.IsNull
                && !this.HasInline(customEventDeclaration.AddAccessor)
                && customEventDeclaration.AddAccessor.Body.IsNull
                && !this.HasScript(customEventDeclaration.AddAccessor))
            {
                Expression initializer = this.GetDefaultFieldInitializer(customEventDeclaration.ReturnType);

                if (isStatic)
                {
                    this.CurrentType.StaticFields.Add(customEventDeclaration.Name.ToLowerCamelCase(), initializer);
                }
                else
                {
                    this.CurrentType.InstanceFields.Add(customEventDeclaration.Name.ToLowerCamelCase(), initializer);
                }
            }
        }

        public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            if (propertyDeclaration.HasModifier(Modifiers.Abstract))
            {
                return;
            }

            bool isStatic = propertyDeclaration.HasModifier(Modifiers.Static);

            IDictionary<string, EntityDeclaration> dict = isStatic
                ? CurrentType.StaticProperties
                : CurrentType.InstanceProperties;

            var key = propertyDeclaration.Name;

            dict.Add(key, propertyDeclaration);

            if (!propertyDeclaration.Getter.IsNull
                && !this.HasInline(propertyDeclaration.Getter)
                && propertyDeclaration.Getter.Body.IsNull
                && !this.HasScript(propertyDeclaration.Getter))
            {
                Expression initializer = this.GetDefaultFieldInitializer(propertyDeclaration.ReturnType);

                if (isStatic)
                {
                    this.CurrentType.StaticFields.Add(propertyDeclaration.Name.ToLowerCamelCase(), initializer);
                }
                else
                {
                    this.CurrentType.InstanceFields.Add(propertyDeclaration.Name.ToLowerCamelCase(), initializer);
                }

                if (!this.AssemblyInfo.AutoPropertyToField)
                {
                this.CurrentType.AutoProperties.Add(propertyDeclaration.Name.ToLowerCamelCase());
                }
            }
        }

        public override void VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            Expression initializer = enumMemberDeclaration.Initializer;
            if (enumMemberDeclaration.Initializer.IsNull)
            {
                initializer = new PrimitiveExpression(this.CurrentType.LastEnumValue);
            }

            this.CurrentType.StaticFields.Add(enumMemberDeclaration.Name.ToLowerCamelCase(), initializer);
        }

        public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            if (eventDeclaration.HasModifier(Modifiers.Static))
            {
                this.CurrentType.StaticEvents.Add(eventDeclaration);
            }
            else
            {
                this.CurrentType.Events.Add(eventDeclaration);
            }            
        }

        public override void VisitAttributeSection(AttributeSection attributeSection)
        {
            if (attributeSection.AttributeTarget != "assembly")
            {
                return;
            }
            
            foreach (var attr in attributeSection.Attributes)
            {
                var name = attr.Type.ToString();
                var resolveResult = this.Resolver.ResolveNode(attr, null);

                var handled = //this.ReadAspect(attr, name, resolveResult, AttributeTargets.Assembly, null) ||
                              this.ReadModuleInfo(attr, name, resolveResult) ||
                              this.ReadFileNameInfo(attr, name, resolveResult) ||
                              this.ReadOutputPathInfo(attr, name, resolveResult) ||
                              this.ReadFileHierarchyInfo(attr, name, resolveResult) ||
                              this.ReadModuleDependency(attr, name, resolveResult);
            }
        }

        protected virtual bool ReadModuleInfo(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult)
        {
            if ((name == (Translator.Bridge_ASSEMBLY + ".Module")) ||
                (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (Translator.Bridge_ASSEMBLY + ".ModuleAttribute")))
            {
                if (attr.Arguments.Count > 0)
                {
                    object nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 0);
                    this.AssemblyInfo.Module = nameObj != null ? nameObj.ToString() : "";
                }
                else
                {
                    this.AssemblyInfo.Module = "";
                }

                return true;
            }

            return false;
        }

        protected virtual bool ReadFileNameInfo(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult)
        {
            if ((name == (Translator.Bridge_ASSEMBLY + ".FileName")) ||
                (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (Translator.Bridge_ASSEMBLY + ".FileNameAttribute")))
            {
                if (attr.Arguments.Count > 0)
                {
                    object nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 0);

                    if (nameObj is string)
                    {
                        this.AssemblyInfo.FileName = nameObj.ToString();
                    }                        
                }

                return true;
            }

            return false;
        }

        protected virtual bool ReadOutputPathInfo(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult)
        {
            if ((name == (Translator.Bridge_ASSEMBLY + ".Output")) ||
                (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (Translator.Bridge_ASSEMBLY + ".OutputPathAttribute")))
            {
                if (attr.Arguments.Count > 0)
                {
                    object nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 0);

                    if (nameObj is string)
                    {
                        this.AssemblyInfo.Output = nameObj.ToString();
                    }
                }

                return true;
            }

            return false;
        }

        protected virtual bool ReadFileHierarchyInfo(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult)
        {
            if ((name == (Translator.Bridge_ASSEMBLY + ".FilesHierarchy")) ||
                (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (Translator.Bridge_ASSEMBLY + ".FilesHierarchyAttribute")))
            {
                if (attr.Arguments.Count > 0)
                {
                    object nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 0);

                    if (nameObj != null)
                    {
                        this.AssemblyInfo.OutputBy = (OutputBy)Enum.ToObject(typeof(OutputBy), nameObj);
                    }

                    if (attr.Arguments.Count > 1)
                    {
                        nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 1);

                        if (nameObj is int)
                        {
                            this.AssemblyInfo.StartIndexInName = (int)nameObj;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        protected virtual bool ReadModuleDependency(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult)
        {
            if ((name == (Translator.Bridge_ASSEMBLY + ".ModuleDependency")) ||
                (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (Translator.Bridge_ASSEMBLY + ".ModuleDependencyAttribute")))
            {
                if (attr.Arguments.Count > 0)
                {
                    ModuleDependency dependency = new ModuleDependency();
                    object nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 0);

                    if (nameObj is string)
                    {
                        dependency.DependencyName = nameObj.ToString();
                    }

                    nameObj = this.GetAttributeArgumentValue(attr, resolveResult, 1);

                    if (nameObj is string)
                    {
                        dependency.VariableName = nameObj.ToString();
                    }

                    this.AssemblyInfo.Dependencies.Add(dependency);
                }

                return true;
            }

            return false;
        }

        protected virtual object GetAttributeArgumentValue(ICSharpCode.NRefactory.CSharp.Attribute attr, ResolveResult resolveResult, int index)
        {
            object nameObj = null;

            if (!(resolveResult is ErrorResolveResult) && (resolveResult is InvocationResolveResult))
            {
                nameObj = ((InvocationResolveResult)resolveResult).Arguments.Skip(index).Take(1).First().ConstantValue;
            }
            else
            {
                var arg = attr.Arguments.Skip(index).Take(1).First();

                if (arg is PrimitiveExpression)
                {
                    var primitive = (PrimitiveExpression)arg;
                    nameObj = primitive.Value;
                }
            }
            return nameObj;
        }
    }
}
