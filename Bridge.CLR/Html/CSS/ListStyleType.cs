namespace Bridge.CLR.Html
{
    /// <summary>
    /// The list-style-type CSS property specifies appearance of a list item element. As it is the only one who defaults to display:list-item, this is usually a <li> element, but can be any element with this display value.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
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
        
        [Bridge.CLR.Name("decimal-leading-zero")]
        DecimalLeadingZero,
        
        [Bridge.CLR.Name("lower-roman")]
        LowerRoman,
    
        [Bridge.CLR.Name("upper-roman")]
        UpperRoman,

        [Bridge.CLR.Name("lower-greek")]
        LowerGreek,
        
        [Bridge.CLR.Name("lower-latin")]
        LowerLatin,
        
        [Bridge.CLR.Name("upper-latin")]
        UpperLatin,
        
        Armenian,
        
        Georgian,
        
        [Bridge.CLR.Name("lower-alpha")]
        LowerAlpha,

        [Bridge.CLR.Name("upper-alpha")]
        UpperAlpha,
        
        None
    }
}
