namespace ScriptKit.CLR.Html
{
    /// <summary>
    /// The display CSS property specifies the type of rendering box used for an element. In HTML, default display property values are taken from behaviors described in the HTML specifications or from the browser/user default stylesheet. The default value in XML is inline.
    /// </summary>
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.EnumEmit(EnumEmit.StringNameLowerCase)]
[ScriptKit.CLR.Name("String")]
    public enum Display
    {
        /// <summary>
        /// Turns off the display of an element (it has no effect on layout); all descendant elements also have their display turned off. The document is rendered as though the element did not exist.
        /// To render an element box's dimensions, yet have its contents be invisible, see the visibility property.
        /// </summary>
        None,

        /// <summary>
        /// The element generates one or more inline element boxes.
        /// </summary>
        Inline, 
        
        /// <summary>
        /// The element generates a block element box.
        /// </summary>
        Block,

        /// <summary>
        /// The element generates a block box for the content and a separate list-item inline box.
        /// </summary>
        [ScriptKit.CLR.Name("list-item")]
        ListItem,

        /// <summary>
        /// The element generates a block element box that will be flowed with surrounding content as if it were a single inline box (behaving much like a replaced element would)
        /// </summary>
        [ScriptKit.CLR.Name("inline-block")]
        InlineBlock,

        /// <summary>
        /// The inline-table value does not have a direct mapping in HTML. It behaves like a <table> HTML element, but as an inline box, rather than a block-level box. Inside the table box is a block-level context.
        /// </summary>
        [ScriptKit.CLR.Name("inline-table")]
        InlineTable,

        /// <summary>
        /// Behaves like the <table> HTML element. It defines a block-level box.
        /// </summary>
        Table,

        /// <summary>
        /// Behaves like the <caption> HTML element.
        /// </summary>
        [ScriptKit.CLR.Name("table-caption")]
        TableCaption,

        /// <summary>
        /// Behaves like the <td> HTML element
        /// </summary>
        [ScriptKit.CLR.Name("table-cell")]
        TableCell,

        /// <summary>
        /// These elements behave like the corresponding <col> HTML elements.
        /// </summary>
        [ScriptKit.CLR.Name("table-column")]
        TableColumn,

        /// <summary>
        /// These elements behave like the corresponding <colgroup> HTML elements.
        /// </summary>
        [ScriptKit.CLR.Name("table-column-group")]
        TableColumnGroup,

        /// <summary>
        /// These elements behave like the corresponding <tfoot> HTML elements
        /// </summary>
        [ScriptKit.CLR.Name("table-footer-group")]
        TableFooterGroup,

        /// <summary>
        /// These elements behave like the corresponding <thead> HTML elements
        /// </summary>
        [ScriptKit.CLR.Name("table-header-group")]
        TableHeaderGroup,
        
        /// <summary>
        /// Behaves like the <tr> HTML element
        /// </summary>
        [ScriptKit.CLR.Name("table-row")]
        TableRow,

        /// <summary>
        /// These elements behave like the corresponding <tbody> HTML elements
        /// </summary>
        [ScriptKit.CLR.Name("table-row-group")]
        TableRowGroup,

        /// <summary>
        /// The element behaves like a block element and lays out its content according to the flexbox model.
        /// </summary>
        Flex,

        /// <summary>
        /// The element behaves like an inline element and lays out its content according to the flexbox model.
        /// </summary>
        [ScriptKit.CLR.Name("inline-flex")]
        InlineFlex,

        /// <summary>
        /// The element behaves like a block element and lay out its content according to the grid model.
        /// </summary>
        Grid,

        /// <summary>
        /// The element behaves like an inline element and lay out its content according to the grid model.
        /// </summary>
        [ScriptKit.CLR.Name("inline-grid")]
        InlineGrid
    }
}
