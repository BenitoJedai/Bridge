using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The URLSearchParams interface defines utility methods to work with the query string of a URL.
    /// </summary>
	[Ignore]
    [Name("URLSearchParams")]
	public partial class URLSearchParams 
    {
		public URLSearchParams() 
        {
		}

        /// <summary>
        /// Constructor returning a URLSearchParams object.
        /// </summary>
        /// <param name="init"></param>
		public URLSearchParams(string init) 
        {
		}

        /// <summary>
        /// Copy constructor returning a URLSearchParams object.
        /// </summary>
        /// <param name="init"></param>
		public URLSearchParams(URLSearchParams init) 
        {
		}

		public void Append(string name, string value) 
        {
		}

		public void Delete(string name) 
        {
		}

		public string Get(string name) 
        {
			return null;
		}

		public string[] GetAll(string name) 
        {
			return null;
		}

		public bool Has(string name) 
        {
			return false;
		}

		public void Set(string name, string value) 
        {
		}
	}
}
