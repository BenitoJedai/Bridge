using System.Collections.Generic;
namespace Bridge.CLR.Html
{
    /// <summary>
    /// collection of rectangles that indicate the bounding rectangles for each box in a client.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("ClientRectList")]
    public class ClientRectList: IEnumerable<ClientRect>
    {
        protected internal ClientRectList()
        {
        }

        /// <summary>
        /// Returns an item in the list by its index, or null if out-of-bounds.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public ClientRect this[int index] 
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
        public ClientRect GetItem(int index) 
        {
			return null;
		}

        /// <summary>
        /// The number of items in the ClientRectList.
        /// </summary>
        public readonly int Length;

        public IEnumerator<ClientRect> GetEnumerator()
        {
            return null;
        }
    }
}