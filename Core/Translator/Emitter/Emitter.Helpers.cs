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
using Bridge.Contract;

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
            var attr = this.GetAttribute(method.Attributes, Translator.Bridge_ASSEMBLY + ".Script");

            return this.GetScriptArguments(attr);
        }

        public virtual string GetDefinitionName(IMemberDefinition member, bool changeCase = true)
        {
            if (changeCase)
            {                
                changeCase = !this.IsNativeMember(member.FullName) ? this.ChangeCase : true;
                if (member is FieldDefinition && ((FieldDefinition)member).HasConstant && !member.DeclaringType.IsEnum)
                {
                    changeCase = false;
                }
            }
            string attrName = Translator.Bridge_ASSEMBLY + ".NameAttribute";
            var attr = member.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName == attrName);
            bool isIgnore = this.Validator.IsIgnoreType(member.DeclaringType);
            string name = member.Name;
            bool isStatic = false;

            if (member is MethodDefinition)
            {
                var method = (MethodDefinition)member;
                isStatic = method.IsStatic;
                if (method.IsConstructor)
                {
                    name = "constructor";
                }
            }
            else if (member is FieldDefinition)
            {
                isStatic = ((FieldDefinition)member).IsStatic;
            }
            else if (member is PropertyDefinition)
            {
                var prop = (PropertyDefinition)member;
                var accessor = prop.GetMethod ?? prop.SetMethod;
                isStatic = prop.GetMethod != null ? prop.GetMethod.IsStatic : false;
            }
            else if (member is EventDefinition)
            {
                var ev = (EventDefinition)member;
                isStatic = ev.AddMethod != null ? ev.AddMethod.IsStatic : false ;
            }
            if (attr != null)
            {
                var value = attr.ConstructorArguments.First().Value;
                if (value is string)
                {
                    name = value.ToString();
                    if (!isIgnore &&
                        ((isStatic && Emitter.IsReservedStaticName(name)) ||
                        Helpers.IsReservedWord(name)))
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)value;
            }

            name = changeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;
            if (!isIgnore &&
                ((isStatic && Emitter.IsReservedStaticName(name)) ||
                Helpers.IsReservedWord(name)))
            {
                name = "$" + name;
            }

            return name;
        }

        public virtual string GetEntityName(IEntity member, bool cancelChangeCase = false)
        {
            bool changeCase = !this.IsNativeMember(member.FullName) ? this.ChangeCase : true;
            if (member is IMember && this.IsMemberConst((IMember)member))
            {
                changeCase = false;
            }
            var attr = member.Attributes.FirstOrDefault(a => a.AttributeType.FullName == Translator.Bridge_ASSEMBLY + ".NameAttribute");
            bool isIgnore = member.DeclaringTypeDefinition != null && this.Validator.IsIgnoreType(member.DeclaringTypeDefinition);            
            string name = member.Name;
            if (member is IMethod && ((IMethod)member).IsConstructor)
            {
                name = "constructor";
            }

            if (attr != null)
            {
                var value = attr.PositionalArguments.First().ConstantValue;
                if (value is string)
                {
                    name = value.ToString();
                    if (!isIgnore && ((member.IsStatic && Emitter.IsReservedStaticName(name)) || Helpers.IsReservedWord(name)))
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)value;
            }

            name = changeCase && !cancelChangeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if (!isIgnore && ((member.IsStatic && Emitter.IsReservedStaticName(name)) || Helpers.IsReservedWord(name)))
            {
                name = "$" + name;
            }

            return name;
        }

        public virtual string GetEntityName(EntityDeclaration entity, bool cancelChangeCase = false)
        {
            bool changeCase = this.ChangeCase;
            var attr = this.GetAttribute(entity.Attributes, Translator.Bridge_ASSEMBLY + ".Name");

            string name = entity.Name;
            if (entity is FieldDeclaration)
            {
                var fieldDec = (FieldDeclaration)entity;
                if (fieldDec.HasModifier(Modifiers.Const))
                {
                    changeCase = false;
                }
                name = this.GetFieldName(fieldDec);
            }
            else if (entity is EventDeclaration)
            {
                name = this.GetEventName((EventDeclaration)entity);
            }
            else if (entity is ConstructorDeclaration)
            {
                name = "constructor";
            }

            bool isStatic = entity.HasModifier(Modifiers.Static) || entity.HasModifier(Modifiers.Const);            

            if (attr != null)
            {
                var expr = (PrimitiveExpression)attr.Arguments.First();
                if (expr.Value is string)
                {
                    name = expr.Value.ToString();
                    if ((isStatic && Emitter.IsReservedStaticName(name)) || Helpers.IsReservedWord(name))
                    {
                        name = "$" + name;
                    }
                    return name;
                }

                changeCase = (bool)expr.Value;
            }

            name = changeCase && !cancelChangeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if ((isStatic && Emitter.IsReservedStaticName(name)) || Helpers.IsReservedWord(name))
            {
                name = "$" + name;
            }

            return name;
        }

        public virtual string GetFieldName(FieldDeclaration field)
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

        public virtual string GetEventName(EventDeclaration evt)
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
            var attr = this.GetAttribute(provider.CustomAttributes, Translator.Bridge_ASSEMBLY + ".TemplateAttribute");

            return attr != null ? ((string)attr.ConstructorArguments.First().Value) : null;
        }

        public virtual string GetInline(EntityDeclaration method)
        {
            var attr = this.GetAttribute(method.Attributes, Translator.Bridge_ASSEMBLY + ".Template");

            return attr != null ? ((string)((PrimitiveExpression)attr.Arguments.First()).Value) : null;
        }

        public virtual string GetInline(IEntity entity)
        {
            string attrName = Translator.Bridge_ASSEMBLY + ".TemplateAttribute";

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
            string attrName = Translator.Bridge_ASSEMBLY + ".TemplateAttribute";

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
            return fullName.Contains(Translator.Bridge_ASSEMBLY) || fullName.StartsWith("System");
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
                var attr = this.GetAttribute(member.Attributes, Translator.Bridge_ASSEMBLY + ".InlineConstAttribute");

                if (attr != null)
                {
                    return true;
                }
            }

            return false;
        }






    }
}
