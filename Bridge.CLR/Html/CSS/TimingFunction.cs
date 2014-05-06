namespace Bridge.CLR.Html
{
    /// <summary>
    /// The CSS animation-timing-function property specifies how a CSS animation should progress over the duration of each cycle. 
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Bridge.CLR.Name("String")]
    public enum TimingFunction
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// 
        /// </summary>
        Ease,

        /// <summary>
        /// 
        /// </summary>
        [Bridge.CLR.Name("ease-in")]
        EaseIn,

        /// <summary>
        /// 
        /// </summary>
        [Bridge.CLR.Name("ease-out")]
        EaseOut,

        /// <summary>
        /// 
        /// </summary>
        [Bridge.CLR.Name("ease-in-out")]
        EaseInOut,

        /// <summary>
        /// 
        /// </summary>
        Linear,

        /// <summary>
        /// 
        /// </summary>
        [Bridge.CLR.Name("step-start")]
        StepStart,

        /// <summary>
        /// 
        /// </summary>
        [Bridge.CLR.Name("step-end")]
        StepEnd
    }
}
