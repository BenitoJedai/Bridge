using System.Collections.Generic;

namespace Bridge.CLR
{
    [Name("Bridge")]
    [Ignore]
    public static class Script
    {
        public static object Apply(object obj, object values)
        {
            return null;
        }

        public static T Apply<T>(T obj, object values)
        {
            return default(T);
        }

        public static bool IsDefined(object value)
        {
            return false;
        }

        public static bool IsArray(object obj)
        {
            return false;
        }

        public static T[] ToArray<T>(IEnumerable<T> items)
        {
            return null;
        }

        [Template("delete {0}")]
        public static void Delete(object value)
        {
        }

        [Template("Bridge.is({0}, {1})")]
        public static bool Is(object type, string typeName)
        {
            return false;
        }

        /// <summary>
        /// Inject javascript code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        [Bridge.CLR.Template]
        public static T Write<T>(string code)
        {
            return default(T);
        }

        /// <summary>
        /// Inject javascript code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Bridge.CLR.Template]
        public static void Write(string code)
        {
        }

        /// <summary>
        /// An Array-like object corresponding to the arguments passed to a function.
        /// </summary>
        [Template("arguments")]
        public static readonly object[] Arguments;

        [Template("debugger")]
        public static void Debugger()
        {
        }

        [Template("typeof {0}")]
        public static string TypeOf(object obj)
        {
            return null;
        }
    }
}
