using System;
using Bridge;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLBRElement interface represents a HTML line break element.
    /// </summary>
    [Ignore]
    [Name("HTMLBRElement")]
    public class BRElement : Element
    {
        [Template("document.createElement('br')")]
        public BRElement()
        {
        }

        /// <summary>
        /// Indicates flow of text around floating objects.
        /// </summary>
        public string Clear;
    }
}