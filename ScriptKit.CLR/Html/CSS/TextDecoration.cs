namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The text-decoration CSS property is used to set the text formatting to underline, overline, line-through or blink.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum TextDecoration
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Produces no text decoration.
        /// </summary>
        None,
        
        /// <summary>
        /// Each line of text is underlined.
        /// </summary>
        Underline,
        
        /// <summary>
        /// Each line of text has a line above it.
        /// </summary>
        Overline,
        
        /// <summary>
        /// Each line of text has a line through the middle.
        /// </summary>
        [ScriptKit.CLR.Name("line-through")]
        LineThrough        
    }
}
