using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// Specifies the horizontal alignment of the content inside an element that supports the align attribute (div, heading).
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNameLowerCase)]
    [Name("String")]
    public enum Align
    {
        /// <summary>
        /// Center-align content
        /// </summary>
        Center,

        /// <summary>
        /// Stretches the lines so that each line has equal width (like in newspapers and magazines)
        /// </summary>
        Justify,

        /// <summary>
        /// Left-align content
        /// </summary>
        Left,

        /// <summary>
        /// Right-align content
        /// </summary>
        Right
    }
}
