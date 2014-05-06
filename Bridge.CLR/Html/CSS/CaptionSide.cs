namespace Bridge.CLR.Html
{
    /// <summary>
    /// The caption-side CSS property positions the content of a table's <caption> on the specified side.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[Bridge.CLR.Name("String")]
    public enum CaptionSide
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// The caption box will be above the table.
        /// </summary>
        Top,

        /// <summary>
        /// The caption box will be below the table.
        /// </summary>
        Bottom,
        
        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
