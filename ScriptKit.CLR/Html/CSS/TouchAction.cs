namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// Determines whether touch input may trigger default behavior supplied by the user agent, such as panning or zooming.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
    public enum TouchAction
    {
        /// <summary>
        /// 
        /// </summary>
        Inherit,

        /// <summary>
        /// The user agent MAY determine any permitted touch behaviors, such as panning and zooming manipulations of the viewport, for touches that begin on the element.
        /// </summary>
        Auto, 
        
        /// <summary>
        /// Touches that begin on the element MUST NOT trigger default touch behaviors.
        /// </summary>
        None,

        /// <summary>
        /// The user agent MAY consider touches that begin on the element only for the purposes of horizontally scrolling the element's nearest ancestor with horizontally scrollable content.
        /// </summary>
        [ScriptKit.CLR.Name("pan-x")]
        PanX,

        /// <summary>
        /// The user agent MAY consider touches that begin on the element only for the purposes of vertically scrolling the element's nearest ancestor with vertically scrollable content.
        /// </summary>
        [ScriptKit.CLR.Name("pan-y")]
        PanY
    }
}
