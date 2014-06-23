using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTML <meter> elements expose the HTMLMeterElement interface, which provides special properties and methods (beyond the HTMLElement object interface they also have available to them by inheritance) for manipulating the layout and presentation of <meter> elements.
    /// </summary>
    [Ignore]
    [Name("HTMLMeterElement")]
    public class MeterElement : Element
    {
        [Template("document.createElement('meter')")]
        public MeterElement()
        {
        }

        /// <summary>
        /// It returns the value of the high boundary, reflecting the high <meter> attribute.
        /// </summary>
        public double High;

        /// <summary>
        /// It returns the value of the low boundary, reflecting the low <meter> attribute.
        /// </summary>
        public double Low;

        /// <summary>
        /// It return the maximum value, reflecting the max <meter> attribute.
        /// </summary>
        public double Max;

        /// <summary>
        /// It returns the minimum value, reflecting the min <meter> attribute.
        /// </summary>
        public double Min;

        /// <summary>
        /// It retruns the optimum value, reflecting the min <meter> attribute.
        /// </summary>
        public double Optimum;

        /// <summary>
        /// A list of <label> elements that are labels for this element.
        /// </summary>
        public NodeList Labels;
    }
}