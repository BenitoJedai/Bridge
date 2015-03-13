using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Bridge.Contract;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Microsoft.Ajax.Utilities;

namespace Bridge.NET
{
    public partial class Translator : ITranslator
    {
        public const string Foundation_ASSEMBLY = "Bridge.Foundation";
        
        public Translator(string location)
        {
            this.Location = location;
            this.Validator = this.CreateValidator();
        }

        public Dictionary<string, string> Translate()
        {
            var config = this.ReadConfig(null);

            if (config != null && !string.IsNullOrWhiteSpace(config.BeforeBuild))
            {
                this.RunEvent(config.BeforeBuild);
            }
            
            this.Plugins = Bridge.NET.Plugins.GetPlugins(this);
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
            emitter.Plugins = this.Plugins;
            this.Plugins.BeforeEmit(emitter, this);
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

        public virtual void SaveToFile(string outputDir, string defaultFileName)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in this.Outputs)
            {
                string code = item.Value;
                builder.AppendLine(code);
            }

            string keyPath = this.Outputs.First().Key;
            string path = Path.Combine(outputDir, keyPath.Replace(Bridge.NET.AssemblyInfo.DEFAULT_FILENAME, defaultFileName));
            var minifier = new Minifier();
            
            var file = new System.IO.FileInfo(path);            
            file.Directory.Create();
            File.WriteAllText(file.FullName, minifier.MinifyJavaScript(builder.ToString()), System.Text.UTF8Encoding.UTF8);

            string fileName = Path.GetFileNameWithoutExtension(path);
            path = Path.GetDirectoryName(path) + ("\\" + fileName + "-debug") + Path.GetExtension(path);
            file = new System.IO.FileInfo(path);
            file.Directory.Create();
            File.WriteAllText(file.FullName, builder.ToString(), System.Text.UTF8Encoding.UTF8);
        }

        public virtual void SaveTo(string dir, string defaultFileName)
        {
            var minifier = new Minifier();
            foreach (var item in this.Outputs)
            {
                string fileName = item.Key;
                string code = item.Value;

                if (fileName.Contains(Bridge.NET.AssemblyInfo.DEFAULT_FILENAME))
                {
                    fileName = fileName.Replace(Bridge.NET.AssemblyInfo.DEFAULT_FILENAME, defaultFileName);
                }

                if (!Path.HasExtension(fileName))
                {
                    fileName = Path.ChangeExtension(fileName, "js");
                }

                string filePath = Path.Combine(dir, fileName);

                var file = new System.IO.FileInfo(filePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, minifier.MinifyJavaScript(code), System.Text.UTF8Encoding.UTF8);

                string fn = Path.GetFileNameWithoutExtension(filePath);
                filePath = Path.GetDirectoryName(filePath) + ("\\" + fn + "-debug") + Path.GetExtension(filePath);
                file = new System.IO.FileInfo(filePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, code, System.Text.UTF8Encoding.UTF8);
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
            var resourceName = "Bridge.Foundation.resources.bridge.js";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    File.WriteAllText(Path.Combine(outputDir, "bridge.js"), reader.ReadToEnd());
                }
            }

            if (!nodebug)
            {
                resourceName = "Bridge.Foundation.resources.bridge-debug.js";

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
