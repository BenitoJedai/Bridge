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
            // PLEASE ENSURE THAT ScriptKit.CLR.dll file is presented in output folder (in bin/Debug/)
            
            var projectLocation = @"c:\projects\ext.net\ScriptKit.NET\ScriptKit.Sandbox\TestProject\TestProject.csproj ";
            var clrLocation = @"c:\projects\ext.net\ScriptKit.NET\ScriptKit.CLR\bin\Debug\ScriptKit.CLR.dll";
            var outputLocation = Path.ChangeExtension(projectLocation, "js");
            
            try
            {
                var translator = new ScriptKit.NET.Translator(projectLocation);
                translator.CLRLocation = clrLocation;
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
