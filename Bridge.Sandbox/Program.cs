using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bridge.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // PLEASE ENSURE THAT Bridge.CLR.dll file is available in the output folder (bin/Debug/)

            //var projectLocation = @"c:\projects\ext.net\Bridge.NET\Bridge.Sandbox\TestProject\TestProject.csproj ";
            //var clrLocation = @"c:\projects\ext.net\Bridge.NET\Bridge.CLR\bin\Debug\Bridge.CLR.dll";

            //var root = @"C:\Users\Geoffrey McGill\Dropbox\Ext.NET\Projects\Bridge.NET\";
            var root = @"c:\projects\ext.net\git\Bridge\";
            //var root = @"C:\Users\geoffreymcgill\Dropbox\Ext.NET\Projects\Bridge.NET\";

            var projectLocation = root + @"Bridge.TestLibrary\Bridge.TestLibrary.csproj ";
            var clrLocation = root + @"Bridge.CLR\bin\Debug\Bridge.CLR.dll";
            var outputLocation = Path.ChangeExtension(projectLocation, "js");
            
            try
            {
                var translator = new Bridge.NET.Translator(projectLocation);
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
