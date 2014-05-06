namespace Bridge.CLR.Html
{
    /// <summary>
    /// The CSS mask-type properties defines if a SVG <mask> element is a luminance or an alpha mask.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum MaskType
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Is a keyword indicating that the associated <mask> is a luminance mask, that is that its relative luminance values must be used when applying it.
        /// </summary>
        Luminance, 
        
        /// <summary>
        /// Is a keyword indicating that the associated <mask> is an alpha mask, that is that its alpha channel values must be used when applying it.
        /// </summary>
        Alpha
    }
}
