using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLQuoteElement interface provides special properties and methods (beyond the regular HTMLElement interface it also has available to it by inheritance) for manipulating quoting elements, like <blockquote> and <q>, but not the <cite> element.
    /// </summary>
    [Ignore]
    [Name("HTMLQuoteElement")]
    public class QuoteElement : Element
    {
        [Template("document.createElement('blockquote')")]
        public QuoteElement()
        {
        }

        [Template("document.createElement({0})")]
        public QuoteElement(QuoteType type)
        {
        }

        /// <summary>
        /// Reflects the cite HTML attribute, containing a URL for the source of the quotation.
        /// </summary>
        public string Cite;
    }

    /// <summary>
    /// Specifies the type of quote: <blockquote> or <q>
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNameLowerCase)]
    [Name("String")]
    public enum QuoteType
    {
        /// <summary>
        /// <blockquote></blockquote>
        /// </summary>
        BlockQuote,

        /// <summary>
        /// <q></q>
        /// </summary>
        Q
    }
}