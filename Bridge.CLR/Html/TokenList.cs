using System.Collections.Generic;

namespace Bridge.CLR.Html 
{
    /// <summary>
    /// This type represents a set of space-separated tokens.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("DOMTokenList")]
	public class TokenList : IEnumerable<string>
    {
        protected internal TokenList()
        {
		}

        /// <summary>
        /// Returns an item in the list by its index (or undefined if the number is greater than or equal to the length of the list, prior to Gecko 7.0 returned null)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public string this[int index] 
        {
			get 
            {
				return null;
			}
		}

        /// <summary>
        /// Returns an item in the list by its index (or undefined if the number is greater than or equal to the length of the list, prior to Gecko 7.0 returned null)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string Item(int index)
        {
            return null;
        }

        /// <summary>
        ///  return true if the underlying string contains token, otherwise false
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Contains(string token)
        {
            return false;
        }

        /// <summary>
        /// adds token to the underlying string
        /// </summary>
        /// <param name="token"></param>
		public void Add(string token) 
        {
		}

        /// <summary>
        /// Remove token from the underlying string
        /// </summary>
        /// <param name="token"></param>
		public void Remove(string token) 
        {
		}

        /// <summary>
        /// Removes token from string and returns false. If token doesn't exist it's added and the function returns true
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
		public bool Toggle(string token) 
        {
			return false;
		}

        /// <summary>
        /// Removes token from string and returns false. If token doesn't exist it's added and the function returns true
        /// </summary>
        /// <param name="token"></param>
        /// <param name="force"></param>
        /// <returns></returns>
		public bool Toggle(string token, bool force) 
        {
			return false;
		}

        public readonly int Length;

        public IEnumerator<string> GetEnumerator()
        {
            return null;
        }
	}
}
