namespace Bridge.CLR.Html
{
    /// <summary>
    /// The animation-play-state CSS property determines whether an animation is running or paused. You can query this property's value to determine whether or not the animation is currently running; in addition, you can set its value to pause and resume playback of an animation.
    /// </summary>
    [Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[Name("String")]
    public enum AnimationPlayState
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// The animation is currently playing.
        /// </summary>
        Running,

        /// <summary>
        /// The animation is currently paused.
        /// </summary>
        Paused
    }
}
