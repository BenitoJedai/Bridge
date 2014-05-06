namespace System.Collections.Generic 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Object")]
    public sealed class KeyValuePair<TKey, TValue> 
    {
        internal KeyValuePair() 
        {
        }

        public TKey Key;
        public TValue Value;
    }
}
