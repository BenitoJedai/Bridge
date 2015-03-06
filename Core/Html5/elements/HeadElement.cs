using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLHeadElement interface contains the descriptive information, or metadata, for a document. This object inherits all of the properties and methods described in the HTMLElement interface.
    /// </summary>
    [Ignore]
    [Name("HTMLHeadElement")]
    public class HeadElement : Element
    {
        [Template("document.createElement('head')")]
        public HeadElement()
        {
        }
    }
}