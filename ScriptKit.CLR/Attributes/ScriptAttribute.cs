using System;

namespace ScriptKit.CLR
{
    /// <summary>
    /// ScriptAttribute is instruction how to convert method declaration (signature + implementation) to javascript
    /// </summary>
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Method)]
    public sealed class ScriptAttribute : Attribute
    {
        public ScriptAttribute(params string[] lines) 
        { 
        }
    }
}