using System;

namespace Bridge.CLR.Html
{
    [Ignore]
    [Name("HTMLElement")]
    public class Input : Element
    {
        protected internal Input()
        {
        }

        public bool Autofocus { get; set; }

        public string Value { get; set; }

    }
}