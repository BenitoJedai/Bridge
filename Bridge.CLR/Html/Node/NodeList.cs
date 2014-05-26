using System.Collections.Generic;
namespace Bridge.CLR.Html
{
    /// <summary>
    /// NodeList objects are collections of nodes such as those returned by Node.childNodes and the querySelectorAll method.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("NodeList")]
    public class NodeList: IEnumerable<Node>
    {
        protected internal NodeList()
        {
        }

        /// <summary>
        /// Returns an item in the list by its index, or null if out-of-bounds.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public Node this[int index] 
        {
			get 
            {
				return null;
			}
		}	

        /// <summary>
        /// Returns an item in the list by its index, or null if out-of-bounds. Equivalent to nodeList[idx].
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		[Bridge.CLR.Name("item")]
        public Node GetItem(int index) 
        {
			return null;
		}

        /// <summary>
        /// The number of nodes in the NodeList.
        /// </summary>
        public readonly int Length;

        public IEnumerator<Node> GetEnumerator()
        {
            return null;
        }
    }
}