using System.Collections.Generic;

namespace Bridge.CLR
{
    [Name("Bridge")]
    [Ignore]
    public static class Core
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

        [Inline("delete {0}")]
        public static void Delete(object value)
        {
        }

        [Inline("Bridge.is({0})")]
        public static bool Is(object type, string typeName)
        {
            return false;
        }
    }
}
