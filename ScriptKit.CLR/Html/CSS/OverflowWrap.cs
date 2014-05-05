namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The word-wrap CSS property is used to specify whether or not the browser may break lines within words in order to prevent overflow (in other words, force wrapping) when an otherwise unbreakable string is too long to fit in its containing box.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum OverflowWrap
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Indicates that lines may only break at normal word break points.
        /// </summary>
        Normal, 
        
        /// <summary>
        /// Indicates that normally unbreakable words may be broken at arbitrary points if there are no otherwise acceptable break points in the line.
        /// </summary>
        [ScriptKit.CLR.Name("break-word")]
        BreakWord
    }
}
