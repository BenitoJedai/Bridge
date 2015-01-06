using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class AspectInfo
    {
        public AspectInfo()
        {
            this.Properties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Properties
        {
            get;
            set;
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
    }

    public class AspectCollection : Dictionary<string, Dictionary<AttributeTargets, List<AspectInfo>>>
    {
    }
}
