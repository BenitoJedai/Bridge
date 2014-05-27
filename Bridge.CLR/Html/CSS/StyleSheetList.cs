using System.Collections.Generic;

namespace Bridge.CLR.Html 
{
    /// <summary>
    /// The StyleSheetList interface provides the abstraction of an ordered collection of style sheets.
    /// The items in the StyleSheetList are accessible via an integral index, starting from 0.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("StyleSheetList")]
    public class StyleSheetList : IEnumerable<StyleSheet>
    {
		internal StyleSheetList() 
        {
		}

		public StyleSheet this[int index] 
        {
			get 
            {
				return null;
			}
		}
		
        [Bridge.CLR.Name("item")]
		public StyleSheet GetItem(int index) 
        {
			return null;
		}

		public readonly int Length;

        public IEnumerator<StyleSheet> GetEnumerator()
        {
            return null;
        }
	}
}
