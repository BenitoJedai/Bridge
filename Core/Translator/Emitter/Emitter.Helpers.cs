using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using ICSharpCode.NRefactory.Semantics;
using Bridge.Plugin;

namespace Bridge.NET
{
    public partial class Emitter
    {
        protected virtual HashSet<string> CreateNamespaces()
        {
            var result = new HashSet<string>();

            foreach (string typeName in this.TypeDefinitions.Keys)
            {
                int index = typeName.LastIndexOf('.');

                if (index >= 0)
                {
                    this.RegisterNamespace(typeName.Substring(0, index), result);
                }
            }

            return result;
        }

        protected virtual void RegisterNamespace(string ns, ICollection<string> repository)
        {
            if (String.IsNullOrEmpty(ns) || repository.Contains(ns))
            {
                return;
            }

            string[] parts = ns.Split('.');
            StringBuilder builder = new StringBuilder();

            foreach (string part in parts)
            {
                if (builder.Length > 0)
                {
                    builder.Append('.');
                }

                builder.Append(part);
                string item = builder.ToString();

                if (!repository.Contains(item))
                {
                    repository.Add(item);
                }
            }
        }

        public static bool IsReservedStaticName(string name)
        {
            return Emitter.reservedStaticNames.Any(n => String.Equals(name, n, StringComparison.InvariantCultureIgnoreCase));
        }

        public virtual string ToJavaScript(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        protected virtual ICSharpCode.NRefactory.CSharp.Attribute GetAttribute(AstNodeCollection<AttributeSection> attributes, string name)
        {
            string fullName = name + "Attribute";
            foreach (var i in attributes)
            {
                foreach (var j in i.Attributes)
                {
                    if (j.Type.ToString() == name)
                    {
                        return j;
                    }

                    var resolveResult = this.Resolver.ResolveNode(j, this);
                    if (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == fullName)
                    {
                        return j;
                    }
                }
            }

            return null;
        }

        public virtual CustomAttribute GetAttribute(IEnumerable<CustomAttribute> attributes, string name)
        {
            foreach (var attr in attributes)
            {
                if (attr.AttributeType.FullName == name)
                {
                    return attr;
                }
            }

            return null;
        }

        public virtual IAttribute GetAttribute(IEnumerable<IAttribute> attributes, string name)
        {
            foreach (var attr in attributes)
            {
                if (attr.AttributeType.FullName == name)
                {
                    return attr;
                }
            }

            return null;
        }

        protected virtual bool HasDelegateAttribute(MethodDeclaration method)
        {
            return this.GetAttribute(method.Attributes, "Delegate") != null;
        }

        protected virtual Tuple<bool, bool, string> AltGetInlineCode(InvocationExpression node)
        {
            var parts = new List<string>();
            Expression current = node.Target;
            var genericCount = -1;

            while (true)
            {
                MemberReferenceExpression member = current as MemberReferenceExpression;

                if (member != null)
                {
                    parts.Insert(0, member.MemberName);
                    current = member.Target;

                    if (genericCount < 0)
                    {
                        genericCount = member.TypeArguments.Count;
                    }

                    continue;
                }

                IdentifierExpression id = current as IdentifierExpression;

                if (id != null)
                {
                    parts.Insert(0, id.Identifier);

                    if (genericCount < 0)
                    {
                        genericCount = id.TypeArguments.Count;
                    }

                    break;
                }

                TypeReferenceExpression typeRef = current as TypeReferenceExpression;

                if (typeRef != null)
                {
                    parts.Insert(0, Helpers.GetScriptName(typeRef.Type, false));
                    break;
                }

                break;
            }

            if (parts.Count < 1)
            {
                return null;
            }

            if (genericCount < 0)
            {
                genericCount = 0;
            }

            string typeName = parts.Count < 2
                ? this.TypeInfo.GenericFullName
                : this.ResolveType(String.Join(".", parts.ToArray(), 0, parts.Count - 1));

            if (String.IsNullOrEmpty(typeName))
            {
                return null;
            }

            string methodName = parts[parts.Count - 1];

            TypeDefinition type = this.TypeDefinitions[typeName];
            var methods = type.Methods.Where(m => m.Name == methodName);

            foreach (var method in methods)
            {
                if (method.Parameters.Count == node.Arguments.Count
                    && method.GenericParameters.Count == genericCount)
                {
                    bool isInlineMethod = this.Validator.IsInlineMethod(method);
                    string inlineCode = isInlineMethod ? null : this.Validator.GetInlineCode(method);

                    return new Tuple<bool, bool, string>(method.IsStatic, isInlineMethod, inlineCode);
                }
            }

            return null;
        }

        public virtual Tuple<bool, bool, string> GetInlineCode(InvocationExpression node)
        {
            var resolveResult = this.Resolver.ResolveNode(node, this);
            var invocationResolveResult = resolveResult as InvocationResolveResult;

            if (invocationResolveResult == null)
            {
                return this.AltGetInlineCode(node);
            }

            bool isInlineMethod = this.IsInlineMethod(invocationResolveResult.Member);
            var inlineCode = isInlineMethod ? null : this.GetInline(invocationResolveResult.Member);
            var isStatic = invocationResolveResult.Member.IsStatic;

            return new Tuple<bool, bool, string>(isStatic, isInlineMethod, inlineCode);
        }

        public virtual IEnumerable<string> GetScript(EntityDeclaration method)
        {
            var attr = this.GetAttribute(method.Attributes, Translator.CLR_ASSEMBLY + ".Script");

            return this.GetScriptArguments(attr);
        }

        public virtual string GetMethodName(MethodDefinition method)
        {
            bool changeCase = !this.IsNativeMember(method.FullName) ? this.ChangeCase : true;
            string attrName = Translator.CLR_ASSEMBLY + ".NameAttribute";
            var attr = method.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName == attrName);
            bool isReserved = method.IsStatic && Emitter.IsReservedStaticName(method.Name) && !this.Validator.IsIgnoreType(method.DeclaringType);
            string name = method.Name;

            if (attr != null)
            {
                var value = attr.ConstructorArguments.First().Value;
                if (value is string)
                {
                    name = value.ToString();
                    if (isReserved)
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)value;
            }

            name = changeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;
            if (isReserved)
            {
                name = "$" + name;
            }

            return name;
        }

