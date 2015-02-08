using System;
namespace Bridge.Plugin
{
    public interface IPlugins
    {
        void BeforeEmit(Bridge.Plugin.IEmitter emitter, Bridge.Plugin.ITranslator translator);
        System.Collections.Generic.IEnumerable<string> GetConstructorInjectors(Bridge.Plugin.IConstructorBlock constructorBlock);
        bool HasConstructorInjectors(Bridge.Plugin.IConstructorBlock constructorBlock);
        System.Collections.Generic.IEnumerable<Bridge.Plugin.IPlugin> Parts { get; }
    }
}
