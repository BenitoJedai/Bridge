using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLLIElement interface expose specific properties and methods (beyond those defined by regular HTMLElement interface it also has available to it by inheritance) for manipulating list elements.
    /// </summary>
    [Ignore]
    [Name("HTMLLIElement")]
    public class LIElement : Element
    {
        [Template("document.createElement('li')")]
        public LIElement()
        {
        }

        /// <summary>
        /// The type of the bullets, "disc", "square" or "circle". As the standard way of defining the list type is via the CSS list-style-type property, use the CSSOM methods to set it via a script.
        /// </summary>
        public string Type;

        /// <summary>
        /// Indicates the ordinal position of the list element inside a given <ol>. It reflects the value attribute of the HTML <li> element, and can be smaller than 0. If the <li> element is not a child of an <ol> element, the property has no meaning.
        /// </summary>
        public new int Value;
    }
}