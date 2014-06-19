using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLSpanElement interface represents a <span> element and derives from the HTMLElement interface, but without implementing any additional properties or methods.
    /// </summary>
    [Ignore]
    [Name("HTMLSpanElement")]
    public class SpanElement : Element
    {
        [Template("document.createElement('span')")]
        public SpanElement()
        {
        }
    }
}