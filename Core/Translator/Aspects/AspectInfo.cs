using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class AspectInfo
    {
        public AspectInfo(AssemblyInfo info)
        {
            this.Properties = new Dictionary<string, object>();
            this.ConstructorArguments = new List<object>();
            this.AssemblyInfo = info;
        }

        public List<object> ConstructorArguments
        {
            get;
            set;
        }

        public Dictionary<string, object> Properties
        {
            get;
            set;
        }

        private Dictionary<string, object> customProperties;
        public Dictionary<string, object> CustomProperties
        {
            get
            {
                if (this.customProperties != null)
                {
                    return this.customProperties;
                }

                var standardNames = new string[] {
                    "TargetMembersAttributes",
                    "TargetMembers",
                    "TargetTypesAttributes",
                    "TargetTypes",
                    "Exclude",
                    "Multiple",
                    "Replace",
                    "Inheritance",
                    "Priority"
                };

                this.customProperties = new Dictionary<string, object>();

                foreach (var prop in this.Properties)
                {
                    var name = prop.Key;
                    if (!standardNames.Contains(name))
                    {
                        this.customProperties.Add(prop.Key, prop.Value);
                    }
                }

                return this.customProperties;
            }
        }

        public string TargetName
        {
            get;
            set;
        }

        public string AspectType
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        public string MergeFormat
        {
            get;
            set;
        }

        public AttributeTargets Target
        {
            get;
            set;
        }

        public IType Type
        {
            get;
            set;
        }

        public TypeReference TypeReference
        {
            get;
            set;
        }

        public AssemblyInfo AssemblyInfo 
        { 
            get; 
            set; 
        }

        public bool IsMethodAspect(Emitter emitter)
        {
            string name = "Bridge.CLR.MethodAspectAttribute";
            if (this.Type != null)
            {
                return AspectHelpers.IsTypeAttribute(this.Type, name);
            }

            return AspectHelpers.IsTypeAttribute(this.TypeReference, name, emitter);
        }

        public bool IsPropertyAspect(Emitter emitter)
        {
            string name = "Bridge.CLR.PropertyAspectAttribute";
            if (this.Type != null)
            {
                return AspectHelpers.IsTypeAttribute(this.Type, name);
            }

            return AspectHelpers.IsTypeAttribute(this.TypeReference, name, emitter);
        }

        public bool IsTypeAspect(Emitter emitter)
        {
            string name = "Bridge.CLR.TypeAspectAttribute";
            if (this.Type != null)
            {
                return AspectHelpers.IsTypeAttribute(this.Type, name);
            }

            return AspectHelpers.IsTypeAttribute(this.TypeReference, name, emitter);
        }

        public T GetProperty<T>(string key, T defaultValue)
        {
            Dictionary<string, object> typeProperties = this.AssemblyInfo.AspectTypeProperties[this.AspectType];

            if (this.Properties.ContainsKey(key))
            {
                if (typeof(T).IsEnum)
                {
                    return (T)this.Properties[key];
                }

                return (T)Convert.ChangeType(this.Properties[key], typeof(T));
            }

            if (typeProperties.ContainsKey(key))
            {
                if (typeof(T).IsEnum)
                {
                    return (T)typeProperties[key];
                }

                return (T)Convert.ChangeType(typeProperties[key], typeof(T));
            }

            return defaultValue;
        }

        public TranslatorMulticastAttributes TargetMembersAttributes
        {
            get
            {
                return this.GetProperty<TranslatorMulticastAttributes>("TargetMembersAttributes", TranslatorMulticastAttributes.Default);
            }
        }

        public string TargetMembers
        {
            get
            {
                return this.GetProperty("TargetMembers", "");
            }
        }

        public TranslatorMulticastAttributes TargetTypesAttributes
        {
            get
            {
                return this.GetProperty<TranslatorMulticastAttributes>("TargetTypesAttributes", TranslatorMulticastAttributes.Default);
            }
        }

        public string TargetTypes
        {
            get
            {
                return this.GetProperty("TargetTypes", "");
            }
        }

        public bool Exclude
        {
            get
            {
                return this.GetProperty("Exclude", false);
            }
        }

        public bool Multiple
        {
            get
            {
                return this.GetProperty("Multiple", true);
            }
        }

        public bool Replace
        {
            get
            {
                return this.GetProperty("Replace", false);
            }
        }

        public TranslatorMulticastInheritance Inheritance
        {
            get
            {
                return this.GetProperty("Inheritance", TranslatorMulticastInheritance.None);
            }
        }

        public int Priority
        {
            get
            {
                return this.GetProperty("Priority", 0);
            }
        }
    }

    public class AspectCollection : Dictionary<AttributeTargets, List<AspectInfo>>
    {
    }
}
