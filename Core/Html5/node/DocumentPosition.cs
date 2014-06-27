using System;

using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// Relationship of nodes
    /// </summary>
    [Ignore]
    [Enum(Emit.Value)]
    [Name("Number")]
    [Flags]
    public enum DocumentPosition
    {
        Disconnected = 1,
        Preceding = 2,
        Following = 4,
        Contains = 8,
        ContainedBy = 16,
        ImplementationSpecific = 32
    }
}
