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

        public AssemblyInfo AssemblyInfo
        {
            get;
            set;
        }

        protected TypeInfo CurrentType 
        { 
            get; 
            set; 
        }

        protected TypeInfo ParentType
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

        public List<Tuple<TypeDeclaration, TypeInfo>> NestedTypes
        {
            get;
            set;
        }
    }
}
