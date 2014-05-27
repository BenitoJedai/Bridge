namespace Bridge.CLR.Html
{
    /// <summary>
    /// mode is the string "BackCompat" for Quirks mode or "CSS1Compat" for Strict mode.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNamePreserveCase)]
    [Bridge.CLR.Name("String")]
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
