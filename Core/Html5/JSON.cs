// JSON WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/JSON

using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The JSON object contains methods for parsing JavaScript Object Notation (JSON) and converting values to JSON. It can't be called or constructed, and aside from its two method properties it has no interesting functionality of its own.
    /// </summary>
    [Ignore]
    [Name("JSON")]
    public static class JSON
    {
        /// <summary>
        /// The JSON.parse() method parses a string as JSON, optionally transforming the value produced by parsing.
        /// </summary>
        /// <param name="text">The string to parse as JSON. See the JSON object for a description of JSON syntax.</param>
        /// <returns></returns>
        public static object Parse(string text)
        {
            return null;
        }

        /// <summary>
        /// The JSON.parse() method parses a string as JSON, optionally transforming the value produced by parsing.
        /// </summary>
        /// <param name="text">The string to parse as JSON. See the JSON object for a description of JSON syntax.</param>
        /// <returns></returns>
        public static T Parse<T>(string text)
        {
            return default(T);
        }

        /// <summary>
        /// The JSON.parse() method parses a string as JSON, optionally transforming the value produced by parsing.
        /// </summary>
        /// <param name="text">The string to parse as JSON. See the JSON object for a description of JSON syntax.</param>
        /// <param name="reviver">If a function, prescribes how the value originally produced by parsing is transformed, before being returned.</param>
        /// <returns></returns>
        public static object Parse(string text, Func<string, object, object> reviver)
        {
            return null;
        }

        /// <summary>
        /// The JSON.parse() method parses a string as JSON, optionally transforming the value produced by parsing.
        /// </summary>
        /// <param name="text">The string to parse as JSON. See the JSON object for a description of JSON syntax.</param>
        /// <param name="reviver">If a function, prescribes how the value originally produced by parsing is transformed, before being returned.</param>
        /// <returns></returns>
        public static T Parse<T>(string text, Func<string, object, object> reviver)
        {
            return default(T);
        }
    }
}
