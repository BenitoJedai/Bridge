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
        public const string ROOT = "Bridge";
        public const string DELEGATE_BIND = "fn.bind";
        public const string DELEGATE_BIND_SCOPE = "fn.bindScope";
        public const string DELEGATE_COMBINE = "fn.combine";
        public const string DELEGATE_REMOVE = "fn.remove";
        public const string CAST = "cast";
        public const string AS = "as";
        public const string IS = "is";
        public const string ENUMERATOR = "getEnumerator";
        public const string HAS_NEXT = "hasNext";
        public const string NEXT = "next";
        public const string APPLY_OBJECT = "apply";
        public const string MERGE_OBJECT = "merge";
        public const string FIX_ARGUMENT_NAME = "__autofix__";


        private static List<string> reservedStaticNames = new List<string> { "Name", "Arguments", "Caller", "Length", "Prototype" };        

        public Validator Validator
        {
            get;
            private set;
        }

        public List<TypeInfo> Types 
        { 
            get; 
            set; 
        }

        public bool IsAssignment
        {
            get;
            set;
        }

        public AssignmentOperatorType AssignmentType
        {
            get;
            set;
        }
        
        public Dictionary<string, AstType> Locals 
        { 
            get; 
            set; 
        }

        public Stack<Dictionary<string, AstType>> LocalsStack 
        { 
            get; 
            set; 
        }

        public int Level 
        { 
            get; 
            set; 
        }

        public bool IsNewLine 
        { 
            get; 
            set; 
        }

        public bool EnableSemicolon 
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

        public int IteratorCount 
        { 
            get; 
            set; 
        }

        public int ThisRefCounter 
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
            set; 
        }

        public Stack<Tuple<string, StringBuilder, bool>> Writers
        {
            get;
            set;
        }

        public bool Comma
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

        public bool IsAsync 
        { 
            get; 
            set; 
        }

        public List<string> AsyncVariables
        {
            get;
            set;
        }

        public AsyncBlock AsyncBlock
        {
            get;
            set;
        }

        public bool ReplaceAwaiterByVar
        {
            get;
            set;
        }

        public bool AsyncExpressionHandling
        {
            get;
            set;
        }

        public AstNode IgnoreBlock 
        { 
            get; 
            set; 
        }

        public WriterInfo LastSavedWriter
        {
            get;
            set;
        }

        public List<JumpInfo> JumpStatements
        {
            get;
            set;
        }

        public SwitchStatement AsyncSwitch
        {
            get;
            set;
        }
    }
}
