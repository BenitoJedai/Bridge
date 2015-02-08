using System;
namespace Bridge.Plugin
{
    public interface IModuleDependency
    {
        string DependencyName { get; set; }
        string VariableName { get; set; }
    }
}
