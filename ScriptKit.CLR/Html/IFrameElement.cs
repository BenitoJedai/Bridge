// IFrameElement WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/HTMLIFrameElement

namespace ScriptKit.CLR.Html
{
    [ScriptKit.CLR.Ignore]
    public class IFrameElement: Element
    {
        private IFrameElement()
        {
        }

        /// <summary>
        /// The active document in the inline frame's nested browsing context.
        /// </summary>
        public readonly DocumentInstance ContentDocument;

        /// <summary>
        /// The window proxy for the nested browsing context.
        /// </summary>
        public readonly WindowInstance ContentWindow;

        /// <summary>
        /// Reflects the height HTML attribute, indicating the height of the frame.
        /// </summary>
        public string Height;

        /// <summary>
        /// Reflects the name HTML attribute, containing a name by which to refer to the frame.
        /// </summary>
        public string Name;

        /// <summary>
        /// Reflects the src HTML attribute, containing the address of the content to be embedded.
        /// </summary>
        public string Src;

        /// <summary>
        /// The content to display in the frame.
        /// </summary>
        public string SrcDoc;

        /// <summary>
        /// Reflects the width HTML attribute, indicating the width of the frame.
        /// </summary>
        public string Width;
    }
}
