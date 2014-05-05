namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The list-style-type CSS property specifies appearance of a list item element. As it is the only one who defaults to display:list-item, this is usually a <li> element, but can be any element with this display value.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum ListStyleType
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        Disc,
        
        Circle,
        
        Square,
        
        Decimal,
        
        [ScriptKit.CLR.Name("decimal-leading-zero")]
        DecimalLeadingZero,
        
        [ScriptKit.CLR.Name("lower-roman")]
        LowerRoman,
    
        [ScriptKit.CLR.Name("upper-roman")]
        UpperRoman,

        [ScriptKit.CLR.Name("lower-greek")]
        LowerGreek,
        
        [ScriptKit.CLR.Name("lower-latin")]
        LowerLatin,
        
        [ScriptKit.CLR.Name("upper-latin")]
        UpperLatin,
        
        Armenian,
        
        Georgian,
        
        [ScriptKit.CLR.Name("lower-alpha")]
        LowerAlpha,

        [ScriptKit.CLR.Name("upper-alpha")]
        UpperAlpha,
        
        None
    }
}
