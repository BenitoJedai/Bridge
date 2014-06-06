namespace Bridge.CLR.Html
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("HashChangeEvent")]
    public class HashChangeEvent : Event
    {
        private HashChangeEvent()
        {
        }

        /// <summary>
        /// The new URL to which the window is navigating.
        /// </summary>
        public readonly string NewURL;

        /// <summary>
        /// The previous URL from which the window was navigated.
        /// </summary>
        public readonly string OldURL;
    }
}
