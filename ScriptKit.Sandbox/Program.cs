using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptKit.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // PLEASE ENSURE THAT ScriptKit.CLR.dll file is available in the output folder (bin/Debug/)

            //var projectLocation = @"c:\projects\ext.net\ScriptKit.NET\ScriptKit.Sandbox\TestProject\TestProject.csproj ";
            //var clrLocation = @"c:\projects\ext.net\ScriptKit.NET\ScriptKit.CLR\bin\Debug\ScriptKit.CLR.dll";

            //var root = @"C:\Users\Geoffrey McGill\Dropbox\Ext.NET\Projects\ScriptKit.NET\";
            var root = @"c:\projects\ext.net\git\ScriptKit\";

            var projectLocation = root + @"ScriptKit.TestLibrary\ScriptKit.TestLibrary.csproj ";
            var clrLocation = root + @"ScriptKit.CLR\bin\Debug\ScriptKit.CLR.dll";
            var outputLocation = Path.ChangeExtension(projectLocation, "js");
            
            try
            {
                var translator = new ScriptKit.NET.Translator(projectLocation);
                translator.CLRLocation = clrLocation;
                translator.Rebuild = false;
                string code = translator.Translate();
                File.WriteAllText(outputLocation, code);
                Console.WriteLine(code);
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
