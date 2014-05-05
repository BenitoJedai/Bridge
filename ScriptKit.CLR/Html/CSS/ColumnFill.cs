namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The column-fill CSS property controls how contents are partitioned into columns. Contents are either balanced, which means that contents in all columns will have the same height or, when using auto, just take up the room the content needs.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[ScriptKit.CLR.Name("String")]
    public enum ColumnFill
    {
        /// <summary>
        /// Is a keyword indicating that columns are filled sequentially.
        /// </summary>
        Auto,

        /// <summary>
        /// Is a keyword indicating that content is equally divided between columns.
        /// </summary>
        Balance, 
        
        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
