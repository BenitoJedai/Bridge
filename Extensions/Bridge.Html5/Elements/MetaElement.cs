using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLMetaElement interface contains descriptive metadata about a document. Itt inherits all of the properties and methods described in the HTMLElement interface.
    /// </summary>
    [Ignore]
    [Name("HTMLMetaElement")]
    public class MetaElement : Element
    {
        [Template("document.createElement('meta')")]
        public MetaElement()
        {
        }

        /// <summary>
        /// Gets or sets the value of meta-data property.
        /// </summary>
        public string Content;

        /// <summary>
        /// Gets or sets the name of an HTTP response header to define for a document.
        /// </summary>
        public string HttpEquiv; // TODO: Implement an enum

        /// <summary>
        /// Gets or sets the name of a meta-data property to define for a document.
        /// </summary>
        public string Name;

        /// <summary>
        /// Gets or sets the name of a scheme used to interpret the value of a meta-data property.
        /// </summary>
        public string Scheme; // TODO: Implement an enum?
    }
}