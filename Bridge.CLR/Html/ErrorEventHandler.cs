using System;
namespace Bridge.CLR.Html
{
    public delegate bool ErrorEventHandler(string message, string url, int lineNumber, int columnNumber, Exception error);
}
