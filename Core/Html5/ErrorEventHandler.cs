using System;

using Bridge;

namespace Bridge.Html5
{
    public delegate bool ErrorEventHandler(string message, string url, int lineNumber, int columnNumber, Exception error);
}
