using System;
namespace Bridge.Contract
{
    public interface IModuleDependency
    {
        string DependencyName { get; set; }
        string VariableName { get; set; }
    }
}
