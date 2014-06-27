using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTableSectionElement interface provides special properties and methods (beyond the HTMLElement interface it also has available to it by inheritance) for manipulating the layout and presentation of sections, that is headers, footers and bodies, in an HTML table.
    /// The HTML elements implementing this interface: <tfoot>, <thead>, and <tbody>.
    /// </summary>
    [Ignore]
    [Name("HTMLTableSectionElement")]
    public class TableSectionElement : Element
    {
        [Template("document.createElement('tbody')")]
        public TableSectionElement()
        {
        }

        [Template("document.createElement({0})")]
        public TableSectionElement(TableSectionType type)
        {
        }

        /// <summary>
        /// Returns a live HTMLCollection containing the rows in the section. The HTMLCollection is live and is automatically updated when rows are added or removed.
        /// </summary>
        public readonly HTMLCollection<TableRowElement> Rows;

        /// <summary>
        /// Removes the cell at the given position in the section. If the given position is greater (or equal as it starts at zero) than the amount of rows in the section, or is smaller than 0, it raises a DOMException with the IndexSizeError value.
        /// </summary>
        /// <param name="index">The position of the row to delete</param>
        public void DeleteRow(int index)
        {
        }

        /// <summary>
        /// Inserts a new row to the end of section.
        /// </summary>
        /// <returns>Returns a TableRowElement object that represents a new row added to a table element.</returns>
        public TableRowElement InsertRow() // TODO: a possibility to define a parameter with a value by default?
        {
            return null;
        }

        /// <summary>
        /// Inserts a new row just before the given position in the section. 
        /// If the given position is -1, it appends the row to the end of section. 
        /// If the given position is greater (or equal as it starts at zero) than the amount of rows in the section, or is smaller than -1, it raises a DOMException with the IndexSizeError value.
        /// </summary>
        /// <param name="index">The possition of a new row to insert</param>
        /// <returns>Returns a TableRowElement object that represents a new row added to a table element.</returns>
        public TableRowElement InsertRow(int index) // TODO: a possibility to define a parameter with a value by default?
        {
            return null;
        }
    }

    /// <summary>
    /// Specifies the type of a table section. It can be a footer <tfoot>, a header <thead> or a body <tbody> section.
    /// </summary>
    [Ignore]
    [Enum(Emit.StringName)]
    [Name("String")]
    public enum TableSectionType
    {
        /// <summary>
        /// <tbody></tbody>
        /// </summary>
        [Name("tbody")]
        Body,

        /// <summary>
        /// <tfoot></tfoot>
        /// </summary>
        [Name("tfoot")]
        Footer,

        /// <summary>
        /// <thead></thead>
        /// </summary>
        [Name("thead")]
        Header
    }
}