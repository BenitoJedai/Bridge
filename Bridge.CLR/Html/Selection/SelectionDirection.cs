namespace Bridge.CLR.Html
{
    /// <summary>
    /// The direction in which to adjust the current selection. 
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
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
