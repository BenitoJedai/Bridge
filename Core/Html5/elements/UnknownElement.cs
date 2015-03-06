using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLUnknownElement interface represents an invalid HTML element and derives from the HTMLElement interface, but without implementing any additional properties or methods.
    /// Obsolete or non-standard HTML elements implementing this interface: <bgsound>, <blink>, <isindex>, <multicol>, <nextid>, <rb>, <spacer>
    /// </summary>
    [Ignore]
    [Name("HTMLUnknownElement")]
    public class UnknownElement : Element
    {
        [Template("document.createElement({0})")]
        public UnknownElement(string tagName)
        {
        }
    }
}