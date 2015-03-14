using Bridge;

namespace System.Collections.Generic 
{
    [Ignore]
    [Namespace("Bridge")]
    public sealed class KeyValuePair<TKey, TValue> 
    {
        internal KeyValuePair() 
        {
        }

        public TKey Key;
        public TValue Value;
    }
}
