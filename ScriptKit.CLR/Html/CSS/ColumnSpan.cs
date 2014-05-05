namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The column-span CSS property makes it possible for an element to span across all columns when its value is set to all. An element that spans more than one column is called a spanning element.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[ScriptKit.CLR.Name("String")]
    public enum ColumnSpan
    {
        /// <summary>
        /// The element does not span multiple columns.
        /// </summary>
        None,

        /// <summary>
        /// The element spans across all columns. Content in the normal flow that appears before the element is automatically balanced across all columns before the element appears. The element establishes a new block formatting context.
        /// </summary>
        All, 
        
        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
