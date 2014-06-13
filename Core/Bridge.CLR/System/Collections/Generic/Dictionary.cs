using Bridge.CLR;

namespace System.Collections.Generic 
{
    [Ignore]
    [Name("Bridge.Dictionary")]
    public sealed class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> 
    {
        public Dictionary() 
        {
        }

        public Dictionary(object obj) 
        {
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return null;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return null;
            }
        }

        public TValue this[TKey key]
        {
            [Template("get({0})")]
            get
            {
                return default(TValue);
            }
            [Template("set({0})")]
            set
            {
            }
        }

        public void Set(TKey key, TValue value)
        {
        }

        public void Add(TKey key, TValue value) 
        {
        }

        public TValue Get(TKey key)
        {
            return default(TValue);
        }

        public void Clear() 
        {
        }

        public bool ContainsKey(TKey key) 
        {
            return false;
        }

        public bool ContainsValue(TValue value)
        {
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() 
        {
            return null;
        }

        public void Remove(TKey key) 
        {
        }        
    }
}
