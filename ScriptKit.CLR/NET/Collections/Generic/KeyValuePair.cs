namespace System.Collections.Generic 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Object")]
    public sealed class KeyValuePair<TKey, TValue> 
    {
        internal KeyValuePair() 
        {
        }

        public TKey Key;
        public TValue Value;
    }
}
