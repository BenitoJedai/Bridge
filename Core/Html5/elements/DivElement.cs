using System;
using Bridge.Foundation;

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
    }
}