        public virtual string GetEntityName(IEntity member, bool cancelChangeCase = false)
        {
            bool changeCase = !this.IsNativeMember(member.FullName) ? this.ChangeCase : true;
            var attr = member.Attributes.FirstOrDefault(a => a.AttributeType.FullName == Translator.CLR_ASSEMBLY + ".NameAttribute");
            bool isReserved = member.IsStatic && Emitter.IsReservedStaticName(member.Name) && !this.Validator.IsIgnoreType(member.DeclaringTypeDefinition);
            string name = member.Name;

            if (attr != null)
            {
                var value = attr.PositionalArguments.First().ConstantValue;
                if (value is string)
                {
                    name = value.ToString();
                    if (isReserved)
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)value;
            }

            name = changeCase && !cancelChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if (isReserved)
            {
                name = "$" + name;
            }

            return name;
        }

        public virtual string GetEntityName(EntityDeclaration entity, bool cancelChangeCase = false)
        {
            bool changeCase = this.ChangeCase;
            var attr = this.GetAttribute(entity.Attributes, Translator.CLR_ASSEMBLY + ".Name");

            string name = entity.Name;
            if (entity is FieldDeclaration)
            {
                name = this.GetFieldName((FieldDeclaration)entity);
            }
            else if (entity is EventDeclaration)
            {
                name = this.GetEventName((EventDeclaration)entity);
            }

            bool isReserved = entity.HasModifier(Modifiers.Static) && Emitter.IsReservedStaticName(name);


            if (attr != null)
            {
                var expr = (PrimitiveExpression)attr.Arguments.First();
                if (expr.Value is string)
                {
                    name = expr.Value.ToString();
                    if (isReserved)
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)expr.Value;
            }

            name = changeCase && !cancelChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if (isReserved)
            {
                name = "$" + name;
            }

            return name;
        }

        protected virtual string GetFieldName(FieldDeclaration field)
        {
            if (!string.IsNullOrEmpty(field.Name))
            {
                return field.Name;
            }

            if (field.Variables.Count > 0)
            {
                return field.Variables.First().Name;
            }

            return null;
        }

