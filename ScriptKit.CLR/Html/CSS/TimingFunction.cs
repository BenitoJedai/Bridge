namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The CSS animation-timing-function property specifies how a CSS animation should progress over the duration of each cycle. 
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [ScriptKit.CLR.Name("String")]
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
        [ScriptKit.CLR.Name("ease-in")]
        EaseIn,

        /// <summary>
        /// 
        /// </summary>
        [ScriptKit.CLR.Name("ease-out")]
        EaseOut,

        /// <summary>
        /// 
        /// </summary>
        [ScriptKit.CLR.Name("ease-in-out")]
        EaseInOut,

        /// <summary>
        /// 
        /// </summary>
        Linear,

        /// <summary>
        /// 
        /// </summary>
        [ScriptKit.CLR.Name("step-start")]
        StepStart,

        /// <summary>
        /// 
        /// </summary>
        [ScriptKit.CLR.Name("step-end")]
        StepEnd
    }
}
