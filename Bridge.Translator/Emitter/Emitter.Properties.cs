using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public partial class Emitter : Visitor
    {
        const string ROOT = "Bridge";
        const string DELEGATE_BIND = "fn.bind";
        const string DELEGATE_BIND_SCOPE = "fn.bindScope";
        const string DELEGATE_COMBINE = "fn.combine";
        const string DELEGATE_REMOVE = "fn.remove";
        const string CAST = "cast";
        const string AS = "as";
        const string IS = "is";
        const string ENUMERATOR = "getEnumerator";
        const string HAS_NEXT = "hasNext";
        const string NEXT = "next";
        const string APPLY_OBJECT = "apply";
        public const string FIX_ARGUMENT_NAME = "__autofix__";


        private static List<string> reservedStaticNames = new List<string> { "Name", "Arguments", "Caller", "Length", "Prototype" };

        public Action<string, string> Log
        {
            get;
            set;
        }

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
                
                this.list = Emitter.ToAssemblyReferences(this.References);

                return this.list;
            }
        }

        internal static List<IAssemblyReference> ToAssemblyReferences(IEnumerable<AssemblyDefinition> references)
        {
            var list = new List<IAssemblyReference>();

            if (references == null)
            {
                return list;
            }

            foreach (var reference in references)
            {
                var loader = new CecilLoader();
                loader.IncludeInternalMembers = true;
                list.Add(loader.LoadAssembly(reference));
            }

            return list;
        }

        public MemberResolver Resolver
        {
            get;
            set;
        }

        public AssemblyInfo AssemblyInfo
        {
            get;
            set;
        }

        public Dictionary<string, TypeInfo> TypeInfoDefinitions
        {
            get;
            set;
        }

        public List<ModuleDependency> CurrentDependencies
        {
            get;
            set;
        }

        public EmitterOutputs Outputs
        {
            get;
            set;
        }

        public bool SkipSemiColon
        {
            get;
            set;
        }

        public IEnumerable<MethodDefinition> MethodsGroup 
        { 
            get; 
            set; 
        }

        public Dictionary<int, StringBuilder> MethodsGroupBuilder
        {
            get;
            set;
        }

        public bool InjectMethodDetectors
        {
            get;
            set;
        }
    }
}
