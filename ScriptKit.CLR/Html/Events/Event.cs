// Event WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/event
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

        /// <summary>
        /// Indicates whether the event is cancelable or not.
        /// Whether an event can be canceled or not is something that's determined when that event is initialized.
        /// To cancel an event, call the preventDefault() method on the event. This keeps the implementation from executing the default action that is associated with the event.
        /// </summary>
        public bool Cancelable;

        /// <summary>
        /// Returns the horizontal coordinate within the application's client area at which the event occurred (as opposed to the coordinates within the page). For example, clicking in the top-left corner of the client area will always result in a mouse event with a clientX value of 0, regardless of whether the page is scrolled horizontally.
        /// </summary>
        public int ClientX;

        /// <summary>
        /// Returns the vertical coordinate within the application's client area at which the event occurred (as opposed to the coordinates within the page). For example, clicking in the top-left corner of the client area will always result in a mouse event with a clientY value of 0, regardless of whether the page is scrolled vertically.
        /// </summary>
        public int ClientY;

        /// <summary>
        /// Indicates whether the CTRL key was pressed when the event fired.
        /// </summary>
        public bool CtrlKey;

        /// <summary>
        /// Identifies the current target for the event, as the event traverses the DOM. It always refers to the element the event handler has been attached to as opposed to event.target which identifies the element on which the event occurred.
        /// On Internet Explorer 6 through 8, the event model is different. Event listeners are attached with the non-standard element.attachEvent method. In this model, there is no equivalent to event.currentTarget and this is the global object. 
        /// </summary>
        public Element CurrentTarget;

        /// <summary>
        /// Returns a boolean indicating whether or not event.preventDefault() was called on the event.
        /// </summary>
        public bool DefaultPrevented;

        /// <summary>
        /// Returns additional numerical information about the event, depending on the type of event. 
        /// For mouse events, such as click, dblclick, mousedown, or mouseup, the detail property indicates how many times the mouse has been clicked in the same location for this event.
        /// For a dblclick event the value of detail is always 2.
        /// </summary>
        public int Detail;

        /// <summary>
        /// Indicates which phase of the event flow is currently being evaluated.
        /// Returns an integer value represented by 4 constants:
        /// Event.NONE = 0
        /// Event.CAPTURING_PHASE = 1
        /// Event.AT_TARGET = 2
        /// Event.BUBBLING_PHASE = 3
        /// </summary>
        public int EventPhase;

        /// <summary>
        /// Returns the Unicode value of a non-character key in a keypress event or any key in any other type of keyboard event.
        /// 
        /// In a keypress event, the Unicode value of the key pressed is stored in either the keyCode or charCode property, never both. If the key pressed generates a character (e.g. 'a'), charCode is set to the code of that character, respecting the letter case. (i.e. charCode takes into account whether the shift key is held down). Otherwise, the code of the pressed key is stored in keyCode.
        /// 
        /// keyCode is always set in the keydown and keyup events. In these cases, charCode is never set.
        /// 
        /// To get the code of the key regardless of whether it was stored in keyCode or charCode, query the which property.
        /// 
        /// Characters entered through an IME do not register through keyCode or charCode.
        /// </summary>
        public int KeyCode;

        /// <summary>
        /// Returns the horizontal coordinate of the event relative to the current layer.
        /// LayerX takes scrolling of the page into account, and returns a value relative to the whole of the document, unless the event occurs inside a positioned element, where the returned value is relative to the top left of the positioned element.
        /// </summary>
        public int LayerX;

        /// <summary>
        /// Returns the vertical coordinate of the event relative to the current layer.
        /// LayerY takes scrolling of the page into account, and returns a value relative to the whole of the document, unless the event occurs inside a positioned element, where the returned value is relative to the top left of the positioned element.
        /// </summary>
        public int LayerY;

        /// <summary>
        /// Indicates whether the "meta" key was pressed when the event fired.
        /// Note: On the Macintosh, this is the Command key. On Microsoft Windows, this is the Windows key.
        /// </summary>
        public bool MetaKey;

        /// <summary>
        /// Returns the horizontal coordinate of the event relative to whole document.
        /// </summary>
        public int PageX;

        /// <summary>
        /// Returns the vertical coordinate of the event relative to the whole document.
        /// </summary>
        public int PageY;

        /// <summary>
        /// Returns the horizontal coordinate of the event within the screen as a whole.
        /// </summary>
        public int ScreenX;

        /// <summary>
        /// Returns the vertical coordinate of the event within the screen as a whole.
        /// </summary>
        public int ScreenY;

        /// <summary>
        /// Indicates whether the SHIFT key was pressed when the event fired.
        /// </summary>
        public bool ShiftKey;

        /// <summary>
        /// This property of event objects is the object the event was dispatched on. It is different than event.currentTarget when the event handler is called in bubbling or capturing phase of the event.
        /// On IE6-8, the event model is different. Event listeners are attached with the non-standard element.attachEvent method. In this model, the event object is not passed as an argument to the event handler function but is the window.event object. This object has an srcElement property which has the same semantics than event.target.
        /// </summary>
        public Element Target;

        /// <summary>
        /// Returns the time (in milliseconds since the epoch) at which the event was created.
        /// This property only works if the event system supports it for the particular event.
        /// </summary>
        public int TimeStamp;

        /// <summary>
        /// Returns a string containing the type of event.
        /// </summary>
        public string Type;

        /// <summary>
        /// Returns the numeric keyCode of the key pressed, or the character code (charCode) for an alphanumeric key pressed.
        /// </summary>
        public int Which;

        /// <summary>
        /// Cancels the event if it is cancelable, without stopping further propagation of the event.
        /// </summary>
        public void PreventDefault()
        {
        }

        /// <summary>
        /// Prevents other listeners of the same event to be called.
        /// If several listeners are attached to the same element for the same event type, they are called in order in which they have been added. If during one such call, event.stopImmediatePropagation() is called, no remaining listeners will be called.
        /// </summary>
        public void StopImmediatePropagation()
        {
        }

        /// <summary>
        /// Prevents further propagation of the current event.
        /// </summary>
        public void StopPropagation()
        {
        }
    }
}
