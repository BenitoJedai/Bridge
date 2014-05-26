namespace Bridge.CLR.Html
{
    /// <summary>
    /// The distance to adjust the current selection or cursor position. 
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum SelectionGranularity
    {
        /// <summary>
        /// 
        /// </summary>
        Character,

        /// <summary>
        /// 
        /// </summary>
        Word,

        /// <summary>
        /// 
        /// </summary>
        Sentence,

        /// <summary>
        /// 
        /// </summary>
        Line,

        /// <summary>
        /// 
        /// </summary>
        Paragraph,

        /// <summary>
        /// 
        /// </summary>
        LineBoundary,

        /// <summary>
        /// 
        /// </summary>
        SentenceBoundary,

        /// <summary>
        /// 
        /// </summary>
        ParagraphBoundary,

        /// <summary>
        /// 
        /// </summary>
        DocumentBoundary
    }
}
