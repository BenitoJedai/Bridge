namespace Bridge.CLR.Html
{
    /// <summary>
    /// The DOM CustomEvent are events initialized by an application for any purpose.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("CustomEvent")]
    public class CustomEvent : Event
    {
        private CustomEvent()
        {
        }

        public CustomEvent(string type, CustomEventInit eventInitDict)
        {
        }

        /// <summary>
        /// The data passed when initializing the event.
        /// </summary>
        public readonly object Detail;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="canBubble"></param>
        /// <param name="cancelable"></param>
        /// <param name="detail"></param>
        public void InitCustomEvent(string type, bool canBubble, bool cancelable, object detail)
        {
        }
    }

    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Object")]
    public class CustomEventInit : EventInit
    {
        /// <summary>
        /// The data passed when initializing the event.
        /// </summary>
        public object Detail;
    }
}
