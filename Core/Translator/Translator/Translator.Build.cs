using System;
using System.Diagnostics;

namespace Bridge.NET
{
    public partial class Translator
    {        
        protected virtual string GetBuilderPath()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return String.Format("{0}\\Microsoft.NET\\Framework\\v{1}\\msbuild", Environment.GetEnvironmentVariable("windir"), this.MSBuildVersion);
                default:
                    throw (Exception)Bridge.NET.Exception.Create("Unsupported platform - {0}", Environment.OSVersion.Platform);
            }
        }

        protected virtual string GetBuilderArguments()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return String.Format(" \"{0}\" /t:Rebuild /p:Configuation={1}", Location, this.Configuration);
                default:
                    throw (Exception)Bridge.NET.Exception.Create("Unsupported platform - {0}", Environment.OSVersion.Platform);
            }
        }

        protected virtual void BuildAssembly()
        {
            var info = new ProcessStartInfo()
            {
                FileName = this.GetBuilderPath(),
                Arguments = this.GetBuilderArguments()
            };
            info.WindowStyle = ProcessWindowStyle.Hidden;
            using (var p = Process.Start(info))
            {
                p.WaitForExit();

                if (p.ExitCode != 0)
                {
                    Bridge.NET.Exception.Throw("Compilation was not successful, exit code - " + p.ExitCode);
                }
            }
        }        
    }
}
