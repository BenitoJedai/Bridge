using System;
using Bridge.Foundation;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTableCellElement interface provides special properties and methods (beyond the regular HTMLElement interface it also has available to it by inheritance) for manipulating the layout and presentation of table cells, either header or data cells, in an HTML document.
    /// The HTML elements implementing this interface: <th> and <td>.
    /// </summary>
    [Ignore]
    [Name("HTMLTableCellElement")]
    public abstract class TableCellElement : Element
    {
        /// <summary>
        /// Is an unsigned long that represents the number of columns this cell must span. It reflects the colspan attribute.
        /// </summary>
        [Name("colspan")]
        public int ColSpan;

        /// <summary>
        /// Is an unsigned long that represents the number of rows this cell must span. It reflects the rowspan attribute.
        /// </summary>
        [Name("rowspan")]
        public string RowSpan;

        /// <summary>
        /// Is a DOMSettableTokenList describing a list of id of <th> elements that represents headers associated with the cell. It reflects the headers attribute.
        /// </summary>
        public readonly DOMSettableTokenList Headers;

        /// <summary>
        /// Is a long representing the cell position in the cells collection of the <tr> it belongs to. If the cell doesn't belong to a <tr>, it returns -1.
        /// </summary>
        public int CellIndex;
    }
}