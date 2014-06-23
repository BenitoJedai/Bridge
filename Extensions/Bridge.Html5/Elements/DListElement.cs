using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLDListElement interface provides special properties (beyond those of the regular HTMLElement interface it also has available to it by inheritance) for manipulating definition list elements.
    /// </summary>
    [Ignore]
    [Name("HTMLDListElement")]
    public class DListElement : Element
    {
        [Template("document.createElement('dl')")]
        public DListElement()
        {
        }

        /// <summary>
        /// Indicates that spacing between list items should be reduced.
        /// </summary>
        public bool Compact;
    }
}