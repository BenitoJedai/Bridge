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
