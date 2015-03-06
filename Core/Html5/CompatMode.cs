using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// mode is the string "BackCompat" for Quirks mode or "CSS1Compat" for Strict mode.
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNamePreserveCase)]
    [Name("String")]
    public enum CompatMode
    {
        /// <summary>
        /// 
        /// </summary>
        BackCompat,

        /// <summary>
        /// 
        /// </summary>
        CSS1Compat
    }
}
