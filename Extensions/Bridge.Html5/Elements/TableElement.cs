using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTableElement interface provides special properties and methods (beyond the regular HTMLElement object interface it also has available to it by inheritance) for manipulating the layout and presentation of tables in an HTML document.
    /// </summary>
    [Ignore]
    [Name("HTMLTableElement")]
    public class TableElement : Element
    {
        [Template("document.createElement('table')")]
        public TableElement()
        {
        }

        /// <summary>
        /// Is an HTMLTableCaptionElement representing the first <caption> that is a child of the element, or null if none is found. When set, if the object doesn't represent a <caption>, a DOMException with the HierarchyRequestError name is thrown. If a correct object is given, it is inserted in the tree as the first child of this element and the first <caption> that is a child of this element is removed from the tree, if any.
        /// </summary>
        public TableCaptionElement Caption;

        /// <summary>
        /// Is an HTMLTableSectionElement representing the first <thead> that is a child of the element, or null if none is found. When set, if the object doesn't represent a <thead>, a DOMException with the HierarchyRequestError name is thrown. If a correct object is given, it is inserted in the tree immediately before the first element that is neither a <caption>, nor a <colgroup>, or as the last child if there is no such element, and the first <thead> that is a child of this element is removed from the tree, if any.
        /// </summary>
        public TableSectionElement THead;

        /// <summary>
        /// Is an HTMLTableSectionElement representing the first <tfoot> that is a child of the element, or null if none is found. When set, if the object doesn't represent a <tfoot>, a DOMException with the HierarchyRequestError name is thrown. If a correct object is given, it is inserted in the tree immediately before the first element that is neither a <caption>, a <colgroup>, nor a <thead>, or as the last child if there is no such element, and the first <tfoot> that is a child of this element is removed from the tree, if any.
        /// </summary>
        public TableSectionElement TFoot;

        /// <summary>
        /// Returns a live HTMLCollection containing all the rows of the element, that is all <tr> that are a child of the element, or a child or one of its <thead>, <tbody> and <tfoot> children. The rows members of a <thead> appear first, in tree order, and those members of a <tbody> last, also in tree order. The HTMLCollection is live and is automatically updated when the HTMLTableElement changes.
        /// </summary>
        public readonly HTMLCollection<TableRowElement> Rows;

        /// <summary>
        /// Returns a live HTMLCollection containing all the <tbody> of the element. The HTMLCollection is live and is automatically updated when the HTMLTableElement changes.
        /// </summary>
        public readonly HTMLCollection<TableSectionElement> TBodies;

        /// <summary>
        /// Returns an HTMLElement representing the first <thead> that is a child of the element. If none is found, a new one is created and inserted in the tree immediately before the first element that is neither a <caption>, nor a <colgroup>, or as the last child if there is no such element.
        /// </summary>
        public TableSectionElement CreateTHead()
        {
            return null;
        }

        /// <summary>
        /// Removes the first <thead> that is a child of the element.
        /// </summary>
        public void DeleteTHead()
        {
        }

        /// <summary>
        /// Returns an HTMLElement representing the first <tfoot> that is a child of the element. If none is found, a new one is created and inserted in the tree immediately before the first element that is neither a <caption>, a <colgroup>, nor a <thead>, or as the last child if there is no such element.
        /// </summary>
        public TableSectionElement CreateTFoot()
        {
            return null;
        }

        /// <summary>
        /// Removes the first <tfoot> that is a child of the element.
        /// </summary>
        public void DeleteTFoot()
        {
        }

        /// <summary>
        /// Returns an HTMLElement representing the first <caption> that is a child of the element. If none is found, a new one is created and inserted in the tree as the first child of the <table> element.
        /// </summary>
        public TableCaptionElement CreateCaption()
        {
            return null;
        }

        /// <summary>
        /// Removes the first <caption> that is a child of the element.
        /// </summary>
        public void DeleteCaption()
        {
        }

        /// <summary>
        /// Appends a new row in the table. If a table has multiple tbody elements, by default, the new row is inserted into the last tbody. To insert the row into a specific tbody, inserts rows to that body directly.
        /// </summary>
        /// <returns>Returns a TableRowElement representing a new row of the table.</returns>
        public TableRowElement InsertRow() // TODO: index = -1 by default?
        {
            return null;
        }

        /// <summary>
        /// Inserts a new row in the table at the given position.
        /// If index is -1 or equal to the number of rows, the row is appended as the last row. If index is greater than the number of rows, an IndexSizeError exception will result.
        /// If a table has multiple tbody elements, by default, the new row is inserted into the last tbody. To insert the row into a specific tbody, inserts rows to that body directly.
        /// </summary>
        /// <param name="index">The row index of the new row</param>
        /// <returns>Returns a TableRowElement representing a new row of the table.</returns>
        public TableRowElement InsertRow(int index) //TODO: index = -1 by default?
        {
            return null;
        }

        /// <summary>
        /// Removes the row corresponding to the index given in parameter. If the index value is -1 the last row is removed; if it smaller than -1 or greater than the amount of rows in the collection, a DOMException with the value IndexSizeError is raised.
        /// </summary>
        public void DeleteRow(int index)
        {
        }
    }
}