namespace Bridge.CLR.Html
{
    /// <summary>
    /// mode is the string "BackCompat" for Quirks mode or "CSS1Compat" for Strict mode.
    /// </summary>
    [Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNamePreserveCase)]
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
