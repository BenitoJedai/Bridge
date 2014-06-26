using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLOptGroupElement interface provides special properties and methods (beyond the regular HTMLElement object interface they also have available to them by inheritance) for manipulating the layout and presentation of <optgroup> elements.
    /// </summary>
    [Ignore]
    [Name("HTMLOptGroupElement")]
    public class OptGroupElement : Element
    {
        [Template("document.createElement('optgroup')")]
        public OptGroupElement()
        {
        }

        /// <summary>
        /// If true, the whole list of children <option> is disabled.
        /// </summary>
        public bool Disabled;

        /// <summary>
        /// Set or get the label for the group.
        /// </summary>
        public string Label;      
    }
}