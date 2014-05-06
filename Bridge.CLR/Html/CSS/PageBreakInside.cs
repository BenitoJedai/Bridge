namespace Bridge.CLR.Html
{
    /// <summary>
    /// The page-break-inside CSS property adjusts page breaks inside the current element.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum PageBreakInside
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Initial value. Automatic page breaks (neither forced nor forbidden).
        /// </summary>
        Auto, 

        /// <summary>
        /// Avoid page breaks after the element.
        /// </summary>
        Avoid
    }
}
