namespace ScriptKit.CLR.Html
{
    [ScriptKit.CLR.Ignore]
    public class Event
    {
        internal Event()
        {
        }

        /// <summary>
        /// Indicates whether the ALT key was pressed when the event fired.
        /// </summary>
        public bool AltKey;

        /// <summary>
        /// Indicates whether the given event bubbles up through the DOM or not.
        /// Only certain events can bubble. Events that do bubble have this property set to true. You can use this property to check if an event is allowed to bubble or not.
        /// </summary>
        public bool Bubbles;

        /// <summary>
        /// Indicates which mouse button caused the event.
        /// This property returns an integer value indicating the button that changed state.
        ///
        ///        0 for standard "click"; this is usually the left button for a right-handed mouse and right button for a left-handed mouse.
        ///        1 for middle button; this is usually a click on the scroll wheel's button.
        ///        2 for right button; this is usually a right-click on a right-handed mouse and left-click on a left-handed mouse.
        ///        
        /// Note: This convention is not followed in Internet Explorer prior to version 9: See QuirksMode for details (http://www.quirksmode.org/js/events_properties.html#button).
        /// 
        /// Notes
        ///
        /// Because mouse clicks are frequently intercepted by the user interface, it may be difficult to detect buttons other than those for a standard mouse click (usually the left button) in some circumstances.
        ///
        /// Users may change the configuration of buttons on their pointing device so that if an event's button property is zero, it may not have been caused by the button that is physically left–most on the pointing device; however, it should behave as if the left button was clicked in the standard button layout.
        /// </summary>
        public int Button;


    }
}
