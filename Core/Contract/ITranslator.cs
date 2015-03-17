using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Contract
{
    public interface ITranslator
    {
        Mono.Cecil.AssemblyDefinition AssemblyDefinition { get; set; }
        IAssemblyInfo AssemblyInfo { get; set; }
        string AssemblyLocation { get; }
        bool ChangeCase { get; set; }
        string BridgeLocation { get; set; }
        string Configuration { get; set; }
        string GetCode();
        string Location { get; }
        Action<string, string> Log { get; set; }
        string MSBuildVersion { get; set; }
        System.Collections.Generic.Dictionary<string, string> Outputs { get; }
        bool Rebuild { get; set; }
        void SaveTo(string path, string defaultFileName);
        System.Collections.Generic.IList<string> SourceFiles { get; }
        System.Collections.Generic.Dictionary<string, string> Translate();
        System.Collections.Generic.Dictionary<string, ITypeInfo> TypeInfoDefinitions { get; set; }
        System.Collections.Generic.List<ITypeInfo> Types { get; }
        IValidator Validator { get; }
        IPlugins Plugins { get; set; }
    }
}
