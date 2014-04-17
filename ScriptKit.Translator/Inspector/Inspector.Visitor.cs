using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Ext.Net.Utilities;
using System.Linq;

namespace ScriptKit.NET
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
                throw this.CreateException(namespaceDeclaration, "Nested namespaces are not supported");
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
                throw this.CreateException(typeDeclaration, "Nested types are not supported");
            }

            if (typeDeclaration.HasModifier(Modifiers.Partial))
            {
                throw this.CreateException(typeDeclaration, "Partial classes are not supported");
            }

            if (this.HasIgnore(typeDeclaration))
            {
                return;
            }            
            
            this.CurrentType = new TypeInfo()
            {
                Name = Helpers.GetScriptName(typeDeclaration, false),
                GenericName = Helpers.GetScriptName(typeDeclaration, true),
                ClassType = typeDeclaration.ClassType,
                Namespace = this.Namespace,
                Usings = new HashSet<string>(Usings),
                IsEnum = typeDeclaration.ClassType == ClassType.Enum
            };

            CurrentType.IsStatic = typeDeclaration.ClassType == ClassType.Enum || typeDeclaration.HasModifier(Modifiers.Static);

            if (typeDeclaration.ClassType != ClassType.Interface)
            {
                typeDeclaration.AcceptChildren(this);
            }

            this.Types.Add(this.CurrentType);
            this.CurrentType = null;
        }

        public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            bool isStatic = this.CurrentType.ClassType == ClassType.Enum
                || fieldDeclaration.HasModifier(Modifiers.Static)
                || fieldDeclaration.HasModifier(Modifiers.Const);

            foreach (var item in fieldDeclaration.Variables)
            {
                if (isStatic && !this.IsValidStaticInitializer(item.Initializer))
                {
                    //throw CreateException(fieldDeclaration, "Only primitive or array initializers for static fields are supported");
                }

                Expression initializer = item.Initializer;

                if (initializer.IsNull)
                {
                    if (this.CurrentType.ClassType == ClassType.Enum)
                    {
                        throw this.CreateException(fieldDeclaration, "Enum items must be explicitly numbered");
                    }

                    initializer = this.GetDefaultFieldInitializer(fieldDeclaration.ReturnType);
                }

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

            if (this.CurrentType.Ctor != null && !isStatic)
            {
                throw this.CreateException(constructorDeclaration, "The only one instance constructor is allowed");
            }

            if (this.CurrentType.StaticCtor != null && isStatic)
            {
                throw this.CreateException(constructorDeclaration, "The only one static constructor is allowed");
            }

            if (isStatic)
            {
                this.CurrentType.StaticCtor = constructorDeclaration;
            }
            else
            {
                this.CurrentType.Ctor = constructorDeclaration;
            }
        }

        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            if (methodDeclaration.HasModifier(Modifiers.Abstract) || this.HasInline(methodDeclaration))
            {
                return;
            }

            bool isStatic = methodDeclaration.HasModifier(Modifiers.Static);

            IDictionary<string, MethodDeclaration> dict = isStatic
                ? CurrentType.StaticMethods
                : CurrentType.InstanceMethods;

            var key = Helpers.GetScriptName(methodDeclaration, false);

            if (dict.ContainsKey(key))
            {
                throw this.CreateException(methodDeclaration, "Overloads are not allowed");
            }

            dict.Add(key, methodDeclaration);
        }

        public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            if (propertyDeclaration.HasModifier(Modifiers.Abstract))
            {
                return;
            }

            bool isStatic = propertyDeclaration.HasModifier(Modifiers.Static);

            IDictionary<string, PropertyDeclaration> dict = isStatic
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
    }
}
