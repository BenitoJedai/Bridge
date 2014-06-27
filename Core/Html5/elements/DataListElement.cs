using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLDataListElement interface provides special properties (beyond the HTMLElement object interface it also has available to it by inheritance) to manipulate <datalist> elements and their content.
    /// </summary>
    [Ignore]
    [Name("HTMLDataListElement")]
    public class DataListElement  : Element
    {
        [Template("document.createElement('datalist')")]
        public DataListElement()
        {
        }

        /// <summary>
        /// A collection of the contained option elements.
        /// </summary>
        public readonly OptionsCollection Options;
    }
}