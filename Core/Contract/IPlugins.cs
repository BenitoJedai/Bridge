using System;
namespace Bridge.Contract
{
    public interface IPlugins
    {
        void OnConfigRead(IAssemblyInfo config);
        void BeforeEmit(Bridge.Contract.IEmitter emitter, Bridge.Contract.ITranslator translator);
        System.Collections.Generic.IEnumerable<string> GetConstructorInjectors(Bridge.Contract.IConstructorBlock constructorBlock);
        bool HasConstructorInjectors(Bridge.Contract.IConstructorBlock constructorBlock);
        System.Collections.Generic.IEnumerable<Bridge.Contract.IPlugin> Parts { get; }
    }
}
