using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Plugin
{
    public interface IPlugin
    {
        void BeforeEmit(IEmitter emitter, ITranslator translator);
        IEnumerable<string> GetConstructorInjectors(IConstructorBlock constructorBlock);
        bool HasConstructorInjectors(IConstructorBlock constructorBlock);
    }
}
