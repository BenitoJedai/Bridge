using System.Collections.Generic;

namespace Bridge.NET 
{
    public partial class Inspector : Visitor 
    {
        protected string Namespace 
        { 
            get; 
            set; 
        }        

        protected TypeInfo CurrentType 
        { 
            get; 
            set; 
        }

        public HashSet<string> Usings
        {
            get;
            set;
        }

        public List<TypeInfo> Types 
        { 
            get; 
            protected set; 
        }

        public MemberResolver Resolver
        {
            get;
            set;
        }
    }
}
