namespace Bridge.CLR.Html
{
    /// <summary>
    /// The position relative to the element
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum InsertPosition
    {
        /// <summary>
        /// Before the element itself.
        /// </summary>
        BeforeBegin,

        /// <summary>
        /// Just inside the element, before its first child.
        /// </summary>
        AfterBegin,

        /// <summary>
        /// Just inside the element, after its last child.
        /// </summary>
        BeforeEnd,

        /// <summary>
        /// After the element itself.
        /// </summary>
        AfterEnd	
    }
}
