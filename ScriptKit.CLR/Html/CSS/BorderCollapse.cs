namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The border-collapse CSS property selects a table's border model. This has a big influence on the look and style of the table cells.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[ScriptKit.CLR.Name("String")]
    public enum BorderCollapse
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// Is a keyword requesting the use of the separated-border table rendering model. It is the default value.
        /// </summary>
        Separate, 
        
        /// <summary>
        /// Is a keyword requesting the use of the collapsed-border table rendering model.
        /// </summary>
        Collapse,

        /// <summary>
        /// 
        /// </summary>
        Inherit
    }
}
