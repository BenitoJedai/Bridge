using System;

namespace Bridge.Foundation
{
    /// <summary>
    /// ScriptAttribute is instruction how to convert method declaration (signature + implementation) to javascript
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class ScriptAttribute : Attribute
    {
        public ScriptAttribute(params string[] lines) 
        { 
        }
    }
}