namespace Bridge.CLR.Html
{
    /// <summary>
    /// The element attribute gets or sets the text writing directionality of the content of the current element.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum TextDirection
    {
        /// <summary>
        /// direction of the element must be determined based on the contents of the element
        /// </summary>
        Auto,

        /// <summary>
        /// left-to-right
        /// </summary>
        Ltr, 
        
        /// <summary>
        /// right-to-left
        /// </summary>
        Rtl
    }
}
