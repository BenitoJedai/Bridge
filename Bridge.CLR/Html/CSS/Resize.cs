namespace Bridge.CLR.Html
{
    /// <summary>
    /// The resize CSS property lets you control the resizability of an element.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum Resize
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// The element offers no user-controllable method for resizing the element.
        /// </summary>
        None,

        /// <summary>
        /// The element displays a mechanism for allowing the user to resize the element, which may be resized both horizontally and vertically.
        /// </summary>
        Both,

        /// <summary>
        /// The element displays a mechanism for allowing the user to resize the element, which may only be resized horizontally.
        /// </summary>
        Horizontal,

        /// <summary>
        /// The element displays a mechanism for allowing the user to resize the element, which may only be resized vertically.
        /// </summary>
        Vertical
    }
}
