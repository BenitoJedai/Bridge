using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTitleElement interface contains the title for a document. This element inherits all of the properties and methods of the HTMLElement interface.
    /// </summary>
    [Ignore]
    [Name("HTMLTitleElement")]
    public class TitleElement : Element
    {
        [Template("document.createElement('title')")]
        public TitleElement()
        {
        }

        /// <summary>
        /// The string representing the text of the document's title.
        /// </summary>
        public string Text;
    }
}