        protected virtual string GetEventName(EventDeclaration evt)
        {
            if (!string.IsNullOrEmpty(evt.Name))
            {
                return evt.Name;
            }

            if (evt.Variables.Count > 0)
            {
                return evt.Variables.First().Name;
            }

            return null;
        }

        public virtual string GetInline(ICustomAttributeProvider provider)
        {
            var attr = this.GetAttribute(provider.CustomAttributes, Translator.CLR_ASSEMBLY + ".TemplateAttribute");

            return attr != null ? ((string)attr.ConstructorArguments.First().Value) : null;
        }

        public virtual string GetInline(EntityDeclaration method)
        {
            var attr = this.GetAttribute(method.Attributes, Translator.CLR_ASSEMBLY + ".Template");

            return attr != null ? ((string)((PrimitiveExpression)attr.Arguments.First()).Value) : null;
        }

        public virtual string GetInline(IEntity entity)
        {
            string attrName = Translator.CLR_ASSEMBLY + ".TemplateAttribute";

            if (entity.SymbolKind == SymbolKind.Property)
            {
                var prop = (IProperty)entity;
                entity = this.IsAssignment ? prop.Setter : prop.Getter;
            }

            if (entity != null)
            {
                var attr = entity.Attributes.FirstOrDefault(a =>
                {

                    return a.AttributeType.FullName == attrName;
                });

                return attr != null ? attr.PositionalArguments[0].ConstantValue.ToString() : null;
            }

            return null;
        }

        protected virtual bool IsInlineMethod(IEntity entity)
        {
            string attrName = Translator.CLR_ASSEMBLY + ".TemplateAttribute";

            if (entity != null)
            {
                var attr = entity.Attributes.FirstOrDefault(a =>
                {

                    return a.AttributeType.FullName == attrName;
                });

                return attr != null && attr.PositionalArguments.Count == 0;
            }

            return false;
        }

        protected virtual IEnumerable<string> GetScriptArguments(ICSharpCode.NRefactory.CSharp.Attribute attr)
        {
            if (attr == null)
            {
                return null;
            }

            var result = new List<string>();

            foreach (var arg in attr.Arguments)
            {
                PrimitiveExpression expr = (PrimitiveExpression)arg;
                result.Add((string)expr.Value);
            }

            return result;
        }

        public virtual bool IsNativeMember(string fullName)
        {
            return fullName.Contains(Translator.CLR_ASSEMBLY) || fullName.StartsWith("System");
        }

        public virtual bool IsMemberConst(IMember member)
        {
            return (member is DefaultResolvedField) && (((DefaultResolvedField)member).IsConst && member.DeclaringType.Kind != TypeKind.Enum);
        }

        public virtual bool IsInlineConst(IMember member)
        {
            bool isConst = (member is DefaultResolvedField) && (((DefaultResolvedField)member).IsConst && member.DeclaringType.Kind != TypeKind.Enum);

            if (isConst)
            {
                var attr = this.GetAttribute(member.Attributes, Translator.CLR_ASSEMBLY + ".InlineConstAttribute");

                if (attr != null)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual string GetMemberOverloadName(IParameterizedMember member)
        {
            var method = member as IMethod;
            var typeDef = this.GetTypeDefinition(member.DeclaringType);           

            if (typeDef != null && !this.Validator.IsIgnoreType(typeDef))
            {
                var methods = Helpers.GetMethodOverloads(typeDef, this, member.Name, 0, !method.IsConstructor);
                Helpers.SortMethodOverloads(methods, this);

                if (methods.Count > 1)
                {
                    MethodDefinition methodDef = Helpers.FindMethodDefinitionInGroup(this, method.Parameters, method.TypeArguments, methods, method.ReturnType);
                    return Helpers.GetOverloadName(this, methodDef, methods);
                }
            }            

            string name = this.GetEntityName(member);
            if (name.StartsWith(".ctor"))
            {
                name = "$ctor";
            }

            return name;
        }

        public int GetMethodIndex(IEnumerable<IMethod> methods, IMethod method)
        {
            return methods.ToList().IndexOf(method);
        }
    }
}
