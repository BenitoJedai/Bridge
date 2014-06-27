using System;
using System.IO;
using System.Reflection;

namespace Bridge.Builder
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
            string projectLocation = null;
            string outputLocation = null;
            bool rebuild = false;
            bool extractCore = true;
            bool changeCase = true;

            if (args.Length == 0)
            {
                Console.WriteLine("Bridge.Builder commands:");
                Console.WriteLine("-p or -project     Path to csproj file (required)");
                Console.WriteLine("-o or -output      Output file name of generated script");
                Console.WriteLine("-r or -rebuild     Force assembly rebuilding");
                Console.WriteLine("-nocore            Do not extract core javascript files");
                Console.WriteLine("-c or -case        Do not change case of members");
                Console.WriteLine("");
                return;
            }

            int i = 0;
            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-p":
                    case "-project":
                        projectLocation = args[++i];
                        break;
                    case "-o":
                    case "-output":
                        outputLocation = args[++i];
                        break;
                    case "-rebuild":
                    case "-r":
                        rebuild = true;                        
                        break;
                    case "-case":
                    case "-c":
                        changeCase = false;
                        break;                   
                    case "-nocore":
                        extractCore = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command: " + args[i]);
                        return;
                }

                i++;
            }

            if (string.IsNullOrEmpty(projectLocation))
            {
                Console.WriteLine("Project location is empty, please define argument with -p command");
                return;
            }

            if (!File.Exists(projectLocation))
            {
                Console.WriteLine("Project file doesn't exist: " + projectLocation);
                return;
            }

            if (string.IsNullOrEmpty(outputLocation))
            {
                outputLocation = Path.ChangeExtension(projectLocation, "js");
            }

            try
            {
                Console.WriteLine("Generating script...");
                var translator = new Bridge.NET.Translator(projectLocation);
                translator.CLRLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Bridge.CLR.dll");
                translator.Rebuild = rebuild;
                translator.ChangeCase = changeCase;
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

                if (extractCore)
                {
                    Console.WriteLine("Extracting core scripts...");
                    Bridge.NET.Translator.ExtractCore(translator.CLRLocation, path);
                }

                Console.WriteLine("Done.");
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
