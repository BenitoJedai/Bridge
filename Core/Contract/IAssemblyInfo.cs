using System;

namespace Bridge.Contract
{
    public enum TypesSplit
    {
        None = 0,
        ByFullName = 1,
        ByName = 2,
        ByModule = 3,
        ByNamespace = 4
    }

    public interface IAssemblyInfo
    {
        System.Collections.Generic.List<IModuleDependency> Dependencies { get; set; }
        string FileName { get; set; }
        TypesSplit FilesHierarchy { get; set; }
        string Module { get; set; }
        string OutputDir { get; set; }
        int StartIndexInName { get; set; }
        string BeforeBuild { get; set; }
        string AfterBuild { get; set; }
    }
}
