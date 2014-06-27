using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public class EmitterOutput
    {
        public EmitterOutput(string fileName)
        {
            this.FileName = fileName;
            this.ModuleOutput = new Dictionary<string, StringBuilder>();
            this.NonModuletOutput = new StringBuilder();
            this.ModuleDependencies = new Dictionary<string, List<ModuleDependency>>();
        }
        
        public string FileName
        {
            get;
            set;
        }

        public StringBuilder NonModuletOutput
        {
            get;
            set;
        }

        public Dictionary<string, StringBuilder> ModuleOutput
        {
            get;
            set;
        }

        public Dictionary<string, List<ModuleDependency>> ModuleDependencies
        {
            get;
            set;
        }

        public bool IsDefaultOutput
        {
            get
            {
                return this.FileName == AssemblyInfo.DEFAULT_FILENAME;
            }
        }
    }

    public class EmitterOutputs: Dictionary<string, EmitterOutput>
    {
        public EmitterOutput FindModuleOutput(string moduleName)
        {
            if (this.Any(o => o.Value.ModuleOutput.ContainsKey(moduleName)))
            {
                return this.First(o => o.Value.ModuleOutput.ContainsKey(moduleName)).Value;
            }

            return null;
        }

        public EmitterOutput DefaultOutput
        {
            get
            {
                return this.First(o => o.Value.IsDefaultOutput).Value;
            }
        }
    }
}
