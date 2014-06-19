using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLDivElement interface provides special properties (beyond the regular HTMLElement interface it also has available to it by inheritance) for manipulating div elements.
    /// </summary>
    [Ignore]
    [Name("HTMLDivElement")]
    public class DivElement : Element
    {
        [Template("document.createElement('div')")]
        public DivElement()
        {
        }

        /// <summary>
        /// Enumerated property indicating alignment of the element's contents with respect to the surrounding context. The possible values are "left", "right", "justify", and "center".
        /// </summary>
        public Align Align;
    }
}