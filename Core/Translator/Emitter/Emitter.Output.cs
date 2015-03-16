using System;
using System.Linq;
using Object.Net.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bridge.NET
{
    public partial class Emitter
    {
        protected virtual Dictionary<string, string> TransformOutputs()
        {
            this.WrapToModules();
            return this.CombineOutputs();
        }

        protected virtual Dictionary<string, string> CombineOutputs()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var outputPair in this.Outputs)
            {
                var fileName = outputPair.Key;
                var output = outputPair.Value;

                foreach (var moduleOutput in output.ModuleOutput)
                {
                    output.NonModuletOutput.AppendLine(moduleOutput.Value.ToString());
                }

                if (this.AssemblyInfo.Output.IsNotEmpty())
                {
                    //fileName = Path.Combine(this.AssemblyInfo.Output, fileName);
                }

                result.Add(fileName, output.NonModuletOutput.ToString());
            }

            return result;
        }

        protected virtual void WrapToModules()
        {
            foreach (var outputPair in this.Outputs)
            {
                var output = outputPair.Value;

                foreach (var moduleOutputPair in output.ModuleOutput)
                {
                    var moduleName = moduleOutputPair.Key;
                    var moduleOutput = moduleOutputPair.Value;
                    var str = moduleOutput.ToString();
                    moduleOutput.Length = 0;

                    moduleOutput.Append("define(");

                    if (moduleName != Bridge.NET.AssemblyInfo.DEFAULT_FILENAME)
                    {
                        moduleOutput.Append(this.ToJavaScript(moduleName));
                        moduleOutput.Append(", ");
                    }


                    moduleOutput.Append("[\"bridge\",");
                    if (output.ModuleDependencies.ContainsKey(moduleName) && output.ModuleDependencies[moduleName].Count > 0)
                    {
                        output.ModuleDependencies[moduleName].Each(md =>
                        {
                            moduleOutput.Append(this.ToJavaScript(md.DependencyName));
                            moduleOutput.Append(",");
                        });
                    }
                    moduleOutput.Remove(moduleOutput.Length - 1, 1); // remove trailing comma
                    moduleOutput.Append("], ");


                    moduleOutput.Append("function (_, ");

                    if (output.ModuleDependencies.ContainsKey(moduleName) && output.ModuleDependencies[moduleName].Count > 0)
                    {
                        output.ModuleDependencies[moduleName].Each(md =>
                        {
                            moduleOutput.Append(md.VariableName.IsNotEmpty() ? md.VariableName : md.DependencyName);
                            moduleOutput.Append(",");
                        });
                    }
                    moduleOutput.Remove(moduleOutput.Length - 1, 1); // remove trailing comma
                    moduleOutput.AppendLine(") {");

                    moduleOutput.Append("    ");
                    moduleOutput.AppendLine("var exports = {};");
                    moduleOutput.Append(str);
                    moduleOutput.Append("    ");
                    moduleOutput.AppendLine("return exports;");
                    moduleOutput.AppendLine("});");
                }
            }
        }

       
    }
}
