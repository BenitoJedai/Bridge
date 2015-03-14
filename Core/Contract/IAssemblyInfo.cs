using System;

namespace Bridge.Contract
{
    /// <summary>
    /// The options to manage JavaScript output folders and files.
    /// </summary>
    public enum TypesSplit
    {
        /// <summary>
        /// No files hierarchy. 
        /// All scripts are generated to [Project_Name].js by default. 
        /// It might be overridden by fileName option in bridge.json or by FileName attribute on the assembly or class levels.
        /// </summary>
        None = 0,

        /// <summary>
        /// The files hierarchy will be constructed by the class full name. 
        /// Each word in the class namespace is an individual folder.
        /// The class name is a file name.
        /// </summary>
        ByFullName = 1,

        /// <summary>
        /// The class name is a file name. 
        /// If there are classes with same names in different namespaces it will be merged in one file.
        /// </summary>
        ByName = 2,

        /// <summary>
        /// If the class is associated with a Module (by [Module("Module_Name")] attribute, for example), the script goes to [Module_Name].js file.
        /// </summary>
        ByModule = 3,

        /// <summary>
        /// The class namespace is split into words by dot. The last one becomes a file name, each other becomes an individual folder.
        /// </summary>
        ByNamespace = 4
    }

    public interface IAssemblyInfo
    {
        System.Collections.Generic.List<IPluginDependency> Dependencies { get; set; }
        string FileName { get; set; }
        TypesSplit FilesHierarchy { get; set; }
        string Module { get; set; }
        string OutputDir { get; set; }
        int StartIndexInName { get; set; }
        string BeforeBuild { get; set; }
        string AfterBuild { get; set; }
    }
}
