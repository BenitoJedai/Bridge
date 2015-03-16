using System;
using System.Diagnostics;
using Bridge.Contract;
using System.IO;
using Newtonsoft.Json;

namespace Bridge.NET
{
    public partial class Translator
    {
        protected virtual IAssemblyInfo ReadConfig(IAssemblyInfo mergeConfig)
        {
            var path = Path.GetDirectoryName(this.Location) + "\\Bridge\\bridge.json";

            if (!File.Exists(path))
            {
                return mergeConfig;
            }

            try
            {
                var json = File.ReadAllText(path);
                IAssemblyInfo assemblyInfo = JsonConvert.DeserializeObject<AssemblyInfo>(json);

                if (mergeConfig == null)
                {
                    return assemblyInfo;
                }

                if (string.IsNullOrWhiteSpace(mergeConfig.FileName))
                {
                    mergeConfig.FileName = assemblyInfo.FileName;
                }

                if (mergeConfig.OutputBy == OutputBy.Namespace)
                {
                    mergeConfig.OutputBy = assemblyInfo.OutputBy;
                }

                if (string.IsNullOrWhiteSpace(mergeConfig.Module))
                {
                    mergeConfig.Module = assemblyInfo.Module;
                }

                if (string.IsNullOrWhiteSpace(mergeConfig.Output))
                {
                    mergeConfig.Output = assemblyInfo.Output;
                }

                if (mergeConfig.StartIndexInName == 0)
                {
                    mergeConfig.StartIndexInName = assemblyInfo.StartIndexInName;
                }

                if (string.IsNullOrWhiteSpace(mergeConfig.BeforeBuild))
                {
                    mergeConfig.BeforeBuild = assemblyInfo.BeforeBuild;
                }

                if (string.IsNullOrWhiteSpace(mergeConfig.AfterBuild))
                {
                    mergeConfig.AfterBuild = assemblyInfo.AfterBuild;
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Cannot read bridge.json", e);                
            }

            return mergeConfig;
        }

        public virtual void RunEvent(string e)
        {
            var info = new ProcessStartInfo()
            {
                FileName = e
            };
            info.WindowStyle = ProcessWindowStyle.Hidden;
            using (var p = Process.Start(info))
            {
                p.WaitForExit();

                if (p.ExitCode != 0)
                {
                    Bridge.NET.Exception.Throw("Event (" + e + ") was not successful, exit code - " + p.ExitCode);
                }
            }
        }
    }
}
