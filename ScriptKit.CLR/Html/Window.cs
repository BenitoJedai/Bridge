// Window WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/Window

using System;
namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The window object represents a window containing a DOM document; the document property points to the DOM document loaded in that window.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("window")]
    public static class Window
    {
        #region Properties

        [ScriptKit.CLR.Name("arguments")]
        public static object[] Arguments;

        /// <summary>
        /// This read-only property indicates whether the referenced window is closed or not.
        /// </summary>
        public static readonly bool Closed;

        /// <summary>
        /// Gets the arguments passed to the window (if it's a dialog box) at the time window.showModalDialog() was called. 
        /// </summary>
        public static readonly object DialogArguments;

        /// <summary>
        /// Returns a reference to the document that the window contains.
        /// </summary>
        public static readonly DocumentInstance Document;

        /// <summary>
        /// Returns the element (such as <iframe> or <object>) in which the window is embedded, or null if the window is top-level.
        /// </summary>
        public static readonly IFrameElement FrameElement;

        /// <summary>
        /// Returns the window itself, which is an array-like object, listing the direct sub-frames of the current window.
        /// </summary>
        public static readonly WindowInstance[] Frames;

        /// <summary>
        /// Returns a reference to the history objectt, which provides an interface for manipulating the browser session history (pages visited in the tab or frame that the current page is loaded in).
        /// </summary>
        public static readonly History History;

        /// <summary>
        /// Height (in pixels) of the browser window viewport including, if rendered, the horizontal scrollbar.
        /// </summary>
        public static readonly int InnerHeight;

        /// <summary>
        /// Width (in pixels) of the browser window viewport including, if rendered, the vertical scrollbar.
        /// </summary>
        public static readonly int InnerWidth;

        /// <summary>
        /// Returns the number of frames in the window.
        /// </summary>
        public static readonly int Length;

        /// <summary>
        /// The Window.location read-only property returns a Location object with information about the current location of the document.
        /// </summary>
        public static readonly Location Location;

        /// <summary>
        /// Returns the locationbar object, whose visibility can be checked.
        /// </summary>
        public static readonly BarProp Locationbar;

        /// <summary>
        /// Returns a reference to the local storage object used to store data that may only be accessed by the origin that created it.
        /// </summary>
        public static readonly Storage LocalStorage;

        /// <summary>
        /// Returns the menubar object, whose visibility can be toggled in the window.
        /// </summary>
        public static readonly BarProp Menubar;

        /// <summary>
        /// Gets/sets the name of the window.
        /// The name of the window is used primarily for setting targets for hyperlinks and forms. Windows do not need to have names.
        /// </summary>
        public static string Name;

        /// <summary>
        /// The Window.navigator read-only property returns a reference to the Navigator object, which can be queried for information about the application running the script.
        /// </summary>
        public static readonly Navigator Navigator;

        /// <summary>
        /// Returns a reference to the window that opened this current window.
        /// </summary>
        public static readonly WindowInstance Opener;

        /// <summary>
        /// Gets the height of the outside of the browser window.
        /// </summary>
        public static readonly int OuterHeight;

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        public static readonly int OuterWidth;

        /// <summary>
        /// An alias for window.scrollX.
        /// </summary>
        public static readonly int PageXOffset;

        /// <summary>
        /// An alias for window.scrollY.
        /// </summary>
        public static readonly int PageYOffset;

        /// <summary>
        /// Returns a reference to the parent of the current window or subframe.
        /// </summary>
        public static readonly WindowInstance Parent;

        /// <summary>
        /// Provides a hosting area for performance related attributes.
        /// </summary>
        public static readonly Performance Performance;

        /// <summary>
        /// The return value to be returned to the function that called window.showModalDialog() to display the window as a modal dialog.
        /// </summary>
        public static readonly object ReturnValue;

        /// <summary>
        /// Returns a reference to the screen object associated with the window.
        /// The screen object is a special object for inspecting properties of the screen on which the current window is being rendered.
        /// </summary>
        public static readonly Screen Screen;

        /// <summary>
        /// Returns the horizontal distance of the left border of the user's browser from the left side of the screen.
        /// </summary>
        public static readonly int ScreenX;

        /// <summary>
        /// Returns the vertical distance of the top border of the user's browser from the top side of the screen.
        /// </summary>
        public static readonly int ScreenY;

        /// <summary>
        /// Returns the scrollbars object, whose visibility can be toggled in the window.
        /// </summary>
        public static readonly BarProp Scrollbars;

        /// <summary>
        /// Returns the number of pixels that the document has already been scrolled horizontally.
        /// </summary>
        public static readonly int ScrollX;

        /// <summary>
        /// Returns the number of pixels that the document has already been scrolled vertically.
        /// </summary>
        public static readonly int ScrollY;

        /// <summary>
        /// Returns an object reference to the window object itself.
        /// </summary>
        public static readonly WindowInstance Self;

        /// <summary>
        /// A storage object for storing data within a single page session.
        /// </summary>
        public static readonly Storage SessionStorage;

        /// <summary>
        /// Gets/sets the text in the statusbar at the bottom of the browser.
        /// </summary>
        public static string Status;

        /// <summary>
        /// Returns the statusbar object, whose visibility can be toggled in the window.
        /// </summary>
        public static readonly BarProp Statusbar;

        /// <summary>
        /// Returns a reference to the topmost window in the window hierarchy. This property is read only.
        /// </summary>
        public static readonly WindowInstance Top;
        
        #endregion Properties

        #region Methods

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(string type, Action listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(EventType type, Action listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(string type, Action<Event> listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(EventType type, Action<Event> listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(string type, Action listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(EventType type, Action listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(string type, Action<Event> listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(EventType type, Action<Event> listener, bool useCapture)
        {
        }

        /// <summary>
        /// The Window.alert() method displays an alert dialog with the optional specified content and an OK button.
        /// </summary>
        /// <param name="message">message is an optional string of text you want to display in the alert dialog, or, alternatively, an object that is converted into a string and displayed.</param>
        public static void Alert(string message)
        {
        }

        /// <summary>
        /// The Window.atob()decodes a string of data which has been encoded using base-64 encoding. You can use the window.btoa() method to encode and transmit data which may otherwise cause communication problems, then transmit it and use the window.atob() method to decode the data again. For example, you can encode, transmit, and decode control characters such as ASCII values 0 through 31.
        /// </summary>
        /// <param name="encodedData">encoded string</param>
        /// <returns></returns>
        public static string Atob(string encodedData)
        {
            return null;
        }

        /// <summary>
        /// Returns the window to the previous item in the history.
        /// </summary>
        public static void Back()
        {
        }

        /// <summary>
        /// Shifts focus away from the window.
        /// </summary>
        public static void Blur()
        {
        }

        /// <summary>
        /// Creates a base-64 encoded ASCII string from a "string" of binary data.
        /// You can use this method to encode data which may otherwise cause communication problems, transmit it, then use the window.atob method to decode the data again. For example, you can encode control characters such as ASCII values 0 through 31.
        /// </summary>
        /// <param name="stringToEncode">String to encode</param>
        /// <returns></returns>
        public static string Btoa(string stringToEncode)
        {
            return null;
        }

        /// <summary>
        /// Cancels an animation frame request previously scheduled through a call to window.requestAnimationFrame().
        /// </summary>
        /// <param name="requestID">The ID value returned by the call to window.requestAnimationFrame() that requested the callback.</param>
        public static void CancelAnimationFrame(int requestID)
        {
        }

        /// <summary>
        /// Cancels repeated action which was set up using setInterval.
        /// </summary>
        /// <param name="intervalID">intervalID is the identifier of the repeated action you want to cancel. This ID is returned from setInterval().</param>
        public static void ClearInterval(int intervalID)
        {
        }

        /// <summary>
        /// Clears the delay set by window.setTimeout().
        /// </summary>
        /// <param name="timeoutID">timeoutID is the ID of the timeout you wish to clear, as returned by window.setTimeout().</param>
        public static void ClearTimeout(int timeoutID)
        {
        }

        /// <summary>
        /// Closes the current window, or a referenced window.
        /// This method is only allowed to be called for windows that were opened by a script using the window.open method. If the window was not opened by a script, the following error appears in the JavaScript Console: Scripts may not close windows that were not opened by script.
        /// </summary>
        public static void Close()
        {
        }

        /// <summary>
        /// The Window.confirm() method displays a modal dialog with an optional message and two buttons, OK and Cancel.
        /// </summary>
        /// <param name="message">message is the optional string to be displayed in the dialog.</param>
        /// <returns>result is a boolean value indicating whether OK or Cancel was selected (true means OK).</returns>
        public static bool Confirm(string message)
        {
            return false;
        }

        /// <summary>
        /// The Window.confirm() method displays a modal dialog with an optional message and two buttons, OK and Cancel.
        /// </summary>
        /// <returns>result is a boolean value indicating whether OK or Cancel was selected (true means OK).</returns>
        public static bool Confirm()
        {
            return false;
        }

        /// <summary>
        /// Dispatches the specified event to the current element.
        /// To create an event object use the createEvent method in Firefox, Opera, Google Chrome, Safari and Internet Explorer from version 9. After the new event is created, initialize it first (for details, see the page for the createEvent method). When the event is initialized, it is ready for dispatching.
        /// </summary>
        /// <param name="event">Required. Reference to an event object to be dispatched.</param>
        /// <returns>Boolean that indicates whether the default action of the event was not canceled.</returns>
        public static bool DispatchEvent(Event @event)
        {
            return false;
        }

        /// <summary>
        /// Prints messages to the (native) console.
        /// </summary>
        /// <param name="message">message is the string message to log.</param>
        public static void Dump(string message)
        {
        }

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
        public static bool Find(string str) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">Boolean value. If true, specifies a case-sensitive search.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">Boolean value. If true, specifies a case-sensitive search.</param>
        /// <param name="backwards">Boolean. If true, specifies a backward search.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive, bool backwards) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">Boolean value. If true, specifies a case-sensitive search.</param>
        /// <param name="backwards">Boolean. If true, specifies a backward search.</param>
        /// <param name="wrapAround">Boolean. If true, specifies a wrap around search.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive, bool backwards, bool wrapAround) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">The text string for which to search.</param>
        /// <param name="backwards">Boolean. If true, specifies a backward search.</param>
        /// <param name="wrapAround">Boolean. If true, specifies a wrap around search.</param>
        /// <param name="wholeWord">Boolean. If true, specifies a whole word search. </param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive, bool backwards, bool wrapAround, bool wholeWord) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">Boolean value. If true, specifies a case-sensitive search.</param>
        /// <param name="backwards">Boolean. If true, specifies a backward search.</param>
        /// <param name="wrapAround">Boolean. If true, specifies a wrap around search.</param>
        /// <param name="wholeWord">Boolean. If true, specifies a whole word search. </param>
        /// <param name="searchInFrames">Boolean. If true, specifies a search in frames.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive, bool backwards, bool wrapAround, bool wholeWord, bool searchInFrames) 
        {
			return false;
		}

        /// <summary>
        /// Finds a string in a window.
        /// </summary>
        /// <param name="str">The text string for which to search.</param>
        /// <param name="caseSensitive">Boolean value. If true, specifies a case-sensitive search.</param>
        /// <param name="backwards">Boolean. If true, specifies a backward search.</param>
        /// <param name="wrapAround">Boolean. If true, specifies a wrap around search.</param>
        /// <param name="wholeWord">Boolean. If true, specifies a whole word search. </param>
        /// <param name="searchInFrames">Boolean. If true, specifies a search in frames.</param>
        /// <param name="showDialog">Boolean. If true, specifies a show Dialog.</param>
        /// <returns>true if the string is found; otherwise, false.</returns>
		public static bool Find(string str, bool caseSensitive, bool backwards, bool wrapAround, bool wholeWord, bool searchInFrames, bool showDialog) 
        {
			return false;
		}

        /// <summary>
        /// Makes a request to bring the window to the front. It may fail due to user settings and the window isn't guaranteed to be frontmost before this method returns.
        /// </summary>
        public static void Focus()
        {
        }

        /// <summary>
        /// Moves the window one document forward in the history.
        /// </summary>
        public static void Forward()
        {
        }

        /// <summary>
        /// The Window.getComputedStyle() method gives the values of all the CSS properties of an element after applying the active stylesheets and resolving any basic computation those values may contain
        /// </summary>
        /// <param name="el">The Element for which to get the computed style.</param>
        /// <returns>The returned style is a CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration GetComputedStyle(Element el)
        {
            return null;
        }

        /// <summary>
        /// The Window.getComputedStyle() method gives the values of all the CSS properties of an element after applying the active stylesheets and resolving any basic computation those values may contain
        /// </summary>
        /// <param name="el">The Element for which to get the computed style.</param>
        /// <param name="pseudoElt">A string specifying the pseudo-element to match. Must be omitted (or null) for regular elements.</param>
        /// <returns>The returned style is a CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration GetComputedStyle(Element el, string pseudoElt)
        {
            return null;
        }

        /// <summary>
        /// getDefaultComputedStyle() gives the default computed values of all the CSS properties of an element, ignoring author styling.  That is, only user-agent and user styles are taken into account.
        /// </summary>
        /// <param name="el">The Element for which to get the computed style.</param>
        /// <returns>The returned style is a CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration GetDefaultComputedStyle(Element el)
        {
            return null;
        }

        /// <summary>
        /// getDefaultComputedStyle() gives the default computed values of all the CSS properties of an element, ignoring author styling.  That is, only user-agent and user styles are taken into account.
        /// </summary>
        /// <param name="el">The Element for which to get the computed style.</param>
        /// <param name="pseudoElt">A string specifying the pseudo-element to match. Must be null (or not specified) for regular elements.</param>
        /// <returns>The returned style is a CSSStyleDeclaration object.</returns>
        public static CSSStyleDeclaration GetDefaultComputedStyle(Element el, string pseudoElt)
        {
            return null;
        }


        [ScriptKit.CLR.Inline("debugger")]
        public static void Debugger()
        {
        }

        [ScriptKit.CLR.Inline("delete {0}")]
        public static void Delete(object value)
        {
        }

        [ScriptKit.CLR.Inline("typeof {0} !== 'undefined'")]
        public static bool IsDefined(object value)
        {
            return false;
        }

        #endregion Methods
    }

    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Window")]
    public class WindowInstance
    {
    }
}
