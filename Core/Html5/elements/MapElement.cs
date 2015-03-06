using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLMapElement interface provides special properties and methods (beyond those of the regular object HTMLElement interface it also has available to it by inheritance) for manipulating the layout and presentation of map elements.
    /// </summary>
    [Ignore]
    [Name("HTMLMapElement")]
    public class MapElement : Element
    {
        [Template("document.createElement('map')")]
        public MapElement()
        {
        }

        /// <summary>
        /// Is a DOMString representing the <map> element for referencing it other context. If the id attribute is set, this must have the same value; and it cannot be null or empty.
        /// </summary>
        public string Name;

        /// <summary>
        /// Is a live HTMLCollection representing the <area> elements associated to this <map>.
        /// </summary>
        public readonly HTMLCollection<AreaElement> Areas;

        /// <summary>
        /// Is a live HTMLCollection representing the <img> and <object> elements associated to this <map>.
        /// </summary>
        public readonly HTMLCollection Images;
    }
}