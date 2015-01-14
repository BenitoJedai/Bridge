using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;
using System;

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
            this.AddNestedTypes(assembly.MainModule.Types);
        }

        protected virtual void AddNestedTypes(IEnumerable<TypeDefinition> types)
        {
            foreach (TypeDefinition type in types)
            {
                if (type.FullName.Contains("<"))
                {
                    continue;
                }

                this.Validator.CheckType(type, this);
                this.TypeDefinitions.Add(Helpers.GetTypeMapKey(type), type);

                if (type.HasNestedTypes)
                {
                    this.AddNestedTypes(type.NestedTypes);
                }
            }
        }

        protected virtual List<AssemblyDefinition> InspectReferences()
        {
            this.TypeInfoDefinitions = new Dictionary<string, TypeInfo>();

            var references = new List<AssemblyDefinition>();            
            var assembly = this.LoadAssembly(this.AssemblyLocation, references);
            this.TypeDefinitions = new Dictionary<string, TypeDefinition>();
            this.AssemblyDefinition = assembly;
            
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

            this.AssemblyInfo = inspector.AssemblyInfo;
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

        protected virtual void InspectGlobalAspects(Emitter emitter)
        {
            if (this.AssemblyDefinition.HasCustomAttributes)
            {
                foreach (var attr in this.AssemblyDefinition.CustomAttributes)
                {
                    if (AspectHelpers.IsMulticastAspectAttribute(attr.AttributeType, emitter))
                    {
                        var globalAspects = this.AssemblyInfo.Aspects;

                        List<AspectInfo> aspects;
                        if (globalAspects.ContainsKey(AttributeTargets.Assembly))
                        {
                            aspects = globalAspects[AttributeTargets.Assembly];
                        }
                        else
                        {
                            aspects = new List<AspectInfo>();
                            globalAspects.Add(AttributeTargets.Assembly, aspects);
                        }

                        AspectInfo aspect = AspectHelpers.GetAspectInfo(attr, emitter, AttributeTargets.Assembly, null);
                        aspects.Add(aspect);                
                    }
                }
            }
        }
    }
}
