using Bridge.Contract;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class AssemblyInfo : IAssemblyInfo
    {
        public const string DEFAULT_FILENAME = "---";
        public const string JAVASCRIPT_EXTENSION = "js";

        public AssemblyInfo()
        {
            this.Dependencies = new List<IPluginDependency>();
            this.AutoPropertyToField = true;        
        }

        /// <summary>
        /// A file name where JavaScript is generated to. If omitted, it is [Project_Name].js by default.
        /// Example: "MyBridgeNetLibrary.js"
        /// Tip. You can decorate a class with a [FileName('MyClass.js')] attribute. A class script will be generated to the defined file. It supersedes a global bridge.json fileName.
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// The output folder path for generated JavaScript. A non-absolute path is concatenated with a project's root. 
        /// Examples: "Bridge\\output\\", "..\\Bridge\\output\\", "c:\\Bridge\\output\\"
        /// </summary>
        public string Output
        {
            get;
            set;
        }

        private OutputBy outputBy = OutputBy.Namespace;

        /// <summary>
        /// The option to manage JavaScript output folders and files.
        /// See the OutputBy enum for more details.
        /// </summary>
        public OutputBy OutputBy
        {
            get
            {
                return this.outputBy;
            }
            set
            {
                this.outputBy = value;
            }
        }

        /// <summary>
        /// Substrings the file name starting with the defined index. 
        /// For example, it might be useful to get rid of the first namespace in the chain if use ByFullName or ByNamespace FilesHierarchy.
        /// </summary>
        public int StartIndexInName
        {
            get;
            set;
        }

        /// <summary>
        /// The global Module setting. The entire project is considered as one Module.
        /// Though, you are still able to define a Module attribute on the class level.
        /// </summary>
        public string Module
        {
            get;
            set;
        }

        /// <summary>
        /// The list of module dependencies.
        /// </summary>
        public List<IPluginDependency> Dependencies
        {
            get;
            set;
        }

        /// <summary>
        /// The executable file to be launched before building. The path will be concatenated with the project's root. 
        /// For example, it might be used for cleaning up the output directory - "Bridge\\builder\\clean.bat".
        /// </summary>
        public string BeforeBuild
        {
            get;
            set;
        }

        /// <summary>
        /// The executable file to be launched after building. The path will be concatenated with the project's root. 
        /// For example, it might be used for copying the generated JavaScript files to a Web application - "Bridge\\builder\\copy.bat"
        /// </summary>
        public string AfterBuild
        {
            get;
            set;
        }
        public bool AutoPropertyToField
        {
            get;
            set;
        }
    }
}
