using System;

using Bridge.CLR;

namespace Bridge.Html5
{
    public delegate bool ErrorEventHandler(string message, string url, int lineNumber, int columnNumber, Exception error);
}
