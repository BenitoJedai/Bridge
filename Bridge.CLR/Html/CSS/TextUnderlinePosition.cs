namespace Bridge.CLR.Html
{
    /// <summary>
    /// The CSS text-underline-position property specifies the position of the underline which is set using the text-decoration property underline value.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum TextUnderlinePosition
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// This keyword allows the browser to use an algorithm to choose between under and alphabetic.
        /// </summary>
        Auto, 
        
        /// <summary>
        /// This keyword forces the line to be set below the alphabetic baseline, at a position where it won't cross any descender. This is useful to prevent chemical or mathematical formulas, which make a large use of subscripts, to be illegible.
        /// </summary>
        Under,

        /// <summary>
        /// In vertical writing-modes, this keyword forces the line to be placed on the left of the characters. In horizontal writing-modes, it is a synonym of under.
        /// </summary>
        Left,

        /// <summary>
        /// In vertical writing-modes, this keyword forces the line to be placed on the right of the characters. In horizontal writing-modes, it is a synonym of under.
        /// </summary>
        Right,

        [Bridge.CLR.Name("under left")]
        UnderLeft,

        [Bridge.CLR.Name("right under")]
        RightUnder
    }
}
