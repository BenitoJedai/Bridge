using System;
namespace Bridge.Plugin
{
    public interface IEmitterOutput
    {
        string FileName { get; set; }
        bool IsDefaultOutput { get; }
        System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<IModuleDependency>> ModuleDependencies { get; set; }
        System.Collections.Generic.Dictionary<string, System.Text.StringBuilder> ModuleOutput { get; set; }
        System.Text.StringBuilder NonModuletOutput { get; set; }
    }
}
