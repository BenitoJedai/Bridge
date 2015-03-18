using Bridge.Contract;
using System;
using System.Collections.Generic;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Linq;
using System.Text;

namespace Bridge.Contract
{
    public class OverloadsCollection
    {
        public static OverloadsCollection Create(IEmitter emitter, MethodDefinition methodDef)
        {
            string key = methodDef.ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, methodDef);
        }

        public static OverloadsCollection Create(IEmitter emitter, PropertyDefinition propDef, bool isSetter)
        {
            string key = propDef.ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, propDef, isSetter);
        }

        public static OverloadsCollection Create(IEmitter emitter, FieldDefinition fieldDef)
        {
            string key = fieldDef.ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, fieldDef);
        }

        public static OverloadsCollection Create(IEmitter emitter, EventDefinition eventDef)
        {
            string key = eventDef.ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, eventDef);
        }

        public static OverloadsCollection Create(IEmitter emitter, FieldDeclaration fieldDeclaration)
        {
            string key = fieldDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, fieldDeclaration);
        }

        public static OverloadsCollection Create(IEmitter emitter, EventDeclaration eventDeclaration)
        {
            string key = eventDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, eventDeclaration);
        }

        public static OverloadsCollection Create(IEmitter emitter, MethodDeclaration methodDeclaration)
        {
            string key = methodDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, methodDeclaration);
        }

        public static OverloadsCollection Create(IEmitter emitter, ConstructorDeclaration constructorDeclaration)
        {
            string key = constructorDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, constructorDeclaration);
        }

        public static OverloadsCollection Create(IEmitter emitter, PropertyDeclaration propDeclaration, bool isSetter)
        {
            string key = propDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, propDeclaration, isSetter);
        }

        public static OverloadsCollection Create(IEmitter emitter, OperatorDeclaration operatorDeclaration)
        {
            string key = operatorDeclaration.GetHashCode().ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, operatorDeclaration);
        }

        public static OverloadsCollection Create(IEmitter emitter, IMember member, bool isSetter = false)
        {
            string key = member.ToString();
            if (emitter.OverloadsCache.ContainsKey(key))
            {
                return emitter.OverloadsCache[key];
            }

            return new OverloadsCollection(emitter, member, isSetter);
        }

        public IEmitter Emitter
        {
            get;
            private set;
        }

        public TypeDefinition TypeDefinition
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string JsName
        {
            get;
            private set;
        }

        public string ParametersCount
        {
            get;
            private set;
        }

        public bool Static
        {
            get;
            private set;
        }

        public bool Inherit
        {
            get;
            private set;
        }

        public bool Constructor
        {
            get;
            private set;
        }

        public IMemberDefinition MemberDefinition
        {
            get;
            private set;
        }

        public bool IsProperty
        {
            get;
            private set;
        }

        public bool IsSetter
        {
            get;
            private set;
        }

        public IMember Member 
        { 
            get; 
            private set; 
        }

        private OverloadsCollection(IEmitter emitter, MethodDefinition methodDef)
        {
            this.Emitter = emitter;
            this.TypeDefinition = methodDef.DeclaringType;
            this.Name = methodDef.Name;
            this.JsName = this.Emitter.GetDefinitionName(methodDef);
            this.Inherit = !methodDef.IsStatic && !methodDef.IsConstructor;
            this.Static = methodDef.IsStatic;
            this.InitMembers();
            this.MemberDefinition = methodDef;
            this.Emitter.OverloadsCache[methodDef.ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, PropertyDefinition propDef, bool isSetter)
        {
            this.Emitter = emitter;
            this.TypeDefinition = propDef.DeclaringType;
            this.Name = propDef.Name;
            this.JsName = Helpers.GetPropertyRef(propDef, emitter, isSetter, true);
            var accessor = propDef.GetMethod ?? propDef.SetMethod;
            this.Inherit = accessor.IsStatic;
            this.Static = accessor.IsStatic;
            this.IsProperty = !Helpers.IsFieldProperty(propDef, emitter);
            this.IsSetter = isSetter;
            this.InitMembers();
            this.MemberDefinition = propDef;
            this.Emitter.OverloadsCache[propDef.ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, FieldDefinition fieldDef)
        {
            this.Emitter = emitter;
            this.TypeDefinition = fieldDef.DeclaringType;
            this.Name = fieldDef.Name;
            this.JsName = this.Emitter.GetDefinitionName(fieldDef);
            this.Inherit = !fieldDef.IsStatic;
            this.Static = fieldDef.IsStatic;
            this.InitMembers();
            this.MemberDefinition = fieldDef;
            this.Emitter.OverloadsCache[fieldDef.ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, EventDefinition eventDef)
        {
            this.Emitter = emitter;
            this.TypeDefinition = eventDef.DeclaringType;
            this.Name = eventDef.Name;
            this.JsName = this.Emitter.GetDefinitionName(eventDef);
            this.Inherit = true;
            this.Static = false;
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(eventDef);
            this.Emitter.OverloadsCache[eventDef.ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, FieldDeclaration fieldDeclaration)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = emitter.GetFieldName(fieldDeclaration);
            this.JsName = this.Emitter.GetEntityName(fieldDeclaration);
            this.Inherit = !fieldDeclaration.HasModifier(Modifiers.Static);
            this.Static = fieldDeclaration.HasModifier(Modifiers.Static);
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(fieldDeclaration);
            this.Emitter.OverloadsCache[fieldDeclaration.GetHashCode().ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, EventDeclaration eventDeclaration)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = emitter.GetEventName(eventDeclaration);
            this.JsName = this.Emitter.GetEntityName(eventDeclaration);
            this.Inherit = !eventDeclaration.HasModifier(Modifiers.Static);
            this.Static = eventDeclaration.HasModifier(Modifiers.Static);
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(eventDeclaration);
            this.Emitter.OverloadsCache[eventDeclaration.GetHashCode().ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, MethodDeclaration methodDeclaration)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = methodDeclaration.Name;
            this.JsName = this.Emitter.GetEntityName(methodDeclaration);
            this.Inherit = !methodDeclaration.HasModifier(Modifiers.Static);
            this.Static = methodDeclaration.HasModifier(Modifiers.Static);
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(methodDeclaration);
            this.Emitter.OverloadsCache[methodDeclaration.GetHashCode().ToString()] = this;
        }   

        private OverloadsCollection(IEmitter emitter, ConstructorDeclaration constructorDeclaration)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = constructorDeclaration.Name;
            this.JsName = this.Emitter.GetEntityName(constructorDeclaration);
            this.Inherit = false;
            this.Constructor = true;
            this.Static = constructorDeclaration.HasModifier(Modifiers.Static);
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(constructorDeclaration);
            this.Emitter.OverloadsCache[constructorDeclaration.GetHashCode().ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, PropertyDeclaration propDeclaration, bool isSetter)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = propDeclaration.Name;
            this.JsName = Helpers.GetPropertyRef(propDeclaration, emitter, isSetter, true);
            this.Inherit = !propDeclaration.HasModifier(Modifiers.Static);
            this.Static = propDeclaration.HasModifier(Modifiers.Static);
            this.IsProperty = !Helpers.IsFieldProperty(propDeclaration, emitter);
            this.IsSetter = isSetter;
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(propDeclaration);
            this.Emitter.OverloadsCache[propDeclaration.GetHashCode().ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, OperatorDeclaration operatorDeclaration)
        {
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition();
            this.Name = operatorDeclaration.Name;
            this.JsName = this.Emitter.GetEntityName(operatorDeclaration);
            this.Inherit = !operatorDeclaration.HasModifier(Modifiers.Static);
            this.Static = operatorDeclaration.HasModifier(Modifiers.Static);
            this.InitMembers();
            this.MemberDefinition = this.FindDefinition(operatorDeclaration);
            this.Emitter.OverloadsCache[operatorDeclaration.GetHashCode().ToString()] = this;
        }        

        private OverloadsCollection(IEmitter emitter, IMember member, bool isSetter = false)
        {
            if (member is IMethod)
            {
                var method = (IMethod)member;
                this.Inherit = !method.IsConstructor && !method.IsStatic;
                this.Static = method.IsStatic;
                this.Constructor = method.IsConstructor;
            }
            else if (member is IEntity)
            {
                var entity = (IEntity)member;
                this.Inherit = !entity.IsStatic;
                this.Static = entity.IsStatic;
            }            
            
            this.Emitter = emitter;
            this.TypeDefinition = emitter.GetTypeDefinition(member.DeclaringType);
            this.Name = member.Name;

            if (member is IProperty)
            {
                this.IsProperty = !Helpers.IsFieldProperty(member, emitter);
                this.JsName = Helpers.GetPropertyRef(member, emitter, isSetter, true);
            }
            else
            {
                this.JsName = this.Emitter.GetEntityName(member);
            }

            this.Member = member;
            this.IsSetter = isSetter;
            this.InitMembers();
            this.MemberDefinition = this.GetMemberDefinition(member);
            this.Emitter.OverloadsCache[member.ToString()] = this;
        }


        public List<MethodDefinition> Methods
        {
            get;
            private set;
        }

        public List<MethodDefinition> AllMethods
        {
            get;
            private set;
        }

        public List<FieldDefinition> Fields
        {
            get;
            private set;
        }

        public List<PropertyDefinition> Properties
        {
            get;
            private set;
        }

        public List<PropertyDefinition> AllProperties
        {
            get;
            private set;
        }

        public List<EventDefinition> Events
        {
            get;
            private set;
        }

        public bool HasOverloads
        {
            get
            {
                return this.Members.Count > 1;
            }
        }

        protected virtual int GetIndex(IMemberDefinition member)
        {
            var typeDef = member.DeclaringType;
            var method = member as MethodDefinition;
            
            while (method != null && method.IsVirtual && method.IsReuseSlot)
            {
                method = Helpers.GetBaseMethod(method, this.Emitter);
            }

            if (method != null)
            {
                return this.Members.IndexOf(method);
            }

            var property = member as PropertyDefinition;
            var accessor = property != null ? property.GetMethod ?? property.SetMethod : null;
            while (property != null && accessor.IsVirtual && accessor.IsReuseSlot)
            {
                var baseType = Helpers.GetBaseType(property.DeclaringType, this.Emitter);
                if (baseType == null)
                {
                    property = null;
                }
                else
                {
                    property = baseType.Properties.FirstOrDefault(p => p.Name == property.Name);
                    accessor = property != null ? property.GetMethod ?? property.SetMethod : null;
                }
            }

            return this.Members.IndexOf(property != null ? property : member);
        }

        private List<IMemberDefinition> members;
        public List<IMemberDefinition> Members
        {
            get
            {
                this.InitMembers();
                return this.members;
            }
        }

        protected virtual void InitMembers()
        {
            if (this.members == null)
            {
                this.Methods = this.GetMethodOverloads(false);
                this.AllMethods = this.GetMethodOverloads(true);
                this.Properties = this.GetPropertyOverloads(false);
                this.AllProperties = this.GetPropertyOverloads(true);
                this.Fields = this.GetFieldOverloads();
                this.Events = this.GetEventOverloads();

                this.members = new List<IMemberDefinition>();
                this.members.AddRange(this.Methods);
                this.members.AddRange(this.Properties);
                this.members.AddRange(this.Fields);
                //this.members.AddRange(this.Events);

                this.SortMembersOverloads();
            }
        }

        protected virtual void SortMembersOverloads()
        {
            this.Members.Sort((m1, m2) =>
            {
                if (m1.DeclaringType != m2.DeclaringType)
                {
                    return Helpers.IsSubclassOf(m1.DeclaringType, m2.DeclaringType, this.Emitter) ? 1 : -1;
                }

                var method1 = m1 as MethodDefinition;
                var method2 = m2 as MethodDefinition;

                if ((method1 != null && method1.IsConstructor) && 
                    (method2 == null || !method2.IsConstructor))
                {
                    return -1;
                }

                if ((method2 != null && method2.IsConstructor) &&
                    (method1 == null || !method1.IsConstructor))
                {
                    return 1;
                }

                if ((method1 != null && method1.IsConstructor) &&
                    (method2 != null && method2.IsConstructor))
                {
                    return string.Compare(this.MemberToString(m1), this.MemberToString(m2));
                }

                var v1 = m1 is FieldDefinition ? 1 : (m1 is EventDefinition ? 2 : (m1 is MethodDefinition ? 3 : 4));
                var v2 = m2 is FieldDefinition ? 1 : (m2 is EventDefinition ? 2 : (m2 is MethodDefinition ? 3 : 4));

                if (v1 != v2)
                {
                    return v1.CompareTo(v2);
                }

                return string.Compare(this.MemberToString(m1), this.MemberToString(m2));
            });
        }

        protected virtual string MemberToString(IMemberDefinition member)
        {
            if (member is MethodDefinition)
            {
                return this.MethodToString((MethodDefinition)member);
            }

            return member.Name;
        }

        protected virtual string MethodToString(MethodDefinition m)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(m.ReturnType.ToString()).Append(" ");
            sb.Append(m.Name).Append(" ");
            sb.Append(m.GenericParameters.Count).Append(" ");

            foreach (var p in m.Parameters)
            {
                sb.Append(p.ParameterType.ToString()).Append(" ");
            }

            return sb.ToString();
        }

        protected virtual List<MethodDefinition> GetMethodOverloads(bool all)
        {
            List<MethodDefinition> list = new List<MethodDefinition>();
            TypeDefinition typeDef = this.TypeDefinition;

            while (typeDef != null)
            {
                var methods = typeDef.Methods.Where(m =>
                {
                    if (this.Emitter.GetDefinitionName(m) == this.JsName && m.IsStatic == this.Static && 
                        ((m.IsConstructor && this.JsName == "$constructor") || m.IsConstructor == this.Constructor))
                    {
                        if (all && m.IsConstructor != this.Constructor)
                        {
                            return false;
                        }

                        if (!all && m.IsVirtual && m.IsReuseSlot)
                        {
                            return false;
                        }

                        return true;
                    }

                    return false;
                });

                list.AddRange(methods);

                if (this.Inherit)
                {
                    var baseTypeDefinition = Helpers.ToTypeDefinition(typeDef.BaseType, this.Emitter);

                    if (baseTypeDefinition != null)
                    {
                        typeDef = baseTypeDefinition;
                    }
                    else
                    {
                        typeDef = null;
                    }
                }
                else
                {
                    typeDef = null;
                }
            }

            return list;
        }

        protected virtual List<PropertyDefinition> GetPropertyOverloads(bool all)
        {
            List<PropertyDefinition> list = new List<PropertyDefinition>();
            TypeDefinition typeDef = this.TypeDefinition;

            while (typeDef != null)
            {
                var properties = typeDef.Properties.Where(p =>
                {
                    var accessor = p.GetMethod ?? p.SetMethod;
                    bool eq = false;
                    if (accessor.IsStatic == this.Static)
                    {
                        if (p.GetMethod != null && (Helpers.GetPropertyRef(p, this.Emitter, false, true) == this.JsName))
                        {
                            eq = true;
                        }
                        else if (p.SetMethod != null && (Helpers.GetPropertyRef(p, this.Emitter, true, true) == this.JsName))
                        {
                            eq = true;
                        }
                    }

                    if (eq)
                    {
                        if (p.GetMethod != null && !all && p.GetMethod.IsVirtual && p.GetMethod.IsReuseSlot)
                        {
                            return false;
                        }

                        return true;
                    }

                    return false;
                });

                list.AddRange(properties);

                if (this.Inherit)
                {
                    var baseTypeDefinition = Helpers.ToTypeDefinition(typeDef.BaseType, this.Emitter);

                    if (baseTypeDefinition != null)
                    {
                        typeDef = baseTypeDefinition;
                    }
                    else
                    {
                        typeDef = null;
                    }
                }
                else
                {
                    typeDef = null;
                }
            }

            return list;
        }

        protected virtual List<FieldDefinition> GetFieldOverloads()
        {
            List<FieldDefinition> list = new List<FieldDefinition>();
            TypeDefinition typeDef = this.TypeDefinition;

            while (typeDef != null)
            {
                var fields = typeDef.Fields.Where(f =>
                {
                    if (this.Emitter.GetDefinitionName(f) == this.JsName && f.IsStatic == this.Static)
                    {
                        return true;
                    }

                    return false;
                });

                list.AddRange(fields);

                if (this.Inherit)
                {
                    var baseTypeDefinition = Helpers.ToTypeDefinition(typeDef.BaseType, this.Emitter);

                    if (baseTypeDefinition != null)
                    {
                        typeDef = baseTypeDefinition;
                    }
                    else
                    {
                        typeDef = null;
                    }
                }
                else
                {
                    typeDef = null;
                }
            }

            return list;
        }

        protected virtual List<EventDefinition> GetEventOverloads()
        {
            List<EventDefinition> list = new List<EventDefinition>();
            TypeDefinition typeDef = this.TypeDefinition;

            while (typeDef != null)
            {
                var events = typeDef.Events.Where(e =>
                {
                    if (this.Emitter.GetDefinitionName(e) == this.JsName)
                    {
                        return true;
                    }

                    return false;
                });

                list.AddRange(events);

                if (this.Inherit)
                {
                    var baseTypeDefinition = Helpers.ToTypeDefinition(typeDef.BaseType, this.Emitter);

                    if (baseTypeDefinition != null)
                    {
                        typeDef = baseTypeDefinition;
                    }
                    else
                    {
                        typeDef = null;
                    }
                }
                else
                {
                    typeDef = null;
                }
            }

            return list;
        }

        private string overloadName;
        public string GetOverloadName()
        {
            if (this.MemberDefinition == null)
            {
                return this.JsName;
            }

            if (this.overloadName == null && this.MemberDefinition != null)
            {
                this.overloadName = this.GetOverloadName(this.MemberDefinition);
            }

            return this.overloadName;
        }

        protected virtual string GetOverloadName(IMemberDefinition definition)
        {
            string name = this.Emitter.GetDefinitionName(definition, !this.IsProperty);
            if (name.StartsWith(".ctor"))
            {
                name = "constructor";
            }

            var attr = this.Emitter.GetAttribute(definition.CustomAttributes, "Bridge.NameAttribute");

            if (attr != null || this.Emitter.Validator.IsIgnoreType(definition.DeclaringType))
            {
                return name;
            }

            if (definition is MethodDefinition && ((MethodDefinition)definition).IsConstructor)
            {
                name = "constructor";
            }

            var index = this.GetIndex(this.MemberDefinition);

            if (index > 0)
            {
                name += "$" + index;

                if (name.StartsWith("$"))
                {
                    name = name.Substring(1);
                }
            }

            return name;
        }

        protected virtual string GetOverloadName(IMember member)
        {
            string name = this.Emitter.GetEntityName(member);
            if (name.StartsWith(".ctor"))
            {
                name = "constructor";
            }

            var typeDef = this.Emitter.GetTypeDefinition(member.DeclaringType);
            var attr = this.Emitter.GetAttribute(member.Attributes, "Bridge.NameAttribute");

            if (attr == null && typeDef != null && !this.Emitter.Validator.IsIgnoreType(typeDef))
            {
                if (this.HasOverloads)
                {
                    IMemberDefinition memberDef = this.GetMemberDefinition(member);
                    var index = this.GetIndex(memberDef);

                    if (index > 0)
                    {
                        name += "$" + index;
                    }
                }
            }            

            return name;
        }

        protected virtual IMemberDefinition GetMemberDefinition(IMember member)
        {
            this.InitMembers();

            if (member is IMethod)
            {
                return this.FindDefinition((IMethod)member);
            }

            if (member is IField)
            {
                return this.FindDefinition((IField)member);
            }

            if (member is IProperty)
            {
                return this.FindDefinition((IProperty)member);
            }

            if (member is IEvent)
            {
                return this.FindDefinition((IEvent)member);
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(MethodDeclaration methodDeclaration)
        {
            var parameters = methodDeclaration.Parameters.ToList();
            return Helpers.FindMethodDefinitionInGroup(this.Emitter, parameters, methodDeclaration.TypeParameters, this.AllMethods, methodDeclaration.ReturnType, this.Emitter.GetTypeDefinition());
        }

        protected virtual IMemberDefinition FindDefinition(ConstructorDeclaration declaration)
        {
            var parameters = declaration.Parameters.ToList();
            return Helpers.FindMethodDefinitionInGroup(this.Emitter, parameters, null, this.AllMethods, declaration.ReturnType, this.Emitter.GetTypeDefinition());
        }

        protected virtual IMemberDefinition FindDefinition(FieldDeclaration declaration)
        {
            var typeDef = this.Emitter.GetTypeDefinition();
            var type = declaration.ReturnType;
            var name = this.Emitter.GetFieldName(declaration);
            var resolveResult = this.Emitter.Resolver.ResolveNode(type, this.Emitter);

            foreach (var field in this.Fields)
            {
                if (field.Name == name &&
                    field.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, resolveResult, type, field.FieldType))
                {
                    return field;
                }
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(PropertyDeclaration declaration)
        {
            var typeDef = this.Emitter.GetTypeDefinition();
            var type = declaration.ReturnType;
            var name = declaration.Name;
            var resolveResult = this.Emitter.Resolver.ResolveNode(type, this.Emitter);

            foreach (var member in this.AllProperties)
            {
                if (member.Name == name &&
                    member.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, resolveResult, type, member.PropertyType))
                {
                    return member;
                }
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(EventDeclaration declaration)
        {
            var typeDef = this.Emitter.GetTypeDefinition();
            var type = declaration.ReturnType;
            var name = this.Emitter.GetEventName(declaration);
            var resolveResult = this.Emitter.Resolver.ResolveNode(type, this.Emitter);

            foreach (var member in this.Fields)
            {
                if (member.Name == name &&
                    member.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, resolveResult, type, member.FieldType))
                {
                    return member;
                }
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(CustomEventDeclaration declaration)
        {
            var typeDef = this.Emitter.GetTypeDefinition();
            var type = declaration.ReturnType;
            var name = declaration.Name;
            var resolveResult = this.Emitter.Resolver.ResolveNode(type, this.Emitter);

            foreach (var member in this.Events)
            {
                if (member.Name == name &&
                    member.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, resolveResult, type, member.EventType))
                {
                    return member;
                }
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(OperatorDeclaration methodDeclaration)
        {
            var parameters = methodDeclaration.Parameters.ToList();
            return Helpers.FindMethodDefinitionInGroup(this.Emitter, parameters, null, this.AllMethods, methodDeclaration.ReturnType, this.Emitter.GetTypeDefinition());
        }

        protected virtual IMemberDefinition GetMemberDefinition(EntityDeclaration declaration)
        {
            this.InitMembers();

            if (declaration is MethodDeclaration)
            {
                return this.FindDefinition((MethodDeclaration)declaration);
            }

            if (declaration is ConstructorDeclaration)
            {
                return this.FindDefinition((ConstructorDeclaration)declaration);
            }

            if (declaration is FieldDeclaration)
            {
                return this.FindDefinition((FieldDeclaration)declaration);
            }

            if (declaration is PropertyDeclaration)
            {
                return this.FindDefinition((PropertyDeclaration)declaration);
            }

            if (declaration is EventDeclaration)
            {
                return this.FindDefinition((EventDeclaration)declaration);
            }

            if (declaration is OperatorDeclaration)
            {
                return this.FindDefinition((OperatorDeclaration)declaration);
            }

            if (declaration is CustomEventDeclaration)
            {
                return this.FindDefinition((CustomEventDeclaration)declaration);
            }

            return null;
        }

        protected virtual MethodDefinition FindDefinition(IMethod m)
        {
            var typeParametersCount = m.TypeParameters.Count;
            var parameters = m.Parameters;
            var typeDef = this.Emitter.GetTypeDefinition(m.DeclaringType);
            var returnType = m.ReturnType;

            foreach (var method in this.AllMethods)
            {
                if (parameters.Count == method.Parameters.Count && method.GenericParameters.Count == typeParametersCount)
                {
                    bool match = true;

                    if (typeDef != null && method.DeclaringType != typeDef)
                    {
                        match = false;
                        continue;
                    }

                    if (returnType != null)
                    {
                        if (!Helpers.TypeIsMatch(this.Emitter, returnType, method.ReturnType, this.Member, -1))
                        {
                            match = false;
                            continue;
                        }
                    }

                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        var type = parameters[i].Type;
                        var typeRef = method.Parameters[i].ParameterType;

                        if (!Helpers.TypeIsMatch(this.Emitter, type, typeRef, this.Member, i))
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        return method;
                    }
                }
            }

            return null;
        }

        protected virtual FieldDefinition FindDefinition(IField f)
        {
            var typeDef = this.Emitter.GetTypeDefinition(f.DeclaringType);
            var type = f.Type;
            var name = f.Name;

            foreach (var field in this.Fields)
            {
                if (field.Name == name && 
                    field.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, type, field.FieldType, this.Member, -1))
                {
                    return field;
                }
            }

            return null;
        }

        protected virtual FieldDefinition FindDefinition(IEvent e)
        {
            var typeDef = this.Emitter.GetTypeDefinition(e.DeclaringType);
            var type = e.ReturnType;
            var name = e.Name;

            foreach (var ev in this.Fields)
            {
                if (ev.Name == name &&
                    ev.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, type, ev.FieldType, this.Member, -1))
                {
                    return ev;
                }
            }

            return null;
        }

        protected virtual PropertyDefinition FindDefinition(IProperty p)
        {
            var typeDef = this.Emitter.GetTypeDefinition(p.DeclaringType);
            var type = p.ReturnType;
            var name = p.Name;

            foreach (var prop in this.AllProperties)
            {
                if (prop.Name == name &&
                    prop.DeclaringType == typeDef &&
                    Helpers.TypeIsMatch(this.Emitter, type, prop.PropertyType, this.Member, -1))
                {
                    return prop;
                }
            }

            return null;
        }

        protected virtual IMemberDefinition FindDefinition(EventDefinition definition)
        {
            var typeDef = this.Emitter.GetTypeDefinition();
            var type = definition.EventType;
            var name = this.Emitter.GetDefinitionName(definition);

            foreach (var field in this.Fields)
            {
                if (field.Name == name &&
                    field.DeclaringType == typeDef &&
                    Helpers.TypeMatch(field.FieldType, type))
                {
                    return field;
                }
            }

            return null;
        }        
    }
}
