using System;
using System.Collections.Generic;

namespace Bridge.Plugin
{
    public interface IEmitterOutputs : IDictionary<string, IEmitterOutput>
    {
        IEmitterOutput DefaultOutput { get; }
        IEmitterOutput FindModuleOutput(string moduleName);
    }
}
