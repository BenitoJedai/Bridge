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
    }
}
