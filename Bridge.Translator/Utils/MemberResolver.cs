using System.Collections.Generic;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory;


namespace Bridge.NET
{
    public class MemberResolver
    {
        private string lastFileName;
        private IList<string> sourceFiles;
        private ICompilation compilation;
        private CSharpAstResolver resolver;
        private IProjectContent project;

        public bool CanFreeze
        {
            get;
            set;
        }


        public MemberResolver(IList<string> sourceFiles, IEnumerable<IAssemblyReference> assemblies)
        {            
            this.project = null;
            this.lastFileName = null;
            this.sourceFiles = sourceFiles;

            this.project = new CSharpProjectContent();
            this.project = this.project.AddAssemblyReferences(assemblies);
            this.AddOrUpdateFiles();
        }

        private void AddOrUpdateFiles()
        {
            var unresolvedFiles = new IUnresolvedFile[this.sourceFiles.Count];
            Parallel.For(0, unresolvedFiles.Length, i =>
            {
                var file = this.sourceFiles[i];
                var syntaxTree = new CSharpParser().Parse(System.IO.File.ReadAllText(file), file);
                if (this.CanFreeze)
                {
                    syntaxTree.Freeze();
                }
                unresolvedFiles[i] = syntaxTree.ToTypeSystem();
            });
            this.project = this.project.AddOrUpdateFiles(unresolvedFiles);
            this.compilation = this.project.CreateCompilation();
        }

        private void InitResolver(SyntaxTree syntaxTree)
        {
            if (this.lastFileName != syntaxTree.FileName)
            {
                this.lastFileName = syntaxTree.FileName;                
                var unresolvedFile = syntaxTree.ToTypeSystem();
                this.resolver = new CSharpAstResolver(compilation, syntaxTree, unresolvedFile);
            }
        }

        public ResolveResult ResolveNode(AstNode node, ILog log)
        {
            var syntaxTree = node.GetParent<SyntaxTree>();
            this.InitResolver(syntaxTree);
            if (this.CanFreeze)
            {
                syntaxTree.Freeze();
            }
            var result = this.resolver.Resolve(node);

            if (result is MethodGroupResolveResult && node.Parent != null)
            {
                return this.ResolveNode(node.Parent, log);
            }

            if ((result == null || result.IsError) && log != null)
            {
                log.LogWarning(string.Format("Node resolving is failed {0}: {1}", node.StartLocation, node.GetText()));
            }

            return result;
        }
    }
}