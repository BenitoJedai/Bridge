using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTableRowElement interface provides special properties and methods (beyond the HTMLElement interface it also has available to it by inheritance) for manipulating the layout and presentation of rows in an HTML table.
    /// </summary>
    [Ignore]
    [Name("HTMLTableRowElement")]
    public class TableRowElement : Element
    {
        [Template("document.createElement('tr')")]
        public TableRowElement()
        {
        }

        /// <summary>
        /// Returns a live HTMLCollection containing the cells in the row. The HTMLCollection is live and is automatically updated when cells are added or removed.
        /// </summary>
        public readonly HTMLCollection<TableDataCellElement> Cells;

        /// <summary>
        /// Returns a long value which gives the logical position of the row within the entire table. If the row is not part of a table, returns -1.
        /// </summary>
        public readonly int RowIndex;

        /// <summary>
        /// Returns a long value which gives the logical position of the row within the table section it belongs to. If the row is not part of a section, returns -1.
        /// </summary>
        public readonly int SectionRowIndex;

        /// <summary>
        /// Removes the cell at the given position in the row. If the given position is greater (or equal as it starts at zero) than the amount of cells in the row, or is smaller than 0, it raises a DOMException with the IndexSizeError value.
        /// <param name="index">The position of the cell in the row</param>
        /// </summary>
        public void DeleteCell(int index)
        {
        }

        /// <summary>
        /// Inserts a new cell into a table row and returns a reference to the cell. The cell is appended as the last cell in the row.
        /// </summary>
        public TableDataCellElement InsertCell()
        {
            return null;   
        }

        /// <summary>
        /// Inserts a new cell into a table row at the specified index and returns a reference to the cell.
        /// If index is -1 or equal to the number of cells. 
        /// If index is greater than the number of cells, an IndexSizeError exception will result.
        /// <param name="index">The cell index of the new cell.</param>
        /// </summary>
        public TableDataCellElement InsertCell(int index)
        {
            return null;   
        }
    }
}