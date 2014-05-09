using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;

namespace Bridge.NET
{
    public partial class Translator
    {
        protected virtual AssemblyDefinition LoadAssembly(string location, List<AssemblyDefinition> references)
        {
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(location);
            string name;
            string path;
            AssemblyDefinition reference;
            
            foreach (AssemblyNameReference r in assemblyDefinition.MainModule.AssemblyReferences)
            {
                name = r.Name;

                if (r.Name == "mscorlib" || r.Name == "System.Core")
                {
                    continue;
                }

                path = Path.Combine(Path.GetDirectoryName(location), name) + ".dll";
                reference = this.LoadAssembly(path, references);
                
                if (!references.Any(a => a.Name.Name == reference.Name.Name))
                {
                    references.Add(reference);
                }
            }

            return assemblyDefinition;
        }

        protected virtual void ReadTypes(AssemblyDefinition assembly)
        {
            foreach (TypeDefinition type in assembly.MainModule.Types)
            {
                if (type.FullName.Contains("<"))
                {
                    continue;
                }

                this.Validator.CheckType(type);
                this.TypeDefinitions.Add(Helpers.GetTypeMapKey(type), type);
            }
        }

        protected virtual List<AssemblyDefinition> InspectReferences()
        {
            var references = new List<AssemblyDefinition>();            
            var assembly = this.LoadAssembly(this.AssemblyLocation, references);
            this.TypeDefinitions = new Dictionary<string, TypeDefinition>();

            if (assembly.Name.Name != Translator.CLR_ASSEMBLY)
            {
                this.ReadTypes(assembly);
            }

            foreach (var item in references)
            {
                this.ReadTypes(item);
            }

            this.Validator.CheckDuplicateNames(this.TypeDefinitions);

            var prefix = Path.GetDirectoryName(this.Location);

            for (int i = 0; i < this.SourceFiles.Count; i++)
            {
                this.SourceFiles[i] = Path.Combine(prefix, this.SourceFiles[i]);
            }

            return references;
        }

        protected virtual void InspectTypes(MemberResolver resolver)
        {
            
            Inspector inspector = this.CreateInspector();
            inspector.Resolver = resolver;

            for (int i = 0; i < this.SourceFiles.Count; i++)
            {
                inspector.VisitSyntaxTree(this.GetSyntaxTree(this.SourceFiles[i]));
            }

            this.Types = inspector.Types;
        }

        protected virtual Inspector CreateInspector()
        {
            return new Inspector();
        }

        protected virtual SyntaxTree GetSyntaxTree(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                var parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();
                var syntaxTree = parser.Parse(reader, fileName);

                if (parser.HasErrors)
                {
                    Bridge.NET.Exception.Throw("Parsing error in a file {0}: {1}", fileName, parser.Errors.ToString());
                }
                
                return syntaxTree;
            }
        }
    }
}
