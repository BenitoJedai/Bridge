using Bridge.CLR;

namespace System.Collections.Generic 
{
    [Ignore]
    [Name("Object")]
    public sealed class KeyValuePair<TKey, TValue> 
    {
        internal KeyValuePair() 
        {
        }

        public TKey Key;
        public TValue Value;
    }
}
