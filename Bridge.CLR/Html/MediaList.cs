using System.Collections.Generic;

namespace Bridge.CLR.Html 
{
    /// <summary>
    /// MediaList representing the intended destination medium for style information.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("MediaList")]
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

        [Bridge.CLR.Name("item")]
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
