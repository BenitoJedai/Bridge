using System.Collections;
using Bridge;

namespace System 
{
    [Ignore]
    [Name("Array")]
    public sealed class Array : IEnumerable 
    {
        public readonly int Length = 0;

        private Array() 
        { 
        }

        public object this[int index]
        {
            [Ignore]
            get
            {
                return null;
            }
            [Ignore]
            set
            {
            }
        } 

        public Array Concat(params object[] items) 
        { 
            return null; 
        }

        /// <summary>
        /// The indexOf() method returns the first index at which a given element can be found in the array, or -1 if it is not present.
        /// </summary>
        /// <param name="searchElement"></param>
        /// <returns></returns>
        public int IndexOf(string searchElement)
        {
            return 0;
        }

        /// <summary>
        /// The indexOf() method returns the first index at which a given element can be found in the array, or -1 if it is not present.
        /// </summary>
        /// <param name="searchElement"></param>
        /// <param name="fromIndex"></param>
        /// <returns></returns>
        public int IndexOf(string searchElement, int fromIndex)
        {
            return 0;
        }

        /// <summary>
        /// The lastIndexOf() method returns the last index at which a given element can be found in the array, or -1 if it is not present. The array is searched backwards, starting at fromIndex.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public int LastIndexOf(string searchString)
        {
            return 0;
        }

        /// <summary>
        /// The lastIndexOf() method returns the last index at which a given element can be found in the array, or -1 if it is not present. The array is searched backwards, starting at fromIndex.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="fromIndex"></param>
        /// <returns></returns>
        public int LastIndexOf(string searchString, int fromIndex)
        {
            return 0;
        }


        public string Join(string separator) 
        { 
            return null; 
        }

        public object Pop() 
        { 
            return null; 
        }

        public void Push(params object[] items) 
        {
        }

        public void Reverse() 
        { 
        }

        public object Shift() 
        { 
            return null; 
        }

        public Array Slice(int start) 
        { 
            return null; 
        }

        public Array Slice(int start, int end) 
        { 
            return null; 
        }

        public void Sort() 
        { 
        }

        public void Sort(object compareFunction) 
        { 
        }

        public Array Splice(int start, int deleteCount, params object[] newItems) 
        { 
            return null; 
        }

        public void Unshift(params object[] items) 
        { 
        }

        public IEnumerator GetEnumerator()
        {
            return null;
        }
    }
}