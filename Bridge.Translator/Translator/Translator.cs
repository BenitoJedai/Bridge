using Mono.Cecil;
using System.IO;

namespace Bridge.NET
{
    public partial class Translator
    {
        public const string CLR_ASSEMBLY = "Bridge.CLR";
        
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
            emitter.ChangeCase = this.ChangeCase;
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

        public static void ExtractCore(string clrPath, string outputDir)
        {
            Translator.ExtractCore(clrPath, outputDir, false);
        }

        public static void ExtractCore(string clrPath, string outputDir, bool nodebug)
        {
            var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(clrPath);
            var resourceName = "Bridge.CLR.resources.bridge.js";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    File.WriteAllText(Path.Combine(outputDir, "bridge.js"), reader.ReadToEnd());
                }
            }

            if (!nodebug)
            {
                resourceName = "Bridge.CLR.resources.bridge-debug.js";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        File.WriteAllText(Path.Combine(outputDir, "bridge-debug.js"), reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
