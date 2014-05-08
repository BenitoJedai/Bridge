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
    public static class MemberResolver
    {
        private static string lastFileName;
        private static string editorText;
        private static ReadOnlyDocument document;
        private static IList<string> sourceFiles;
        private static ICompilation compilation;
        private static CSharpAstResolver resolver;

        private static IProjectContent project;
        public static void InitResolver(IList<string> sourceFiles, IEnumerable<IAssemblyReference> assemblies)
        {            
            MemberResolver.project = null;
            MemberResolver.lastFileName = null;
            MemberResolver.sourceFiles = sourceFiles;

            MemberResolver.project = new CSharpProjectContent();
            MemberResolver.project = MemberResolver.project.AddAssemblyReferences(assemblies);
            MemberResolver.AddOrUpdateFiles();
        }

        private static void AddOrUpdateFiles()
        {
            var unresolvedFiles = new IUnresolvedFile[MemberResolver.sourceFiles.Count];
            Parallel.For(0, unresolvedFiles.Length, i =>
            {
                var file = sourceFiles[i];
                var syntaxTree = new CSharpParser().Parse(System.IO.File.ReadAllText(file), file);
                syntaxTree.Freeze();
                unresolvedFiles[i] = syntaxTree.ToTypeSystem();
            });
            MemberResolver.project = MemberResolver.project.AddOrUpdateFiles(unresolvedFiles);
            MemberResolver.compilation = MemberResolver.project.CreateCompilation();
        }

        private static void InitDocument(SyntaxTree syntaxTree)
        {
            if (MemberResolver.lastFileName != syntaxTree.FileName)
            {
                MemberResolver.lastFileName = syntaxTree.FileName;
                MemberResolver.editorText = System.IO.File.ReadAllText(MemberResolver.lastFileName);
                MemberResolver.document = new ReadOnlyDocument(MemberResolver.editorText);
                var unresolvedFile = syntaxTree.ToTypeSystem();
                MemberResolver.resolver = new CSharpAstResolver(compilation, syntaxTree, unresolvedFile);
            }
        }

        public static ResolveResult ResolveNode(AstNode node, ILog log)
        {
            var syntaxTree = node.GetParent<SyntaxTree>();
            MemberResolver.InitDocument(syntaxTree);
            syntaxTree.Freeze();
            var result = MemberResolver.resolver.Resolve(node);

            if (result is MethodGroupResolveResult && node.Parent != null)
            {
                result = MemberResolver.ResolveNode(node.Parent, log);
            }

            if ((result == null || result.IsError) && log != null)
            {
                log.LogWarning(string.Format("Node resolving is failed {0}: {1}", node.StartLocation, node.GetText()));
            }

            return result;
        }
    }
}