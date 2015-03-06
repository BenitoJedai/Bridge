using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLPreElement interface expose specific properties and methods (beyond those defined by regular HTMLElement interface it also has available to it by inheritance) for manipulating block of preformatted text.
    /// </summary>
    [Ignore]
    [Name("HTMLPreElement")]
    public class PreElement : Element
    {
        [Template("document.createElement('pre')")]
        public PreElement()
        {
        }
    }
}