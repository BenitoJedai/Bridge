using System.Collections.Generic;
namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Name("ScriptKit")]
    [ScriptKit.CLR.Ignore]
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

        [ScriptKit.CLR.Inline("delete {0}")]
        public static void Delete(object value)
        {
        }

        [ScriptKit.CLR.Inline("ScriptKit.is({0})")]
        public static bool Is(object type, string typeName)
        {
            return false;
        }
    }
}
