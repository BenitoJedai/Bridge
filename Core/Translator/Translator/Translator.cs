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
        public const string Bridge_ASSEMBLY = "Bridge";
        
        public Translator(string location)
        {
            this.Location = location;
            this.Validator = this.CreateValidator();
        }

        public Dictionary<string, string> Translate()
        {
            this.Plugins = Bridge.NET.Plugins.GetPlugins(this);
            var config = this.ReadConfig();

            if (config != null && !string.IsNullOrWhiteSpace(config.BeforeBuild))
            {
                this.RunEvent(config.BeforeBuild);
            }
            
            this.ReadProjectFile();

            if (this.Rebuild || !File.Exists(this.AssemblyLocation))
            {
                this.BuildAssembly();
            }

            var references = this.InspectReferences();            

            var resolver = new MemberResolver(this.SourceFiles, Emitter.ToAssemblyReferences(references));

            this.InspectTypes(resolver, config);
            
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

        public virtual void SaveTo(string path, string defaultFileName)
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

                if (!fileName.ToLower().EndsWith("." + Bridge.NET.AssemblyInfo.JAVASCRIPT_EXTENSION))
                {
                    fileName += "." + Bridge.NET.AssemblyInfo.JAVASCRIPT_EXTENSION;
                }

                // Ensure filename contains no ":". It could be used like "c:/absolute/path"
                fileName = fileName.Replace(":", "_");

                // Trim heading slash/backslash off file names until it does not start/end with slash.
                int count = 0;
                while (Path.IsPathRooted(fileName))
                {
                    fileName = fileName.TrimStart('/', '\\');
                    count++;
                    if (count > 1000) throw new InvalidDataException("Provided file path '" + item.Key + "' can't be translated into a valid file name.");
                }

                string filePath = Path.Combine(path, fileName);

                var file = new System.IO.FileInfo(filePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, code, System.Text.UTF8Encoding.UTF8);

                string fn = Path.GetFileNameWithoutExtension(filePath);
                filePath = Path.GetDirectoryName(filePath) + ("\\" + fn + ".min") + Path.GetExtension(filePath);
                file = new System.IO.FileInfo(filePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, minifier.MinifyJavaScript(code), System.Text.UTF8Encoding.UTF8);
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

        public static void ExtractCore(string clrPath, string outputPath)
        {
            Translator.ExtractCore(clrPath, outputPath, false);
        }

        public static void ExtractCore(string clrPath, string outputPath, bool nodebug)
        {
            var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(clrPath);
            var resourceName = "Bridge.resources.bridge.js";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    File.WriteAllText(Path.Combine(outputPath, "bridge.js"), reader.ReadToEnd());
                }
            }

            if (!nodebug)
            {
                resourceName = "Bridge.resources.bridge.min.js";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        File.WriteAllText(Path.Combine(outputPath, "bridge.min.js"), reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
