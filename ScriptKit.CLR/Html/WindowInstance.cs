// Window WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/Window

namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The window object represents a window containing a DOM document; the document property points to the DOM document loaded in that window.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    public class WindowInstance
    {
        private WindowInstance()
        {
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
    }
}
