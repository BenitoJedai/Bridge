using System;
using System.Collections.Generic;
using Mono.Cecil;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace Bridge.NET
{
    public class Validator 
    {
        public virtual bool CanIgnoreType(TypeDefinition type) 
        {
            if (type.IsInterface)
            {
                return true;
            }

            if (this.IsIgnoreType(type))
            {
                return true;
            }
            
            if (type.BaseType != null && type.BaseType.FullName == "System.MulticastDelegate")
            {
                return true;
            }

            return false;
        }

        public virtual void CheckType(TypeDefinition type) 
        {
            if (this.CanIgnoreType(type))
            {
                return;
            }

            if (type.IsNested)
            {
                Exception.Throw("Nested types are not supported: {0}", type);
            }

            if (type.HasEvents)
            {
                //Exception.Throw("Events are not supported: {0}", type);
            }

            if (type.IsValueType && !type.IsEnum)
            {
                //Exception.Throw("Struct types not supported, use classes instead: {0}", type);
            }

            this.CheckConstructors(type);
            this.CheckFields(type);
            this.CheckMethods(type);
        }

        public virtual bool IsIgnoreType(ICustomAttributeProvider type) 
        {
            string ignoreAttr = Translator.CLR_ASSEMBLY + ".IgnoreAttribute";
            string objectLiteralAttr = Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute";

            return this.HasAttribute(type.CustomAttributes, ignoreAttr) || this.HasAttribute(type.CustomAttributes, objectLiteralAttr);
        }

        protected internal virtual bool IsIgnoreType(ICSharpCode.NRefactory.TypeSystem.ITypeDefinition typeDefinition)
        {
            string ignoreAttr = Translator.CLR_ASSEMBLY + ".IgnoreAttribute";
            string objectLiteralAttr = Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute";

            return typeDefinition.Attributes.Any(attr => (attr.Constructor.DeclaringType.FullName == ignoreAttr) || (attr.Constructor.DeclaringType.FullName == objectLiteralAttr));
        }

        public virtual int EnumEmitMode(DefaultResolvedTypeDefinition type)
        {
            string enumAttr = Translator.CLR_ASSEMBLY + ".EnumEmitAttribute";
            int result = -1;
            type.Attributes.Any(attr => {
                if (attr.Constructor.DeclaringType.FullName == enumAttr && attr.PositionalArguments.Count > 0)
                {
                    result = (int)attr.PositionalArguments.First().ConstantValue;                    
                    return true;
                }

                return false;
            });

            return result;
        }

        public virtual bool IsValueEnum(DefaultResolvedTypeDefinition type)
        {
            return this.EnumEmitMode(type) == 2;
        }

        public virtual bool IsNameEnum(DefaultResolvedTypeDefinition type)
        {
            return this.EnumEmitMode(type) == 1;
        }

        public virtual bool IsStringNameEnum(DefaultResolvedTypeDefinition type)
        {
            return this.EnumEmitMode(type) == 3;
        }

        public virtual bool HasAttribute(IEnumerable<CustomAttribute> attributes, string name)
        {
            return this.GetAttribute(attributes, name) != null;
        }

        public virtual bool HasAttribute(IEnumerable<ICSharpCode.NRefactory.TypeSystem.IAttribute> attributes, string name)
        {
            return this.GetAttribute(attributes, name) != null;
        }

        public virtual CustomAttribute GetAttribute(IEnumerable<CustomAttribute> attributes, string name)
        {
            CustomAttribute a = attributes
                .FirstOrDefault(attr => attr.AttributeType.FullName == name);
            return a;
        }

        public virtual ICSharpCode.NRefactory.TypeSystem.IAttribute GetAttribute(IEnumerable<ICSharpCode.NRefactory.TypeSystem.IAttribute> attributes, string name)
        {
            ICSharpCode.NRefactory.TypeSystem.IAttribute a = attributes
                .FirstOrDefault(attr => attr.AttributeType.FullName == name);
            return a;
        }

        public virtual string GetAttributeValue(IEnumerable<CustomAttribute> attributes, string name)
        {
            CustomAttribute a = this.GetAttribute(attributes, name);

            if (a != null)
            {
                return (string)a.ConstructorArguments[0].Value;
            }

            return null;
        }

        public virtual string GetInlineCode(MethodDefinition method) 
        {
            return this.GetAttributeValue(method.CustomAttributes, Translator.CLR_ASSEMBLY + ".InlineAttribute");
        }

        public virtual string GetInlineCode(PropertyDefinition property)
        {
            return this.GetAttributeValue(property.CustomAttributes, Translator.CLR_ASSEMBLY + ".InlineAttribute");
        }

        public virtual bool IsObjectLiteral(TypeDefinition type)
        {
            return this.HasAttribute(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute");
        }

        public virtual bool IsObjectLiteral(ICSharpCode.NRefactory.TypeSystem.ITypeDefinition type)
        {
            return this.HasAttribute(type.Attributes, Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute");
        }

        public virtual string GetCustomTypeName(TypeDefinition type) 
        {
            var name = this.GetAttributeValue(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".NameAttribute");

            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            var nsAtrr = this.GetAttribute(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".NamespaceAttribute");
            if (nsAtrr != null && nsAtrr.ConstructorArguments.Count > 0)
            {
                var arg = nsAtrr.ConstructorArguments[0];

                if (arg.Value is bool && !((bool)arg.Value))
                {
                    return type.Name;
                }
                
                if (arg.Value is string) 
                {
                    string ns = arg.Value.ToString();

                    return (!string.IsNullOrWhiteSpace(ns) ? (ns + ".") : "") + type.Name;
                }
            }

            if (this.HasAttribute(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute"))
            {
                return "Object";
            }

            return null;
        }

        public virtual string GetCustomConstructor(TypeDefinition type) 
        {
            string ctor = this.GetAttributeValue(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".ConstructorAttribute");

            if (!string.IsNullOrEmpty(ctor))
            {
                return ctor;
            }

            if (this.HasAttribute(type.CustomAttributes, Translator.CLR_ASSEMBLY + ".ObjectLiteralAttribute"))
            {
                return "{ }";
            }

            return null;
        }

        public virtual void CheckConstructors(TypeDefinition type) 
        {
            bool foundInstance = false;
            bool foundStatic = false;

            if (type.HasMethods) 
            {
                var ctors = type.Methods.Where(method => method.IsConstructor);
                foreach (MethodDefinition ctor in ctors) 
                {
                    if (foundStatic && ctor.IsStatic)
                    {
                        Bridge.NET.Exception.Throw("Type {0} must have the only one static constructor", type);
                    }

                    if (foundInstance && !ctor.IsStatic)
                    {
                        Bridge.NET.Exception.Throw("Type {0} must have the only one instance constructor", type);
                    }

                    this.CheckMethodArguments(ctor);

                    if (ctor.IsStatic)
                    {
                        foundStatic = true;
                    }
                    else
                    {
                        foundInstance = true;
                    }
                }
            }            
        }

        public virtual void CheckFields(TypeDefinition type) 
        {
            //no rules yet
        }

        public virtual void CheckMethods(TypeDefinition type) 
        {
            foreach(MethodDefinition method in type.Methods) 
            {
                if (method.HasCustomAttributes && method.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
                {
                    continue;
                }

                if (!method.IsConstructor && method.Name.Contains("."))
                {
                    Bridge.NET.Exception.Throw("Explicit interface implementations are not supported: {0}", method);
                }

                this.CheckMethodArguments(method);                
            }
        }

        public virtual void CheckMethodArguments(MethodDefinition method) 
        {
            foreach(ParameterDefinition param in method.Parameters) 
            {
                if (param.ParameterType is ByReferenceType)
                {
                    Exception.Throw("Reference parameters are not supported: {0}", method);
                }

                if (param.CustomAttributes.Any(a => a.Constructor.DeclaringType.FullName == "System.ParamArrayAttribute"))
                {
                    Exception.Throw("Param arrays are not supported: {0}", method);
                }
            }
        }

        public virtual void CheckDuplicateNames(IDictionary<string, TypeDefinition> allTypes) 
        {
            var parents = this.GetParentTypes(allTypes);

            foreach(var name in allTypes.Keys) 
            {
                if (parents.Contains(name))
                {
                    continue;
                }

                this.CheckDuplicateNames(allTypes, allTypes[name]);
            }
        }

        public virtual HashSet<string> GetParentTypes(IDictionary<string, TypeDefinition> allTypes) 
        {
            var result = new HashSet<string>();

            foreach(var type in allTypes.Values) 
            {
                if(type.BaseType != null) 
                {
                    string parentName = type.BaseType.FullName;

                    if (!allTypes.ContainsKey(parentName))
                    {
                        Bridge.NET.Exception.Throw("Unknown type {0}", parentName);
                    }

                    if (!result.Contains(parentName))
                    {
                        result.Add(parentName);
                    }
                }
            }
            return result;
        }

        public virtual void CheckDuplicateNames(IDictionary<string, TypeDefinition> allTypes, TypeDefinition leaf) 
        {
            if (this.CanIgnoreType(leaf))
            {
                return;
            }

            var instanceMethods = new Dictionary<string, MethodDefinition>();
            var staticMethods = new Dictionary<string, MethodDefinition>();
            var instanceFields = new Dictionary<string, FieldDefinition>();
            var signatureTable = new Dictionary<string, string>();

            while(true) 
            {
                foreach(FieldDefinition field in leaf.Fields) 
                {
                    if (field.IsStatic)
                    {
                        continue;
                    }
                    
                    if (instanceFields.ContainsKey(field.Name))
                    {
                        Bridge.NET.Exception.Throw("All instance fields within an hierarchy must have unique names: {0} and {1}", field, instanceFields[field.Name]);
                    }

                    if (instanceMethods.ContainsKey(field.Name))
                    {
                        Bridge.NET.Exception.Throw("A field and a method cannot have the same name: {0} and {1}", field, instanceMethods[field.Name]);
                    }

                    instanceFields.Add(field.Name, field);
                }

                foreach(MethodDefinition method in leaf.Methods) 
                {
                    if (method.IsConstructor)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(this.GetInlineCode(method)))
                    {
                        continue;
                    }

                    string key = Helpers.GetScriptName(method, false);

                    if(method.IsStatic) 
                    {                        
                        if (staticMethods.ContainsKey(key))
                        {
                            Bridge.NET.Exception.Throw("All static methods within an hierarchy must have unique names: {0} and {1}", method, staticMethods[key]);
                        }
                        staticMethods.Add(key, method);
                    } 
                    else 
                    {
                        if (instanceFields.ContainsKey(key))
                        {
                            Bridge.NET.Exception.Throw("A field and a method cannot have the same name: {0} and {1}", instanceFields[key], method);
                        }

                        if(instanceMethods.ContainsKey(key)) 
                        {
                            if (!method.IsVirtual || signatureTable[key] != this.GetMethodSignatureKey(method))
                            {
                                Bridge.NET.Exception.Throw("Methods with same name must have same signature and be virtual: {0} and {1}", method, instanceMethods[key]);
                            }
                        } 
                        else 
                        {
                            instanceMethods.Add(key, method);
                            signatureTable.Add(key, GetMethodSignatureKey(method));
                        }
                    }
                }

                if (leaf.BaseType == null)
                {
                    break;
                }

                leaf = allTypes[leaf.BaseType.FullName];
            }
        }

        public virtual string GetMethodSignatureKey(MethodDefinition method) 
        {
            var list = new List<string>(method.Parameters.Count);

            foreach (ParameterDefinition p in method.Parameters)
            {
                list.Add(p.ParameterType.FullName);
            }
            
            return String.Join("$", list.ToArray());
        }        
    }
}
