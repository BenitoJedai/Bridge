using System;

namespace Bridge
{
    /// <summary>
    /// The file name where JavaScript is generated to.
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Interface)]
    public sealed class FileNameAttribute : Attribute
    {
        public FileNameAttribute(string filename) 
        { 
        }
    }

    /// <summary>
    /// The output folder path for generated JavaScript. A non-absolute path is concatenated with a project's root. 
    /// Examples: "Bridge\\output\\", "..\\Bridge\\output\\", "c:\\output\\"
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class OutputDirAttribute : Attribute
    {
        public OutputDirAttribute(string dir)
        {
        }
    }

    /// <summary>
    /// The option to manage JavaScript output folders and files.
    /// See TypesSplit enum for more details.
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class FilesHierarchyAttribute : Attribute
    {
        public FilesHierarchyAttribute(TypesSplit splitBy)
        {
        }

        public FilesHierarchyAttribute(TypesSplit splitBy, int startIndexInName)
        {
        }
    }

    /// <summary>
    /// The options to manage JavaScript output folders and files.
    /// </summary>
    [Ignore]
    public enum TypesSplit
    {
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
}