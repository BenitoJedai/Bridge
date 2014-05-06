namespace Bridge.CLR.Html
{
    /// <summary>
    /// The empty-cells CSS property specifies how user agents should render borders and backgrounds around cells that have no visible content.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[Bridge.CLR.Name("String")]
    public enum EmptyCells
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// Is a keyword indicating that borders and backgrounds should be drawn like in a normal cells.
        /// </summary>
        Show, 
        
        /// <summary>
        /// Is a keyword indicating that no border or backgrounds should be drawn.
        /// </summary>
        Hide
    }
}
