// Window WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/Window

namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The window object represents a window containing a DOM document; the document property points to the DOM document loaded in that window.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("window")]
    public static class Window
    {
        [ScriptKit.CLR.Inline("arguments")]
        public static object[] Arguments()
        {
            return null;
        }

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
        public static readonly IFrameElement IFrameElement;

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




        [ScriptKit.CLR.Inline("debugger")]
        public static void Debugger()
        {
        }

        [ScriptKit.CLR.Inline("delete {0}")]
        public static void Delete(object value)
        {
        }
    }
}
