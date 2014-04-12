using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;

namespace ScriptKit.NET
{
    public partial class Emitter : Visitor
    {
        const string ROOT = "ScriptKit";
        const string BIND = "bind";        
        const string CAST = "cast";
        const string AS = "as";
        const string IS = "is";
        const string ITERATOR = "getIterator";
        const string HAS_NEXT = "hasNext";
        const string NEXT = "next";
        const string APPLY_OBJECT = "apply";


        private static List<string> reservedStaticNames = new List<string> { "Name", "Arguments", "Caller", "Length", "Prototype" };

        public Validator Validator
        {
            get;
            private set;
        }

        protected List<TypeInfo> Types 
        { 
            get; 
            set; 
        }
        
        protected bool IsAssignment
        {
            get;
            set;
        }
        
        protected Dictionary<string, AstType> Locals 
        { 
            get; 
            set; 
        }

        protected Stack<Dictionary<string, AstType>> LocalsStack 
        { 
            get; 
            set; 
        }

        protected int Level 
        { 
            get; 
            set; 
        }

        protected bool IsNewLine 
        { 
            get; 
            set; 
        }

        protected bool EnableSemicolon 
        { 
            get; 
            set; 
        }

        private bool changeCase = true;
        public bool ChangeCase
        {
            get
            {
                return this.changeCase;
            }
            set
            {
                this.changeCase = value;
            }
        }

        protected int IteratorCount 
        { 
            get; 
            set; 
        }

        protected int ThisRefCounter 
        { 
            get; 
            set; 
        }

        public IDictionary<string, TypeDefinition> TypeDefinitions 
        { 
            get; 
            protected set; 
        }

        public TypeInfo TypeInfo 
        { 
            get; 
            set; 
        }

        public StringBuilder Output 
        { 
            get; 
            protected set; 
        }

        protected Stack<Tuple<string, StringBuilder, bool>> Writers
        {
            get;
            set;
        }

        protected bool Comma
        {
            get;
            set;
        }

        HashSet<string> namespaces;

        protected virtual HashSet<string> Namespaces
        {
            get
            {
                if (this.namespaces == null)
                {
                    this.namespaces = this.CreateNamespaces();
                }
                return this.namespaces;
            }
        }

        public virtual IEnumerable<AssemblyDefinition> References
        {
            get;
            set;
        }

        public virtual IList<string> SourceFiles 
        { 
            get; 
            set; 
        }

        private List<IAssemblyReference> list;
        protected virtual IEnumerable<IAssemblyReference> AssemblyReferences
        {
            get
            {
                if (this.list != null)
                {
                    return this.list;
                }
                
                this.list = new List<IAssemblyReference>();

                if (this.References == null)
                {
                    return this.list;
                }

                foreach (var reference in this.References)
                {
                    var loader = new CecilLoader();
                    loader.IncludeInternalMembers = true;
                    this.list.Add(loader.LoadAssembly(reference));
                }

                return this.list;
            }
        }
    }
}
