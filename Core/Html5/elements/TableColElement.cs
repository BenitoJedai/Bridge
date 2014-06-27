using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLTableColElement interface provides special properties (beyond the HTMLElement interface it also has available to it inheritance) for manipulating single or grouped table column elements. 
    /// The HTML element implementing this interface: <col> and <colgroup>.
    /// </summary>
    [Ignore]
    [Name("HTMLTableColElement")]
    public class TableColElement : Element
    {
        [Template("document.createElement('col')")]
        public TableColElement()
        {
        }

        [Template("document.createElement({0})")]
        public TableColElement(TableColumnType type)
        {
        }

        /// <summary>
        /// Reflects the span HTML attribute, indicating the number of columns to apply this object's attributes to. Must be a positive integer.
        /// </summary>
        public int Span;
    }

    /// <summary>
    /// Specifies the type of a table column. It can be a single <col> or a group <colgroup> column.
    /// </summary>
    [Ignore]
    [Enum(Emit.StringName)]
    [Name("String")]
    public enum TableColumnType
    {
        /// <summary>
        /// <col></col>
        /// </summary>
        [Name("col")]
        Single,

        /// <summary>
        /// <colgroup></colgroup>
        /// </summary>
        [Name("colgroup")]
        Group
    }
}