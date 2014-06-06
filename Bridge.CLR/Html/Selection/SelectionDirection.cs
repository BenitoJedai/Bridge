namespace Bridge.CLR.Html
{
    /// <summary>
    /// The direction in which to adjust the current selection. 
    /// </summary>
    [Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Name("String")]
    public enum SelectionDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Forward,

        /// <summary>
        /// 
        /// </summary>
        Backward,

        /// <summary>
        /// 
        /// </summary>
        Left,

        /// <summary>
        /// 
        /// </summary>
        Right
    }
}
