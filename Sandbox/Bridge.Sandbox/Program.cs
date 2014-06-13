using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bridge.Sandbox
{
    class Program
    {
        static void LogMessage(string level, string message)
        {
            level = level ?? "message";
            switch (level.ToLowerInvariant())
            {
                case "message":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Message: {0}", message);
                    Console.ResetColor();
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Warning: {0}", message);
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: {0}", message);
                    Console.ResetColor();
                    break;
            }
        }

        static void Main(string[] args)
        {
            // PLEASE ENSURE THAT Bridge.CLR.dll file is available in the output folder (bin/Debug/)

            //var projectLocation = @"c:\projects\ext.net\Bridge.NET\Bridge.Sandbox\TestProject\TestProject.csproj ";
            //var clrLocation = @"c:\projects\ext.net\Bridge.NET\Bridge.CLR\bin\Debug\Bridge.CLR.dll";

            //var root = @"C:\Users\Geoffrey McGill\Dropbox\Ext.NET\Projects\Bridge.NET\";
            var root = @"c:\projects\ext.net\git\Bridge.NET\";
            //var root = @"C:\Users\geoffreymcgill\Dropbox\Ext.NET\Projects\Bridge.NET\";
            
            var projectLocation = root + @"Sandbox\Bridge.TestLibrary\Bridge.TestLibrary.csproj ";
            //var projectLocation = root + @"Extensions\Bridge.DateTime\Bridge.DateTime.csproj ";
            //var projectLocation = root + @"Sandbox\CompanyX\CompanyX.csproj ";
            var clrLocation = root + @"Core\Bridge.CLR\bin\Debug\Bridge.CLR.dll";
            var outputLocation = Path.ChangeExtension(projectLocation, "js");
            
            try
            {
                var translator = new Bridge.NET.Translator(projectLocation);
                translator.CLRLocation = clrLocation;
                translator.Rebuild = false;
                translator.Log = LogMessage;
                translator.Translate();

                string path = string.IsNullOrWhiteSpace(Path.GetFileName(outputLocation)) ? outputLocation : Path.GetDirectoryName(outputLocation);
                if (translator.Outputs.Count == 1)
                {
                    translator.SaveToFile(outputLocation);
                }
                else
                {
                    translator.SaveTo(path, outputLocation);
                    
                }

                Console.WriteLine(translator.GetCode());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: {0}", e.Message);
                Console.ResetColor();
                Console.ReadLine();
            }
        }
    }
}
