using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public Dictionary<string, string> Translate()
        {
            this.ReadProjectFile();

            if (this.Rebuild || !File.Exists(this.AssemblyLocation))
            {
                this.BuildAssembly();
            }

            var references = this.InspectReferences();            

            var resolver = new MemberResolver(this.SourceFiles, Emitter.ToAssemblyReferences(references));

            this.InspectTypes(resolver);
            

            resolver.CanFreeze = true;
            var emitter = this.CreateEmitter();
            emitter.TypeInfoDefinitions = this.TypeInfoDefinitions;
            emitter.AssemblyInfo = this.AssemblyInfo;
            emitter.Resolver = resolver;
            emitter.ChangeCase = this.ChangeCase;
            emitter.References = references;
            emitter.SourceFiles = this.SourceFiles;
            emitter.Log = this.Log;
            this.Outputs = emitter.Emit();

            return this.Outputs;
        }

        public virtual string GetCode()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in this.Outputs)
            {
                string code = item.Value;
                builder.AppendLine(code);
            }

            return builder.ToString();
        }

        public virtual void SaveToFile(string path)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in this.Outputs)
            {
                string code = item.Value;
                builder.AppendLine(code);
            }

            var file = new System.IO.FileInfo(path);
            file.Directory.Create();
            File.WriteAllText(file.FullName, builder.ToString());
        }

        public virtual void SaveTo(string dir, string defaultFileName)
        {
            foreach (var item in this.Outputs)
            {
                string fileName = item.Key;
                string code = item.Value;

                if (fileName == AssemblyInfo.DEFAULT_FILENAME)
                {
                    fileName = defaultFileName;
                }

                if (!Path.HasExtension(fileName))
                {
                    fileName = Path.ChangeExtension(fileName, "js");
                }

                string filePath = Path.Combine(dir, fileName);

                var file = new System.IO.FileInfo(filePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, code);
            }            
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
