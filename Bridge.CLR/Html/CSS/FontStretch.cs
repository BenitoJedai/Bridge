namespace Bridge.CLR.Html
{
    /// <summary>
    /// The font-stretch CSS property selects a normal, condensed, or expanded face from a font.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum FontStretch
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies a normal font face.
        /// </summary>
        Normal, 
        
        /// <summary>
        /// Specifies a font face more condensed than normal, with ultra-condensed as the most condensed.
        /// </summary>
        [Bridge.CLR.Name("semi-condensed")]
        SemiCondensed, 
    
        /// <summary>
        /// Specifies a font face more condensed than normal, with ultra-condensed as the most condensed.
        /// </summary>
        Condensed,     

        /// <summary>
        /// Specifies a font face more condensed than normal, with ultra-condensed as the most condensed.
        /// </summary>
        [Bridge.CLR.Name("extra-condensed")]
        ExtraCondensed, 
    
        /// <summary>
        /// Specifies a font face more condensed than normal, with ultra-condensed as the most condensed.
        /// </summary>
        [Bridge.CLR.Name("ultra-condensed")]
        UltraCondensed,

        /// <summary>
        /// Specifies a font face more expanded than normal, with ultra-expanded as the most expanded.
        /// </summary>
        [Bridge.CLR.Name("semi-expanded")]
        SemiExpanded, 
    
        /// <summary>
        /// Specifies a font face more expanded than normal, with ultra-expanded as the most expanded.
        /// </summary>
        Expanded, 
    
        /// <summary>
        /// Specifies a font face more expanded than normal, with ultra-expanded as the most expanded.
        /// </summary>
        [Bridge.CLR.Name("extra-expanded")]
        ExtraExpanded, 
    
        /// <summary>
        /// Specifies a font face more expanded than normal, with ultra-expanded as the most expanded.
        /// </summary>
        [Bridge.CLR.Name("ultra-expanded")]
        UltraExpanded
    }
}
