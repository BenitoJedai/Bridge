using Bridge.Foundation;

namespace System.Collections.Generic 
{
    [Ignore]
    [Namespace("Bridge")]
    public sealed class Dictionary<TKey, TValue> : IDictionary<TKey, TValue> 
    {
        public Dictionary() 
        {
        }

        public Dictionary(object obj) 
        {
        }

        public Dictionary(object obj, IEqualityComparer<TKey> comparer)
        {
        }

        [Template("new Bridge.Dictionary$2({TKey}, {TValue})(null, {comparer})")]
        public Dictionary(IEqualityComparer<TKey> comparer)
        {
        }

        public IEqualityComparer<TKey> Comparer 
        { 
            get 
            { 
                return null; 
            } 
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        public bool Remove(TKey key) 
        {
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            return false;
        }
    }
}
