using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;

namespace ScriptKit.NET
{
    public partial class Translator
    {     
        private bool clrLoaded = false;

        protected virtual AssemblyDefinition LoadAssembly(string location, HashSet<AssemblyDefinition> references)
        {
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(location);

            foreach (AssemblyNameReference r in assemblyDefinition.MainModule.AssemblyReferences)
            {
                string name = r.Name;

                if (r.Name == Translator.CORE_ASSEMBLY)
                {
                    continue;
                }

                if (r.Name == "mscorlib" || r.Name == "System.Core")
                {
                    if (this.clrLoaded)
                    {
                        continue;
                    }

                    this.clrLoaded = true;
                    name = Translator.CLR_ASSEMBLY;
                }

                string path = Path.Combine(Path.GetDirectoryName(location), name) + ".dll";

                if (name == Translator.CLR_ASSEMBLY)
                {
                    if (string.IsNullOrEmpty(this.CLRLocation))
                    {
                        path = Translator.CLR_ASSEMBLY + ".dll";
                    }
                    else
                    {
                        path = this.CLRLocation;
                    }                    
                }

                var reference = this.LoadAssembly(path, references);

                if (!references.Contains(reference))
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

        protected virtual void InspecReferences()
        {
            var references = new HashSet<AssemblyDefinition>();
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

            Inspector inspector = this.CreateInspector();
            var prefix = Path.GetDirectoryName(this.Location);

            foreach (var path in this.SourceFiles)
            {
                inspector.VisitSyntaxTree(this.GetSyntaxTree(Path.Combine(prefix, path)));
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
                var syntaxTree = parser.Parse(reader);

                if (parser.HasErrors)
                {
                    ScriptKit.NET.Exception.Throw("Parsing error in a file {0}: {1}", fileName, parser.Errors.ToString());
                }
                
                return syntaxTree;
            }
        }
    }
}
