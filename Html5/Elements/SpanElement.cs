using System;
using Bridge;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLSpanElement interface represents a &lt;span&gt; element and derives from the HTMLElement interface, but without implementing any additional properties or methods.
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