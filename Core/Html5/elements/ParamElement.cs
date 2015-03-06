using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLParamElement interface provides special properties (beyond those of the regular HTMLElement object interface it inherits) for manipulating <param> elements, representing a pair of a key and a value that acts as a parameter for an <object> element.
    /// </summary>
    [Ignore]
    [Name("HTMLParamElement")]
    public class ParamElement : Element
    {
        [Template("document.createElement('param')")]
        public ParamElement()
        {
        }

        /// <summary>
        /// Is a DOMString representing the name of the parameter. It reflects the name attribute.
        /// </summary>
        public string Name;
    }
}