using System.IO;

namespace ScriptKit.NET
{
    public partial class Translator
    {
        const string CLR_ASSEMBLY = "ScriptKit.CLR";
        const string CORE_ASSEMBLY = "ScriptKit.Core";
        
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

            this.InspecReferences();

            var emitter = this.CreateEmitter();
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
