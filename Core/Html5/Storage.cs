using Bridge.CLR;

namespace Bridge.Html5
{
    [Ignore]
    [Name("Storage")]
    public class Storage
    {
        private Storage()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly int Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key] 
        {
            get {
                return null;
            }
            set {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear() 
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetItem(string key) 
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string Key(int index) 
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void RemoveItem(string key) 
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetItem(string key, object value) 
        {
        } 
    }
}
