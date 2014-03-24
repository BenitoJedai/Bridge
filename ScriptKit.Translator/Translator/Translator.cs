using Mono.Cecil;
using System.IO;

namespace ScriptKit.NET
{
    public partial class Translator
    {
        public const string CLR_ASSEMBLY = "ScriptKit.CLR";
        
        public Translator(string location)
        {
            this.Location = location;
            this.Validator = this.CreateValidator();
        }

        public string Translate()
        {
            this.ReadProjectFile();

            if (this.Rebuild || !File.Exists(this.AssemblyLocation))
            {
                this.BuildAssembly();
            }

            var references = this.InspectReferences();
            references.Add(AssemblyDefinition.ReadAssembly(this.AssemblyLocation));

            var emitter = this.CreateEmitter();
            emitter.References = references;
            emitter.SourceFiles = this.SourceFiles;
            emitter.Emit();

            return emitter.Output.ToString();
        }

        protected virtual Emitter CreateEmitter()
        {
            return new Emitter(this.TypeDefinitions, this.Types, this.Validator);
        }

        protected virtual Validator CreateValidator()
        {
            return new Validator();
        }
    }
}
