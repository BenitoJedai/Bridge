using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Ext.Net.Utilities;

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

        public List<TypeInfo> ObjectLiteralTypes
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
