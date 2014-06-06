using System;

namespace Bridge.CLR.Html
{
    /// <summary>
    /// A CDATA Section can be used within XML to include extended portions of unescaped text, such that the symbols &lt; and & do not need escaping as they normally do within XML when used as text.
    /// </summary>
    [Ignore]
    [Name("CDATASection")]
    public class CDATASection : Text
    {
        internal CDATASection()
        {
        }
    }
}