namespace Bridge.CLR.Html
{
    /// <summary>
    /// loading status of the document
    /// </summary>
    [Ignore]
    [Enum(Emit.StringNameLowerCase)]
    [Name("String")]
    public enum ReadyState
    {
        /// <summary>
        /// "loading" while the document is loading
        /// </summary>
        Loading,

        /// <summary>
        /// "interactive" once the document is finished parsing but still loading sub-resources
        /// </summary>
        Interactive,

        /// <summary>
        /// "complete" once the document has loaded
        /// </summary>
        Complete
    }
}
