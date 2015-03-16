using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Bridge.NET
{
    public partial class Translator
    {   
        protected virtual void ReadProjectFile()
        {
            var doc = new XmlDocument();

            doc.Load(Location);

            var manager = new XmlNamespaceManager(new NameTable());
            manager.AddNamespace("my", "http://schemas.microsoft.com/developer/msbuild/2003");

            this.BuildAssemblyLocation(doc, manager);
            this.SourceFiles = this.GetSourceFiles(doc, manager);
        }

        protected virtual void BuildAssemblyLocation(XmlDocument doc, XmlNamespaceManager manager)
        {            
            if (this.AssemblyLocation == null || this.AssemblyLocation.Length == 0)
            {
                this.Configuration = this.Configuration ?? "Debug";
                var outputPath = this.GetOutputPath(doc, manager, this.Configuration);

                if (string.IsNullOrEmpty(outputPath))
                {
                    outputPath = this.GetOutputPath(doc, manager, "Release");
                }

                this.AssemblyLocation = Path.Combine(outputPath, this.GetAssemblyName(doc, manager) + ".dll");

                if (!File.Exists(this.AssemblyLocation))
                {
                    outputPath = this.GetOutputPath(doc, manager, "Release");
                    this.AssemblyLocation = Path.Combine(outputPath, this.GetAssemblyName(doc, manager) + ".dll");
                }
            }
        }

        protected virtual string GetOutputPath(XmlDocument doc, XmlNamespaceManager manager, string configuration)
        {
            var nodes = doc.SelectNodes("//my:PropertyGroup[contains(@Condition,'" + configuration + "')]/my:OutputPath", manager);
            if (nodes.Count != 1)
            {
                Bridge.NET.Exception.Throw("Unable to determine output path");
            }

            var path = nodes[0].InnerText;

            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Location), path));
            }

            return path;
        }

        protected virtual IList<string> GetSourceFiles(XmlDocument doc, XmlNamespaceManager manager)
        {
            var result = new List<string>();

            foreach (XmlNode node in doc.SelectNodes("//my:Compile[@Include]", manager))
            {
                result.Add(node.Attributes["Include"].InnerText);
            }
            
            return result;
        }

        protected virtual string GetAssemblyName(XmlDocument doc, XmlNamespaceManager manager)
        {
            var nodes = doc.SelectNodes("//my:AssemblyName", manager);
            
            if (nodes.Count != 1)
            {
                Bridge.NET.Exception.Throw("Unable to determine assembly name");
            }
            
            return nodes[0].InnerText;
        }        
    }
}
