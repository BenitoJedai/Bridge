using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptKit.NET.Tasks
{
    public class GenerateScript : Task
    {
        [Required]
        public ITaskItem Assembly
        {
            get;
            set;
        }

        [Required]
        public string OutputPath
        {
            get;
            set;
        }

        [Required]
        public string ProjectPath
        {
            get;
            set;
        }        

        public override bool Execute()
        {
            var success = true;
            try
            {                
                var translator = new ScriptKit.NET.Translator(this.BuildEngine3.ProjectFileOfTaskNode);
                translator.CLRLocation = "ScriptKit\\ScriptKit.CLR.dll";
                translator.Rebuild = false;
                string code = translator.Translate();                
                File.WriteAllText(Path.Combine(this.OutputPath, Path.GetFileNameWithoutExtension(this.Assembly.ItemSpec) + ".js"), code);

                var assembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom("ScriptKit\\ScriptKit.CLR.dll");
                var resourceName = "ScriptKit.CLR.resources.scriptkit.js";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        File.WriteAllText(Path.Combine(this.OutputPath, "scriptkit.js"), reader.ReadToEnd());
                    }
                }

                resourceName = "ScriptKit.CLR.resources.scriptkit-debug.js";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        File.WriteAllText(Path.Combine(this.OutputPath, "scriptkit-debug.js"), reader.ReadToEnd());
                    }
                }              
            }
            catch (Exception e)
            {
                Log.LogError("Error: {0}", e.Message);
                success = false;
            }

            return success;
        }
    }
}
