using System;

namespace Bridge.Foundation
{
    /// <summary>
    /// 
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
    /// 
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class OutputDirAttribute : Attribute
    {
        public OutputDirAttribute(string dir)
        {
        }
    }

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

    [Ignore]
    public enum TypesSplit
    {
        ByFullName = 1,
        ByName = 2,
        ByModule = 3,
        ByNamespace = 4
    }
}