using System;
namespace Bridge.CLR.Html
{
    /// <summary>
    /// Indicate whether or not the element is editable.
    /// </summary>
    [Ignore]
    [Bridge.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
    [Name("String")]
    public enum ContentEditable
    {
        /// <summary>
        /// The empty string, indicates that the element is editable.
        /// </summary>
        True,

        /// <summary>
        /// Indicates that the element cannot be edited.
        /// </summary>
        False,

        /// <summary>
        /// Indicates that the element inherits its parent's editable status.
        /// </summary>
        Inherit
    }
}
