using System.Collections.Generic;

using Bridge.CLR;

namespace Bridge.Html5 
{
    /// <summary>
    /// MediaList representing the intended destination medium for style information.
    /// </summary>
    [Ignore]
    [Name("MediaList")]
	public class MediaList : IEnumerable<string>
    {		
        internal MediaList() 
        {
		}

		public string this[int index] 
        {
			get
            {
				return null;
			}
		}

		public void AppendMedium(string newMedium) 
        {
		}

		public void DeleteMedium(string oldMedium) 
        {
		}		

        [Name("item")]
		public string GetItem(int index) 
        {
			return null;
		}
		
		public readonly int Length;
        		
		public string MediaText;
        
        public IEnumerator<string> GetEnumerator()
        {
            return null;
        }
	}
}
