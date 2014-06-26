using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLHeadingElement interface represents the different heading elements.
    /// </summary>
    [Ignore]
    [Name("HTMLHeadingElement")]
    public class HeadingElement : Element
    {
        [Template("document.createElement('h1')")]
        public HeadingElement()
        {
        }

        /// <summary>
        /// Creates a heading element of the specified type
        /// </summary>
        [Template("document.createElement({0})")]
        public HeadingElement(HeadingType h)
        {
        }
    }

    /// <summary>
    /// Specifies the type of heading
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNameLowerCase)]
    [Name("String")]
    public enum HeadingType
    {
        /// <summary>
        /// <h1></h1>
        /// </summary>
        H1,

        /// <summary>
        /// <h2></h2>
        /// </summary>
        H2,

        /// <summary>
        /// <h3></h3>
        /// </summary>
        H3,

        /// <summary>
        /// <h4></h4>
        /// </summary>
        H4,

        /// <summary>
        /// <h5></h5>
        /// </summary>
        H5,

        /// <summary>
        /// <h6></h6>
        /// </summary>
        H6
    }
}