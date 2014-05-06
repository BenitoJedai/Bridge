namespace Bridge.CLR.Html
{
    /// <summary>
    /// The border style CSS property sets the width of the border of a box.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[Bridge.CLR.Name("String")]
    public enum BorderWidth
    {
        /// <summary>
        /// A thin border
        /// </summary>
        Thin,
 
        /// <summary>
        /// A medium border
        /// </summary>
        Medium,
 
        /// <summary>
        /// A thick border
        /// </summary>
        Thick,

        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
