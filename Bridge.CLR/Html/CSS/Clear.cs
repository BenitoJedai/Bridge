namespace Bridge.CLR.Html
{
    /// <summary>
    /// The caption-side CSS property positions the content of a table's <caption> on the specified side.
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNameLowerCase)]
    [Name("String")]
    public enum Clear
    {
        /// <summary>
        /// The element is not moved down to clear past floating elements.
        /// </summary>
        None,

        /// <summary>
        /// The element is moved down to clear past left floats.
        /// </summary>
        Left,

        /// <summary>
        /// The element is moved down to clear past right floats.
        /// </summary>
        Right,

        /// <summary>
        /// The element is moved down to clear past both left and right floats.
        /// </summary>
        Both,
        
        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
