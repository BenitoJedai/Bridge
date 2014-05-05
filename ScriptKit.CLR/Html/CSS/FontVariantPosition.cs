namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The font-variant-position property is used to enable typographic subscript and superscript glyphs. These are alternate glyphs designed within the same em-box as default glyphs and are intended to be laid out on the same baseline as the default glyphs, with no resizing or repositioning of the baseline. They are explicitly designed to match the surrounding text and to be more readable without affecting the line height.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum FontVariantPosition
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// None of the features are enabled.
        /// </summary>
        Normal, 
        
        /// <summary>
        /// Enables display of subscript variants (OpenType feature: subs). 
        /// </summary>
        Sub,

        /// <summary>
        /// Enables display of superscript variants (OpenType feature: sups).
        /// </summary>
        Super
    }
}
