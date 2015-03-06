using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLLegendElement is an interface allowing to access properties of the <legend> elements. It inherits properties and methods from the HTMLElement interface.
    /// </summary>
    [Ignore]
    [Name("HTMLLegendElement")]
    public class LegendElement : Element
    {
        [Template("document.createElement('legend')")]
        public LegendElement()
        {
        }

        /// <summary>
        /// The form that this legend belongs to. If the legend has a fieldset element as its parent, then this attribute returns the same value as the form attribute on the parent fieldset element. Otherwise, it returns null.
        /// </summary>
        public readonly FormElement Form;
    }
}