namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The word-break CSS property is used to specify how (or if) to break lines within words.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum WordBreak
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Use the default line break rule.
        /// </summary>
        Normal, 
        
        /// <summary>
        /// Word breaks may be inserted between any character for non-CJK (Chinese/Japanese/Korean) text.
        /// </summary>
        [ScriptKit.CLR.Name("break-all")]
        BreakAll,

        /// <summary>
        /// Don't allow word breaks for CJK text.  Non-CJK text behavior is same as normal.
        /// </summary>
        [ScriptKit.CLR.Name("keep-all")]
        KeepAll
    }
}
