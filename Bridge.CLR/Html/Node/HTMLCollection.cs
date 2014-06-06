using System.Collections.Generic;

namespace Bridge.CLR.Html
{
    /// <summary>
    /// HTMLCollection is an class representing a generic collection (array) of elements (in document order) and offers methods and properties for selecting from the list.
    /// </summary>
    [Ignore]
    [Name("HTMLCollection")]
    public class HTMLCollection: IEnumerable<Element>
    {
        protected internal HTMLCollection()
        {
        }

        /// <summary>
        /// Returns the specific node at the given zero-based index into the list. Returns null if the index is out of range.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public Element this[int index] 
        {
			get 
            {
				return null;
			}
		}

        /// <summary>
        /// Returns the specific node whose ID or, as a fallback, name matches the string specified by name. Matching by name is only done as a last resort, only in HTML, and only if the referenced element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Element this[string name]
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the specific node at the given zero-based index into the list. Returns null if the index is out of range.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		[Name("item")]
        public Element GetItem(int index) 
        {
			return null;
		}

        /// <summary>
        /// Returns the specific node whose ID or, as a fallback, name matches the string specified by name. Matching by name is only done as a last resort, only in HTML, and only if the referenced element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Name("namedItem")]
        public Element GetNamedItem(string name)
        {
            return null;
        }

        /// <summary>
        /// The number of items in the collection.
        /// </summary>
        public readonly int Length;

        public IEnumerator<Element> GetEnumerator()
        {
            return null;
        }
    }
}