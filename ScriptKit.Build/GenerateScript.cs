using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptKit.Build
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

        [Required]
        public string AssemliesPath
        {
            get;
            set;
        }

        private bool changeCase = true;
        public bool ChangeCase
        {
            get
            {
                return this.changeCase;
            }
            set
            {
                this.changeCase = value;
            }
        }

        public bool NoCore
        {
            get;
            set;
        }

        public override bool Execute()
        {
            var success = true;
            try
            {
                var translator = new ScriptKit.NET.Translator(this.ProjectPath);
                translator.CLRLocation = Path.Combine(this.AssemliesPath, "ScriptKit.CLR.dll");                
                translator.Rebuild = false;
                translator.ChangeCase = this.ChangeCase;
                string code = translator.Translate();
                File.WriteAllText(Path.Combine(this.OutputPath, Path.GetFileNameWithoutExtension(this.Assembly.ItemSpec) + ".js"), code);

                if (!this.NoCore)
                {
                    ScriptKit.NET.Translator.ExtractCore(translator.CLRLocation, this.OutputPath);
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
