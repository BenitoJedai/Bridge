using Bridge.CLR;

namespace System.Text.RegularExpressions 
{
	[Ignore]
	[Name("RegExp")]
	public sealed class Regex 
    {
		public Regex(string pattern) 
        {
		}

		public Regex(string pattern, string flags) 
        {
		}

		public int LastIndex 
        {
			get 
            {
				return 0;
			}
			set 
            {
			}
		}

		public bool Global 
        {
            [Name("global")]
			get 
            {
				return false;
			}
		}

		public bool IgnoreCase 
        {
            [Name("ignoreCase")]
			get 
            {
				return false;
			}
		}

		public bool Multiline 
        {
            [Name("multiline")]
			get 
            {
				return false;
			}
		}

		public string Pattern 
        {
            [Name("source")]
			get 
            {
				return null;
			}
		}

		public string Source 
        {
            [Name("source")]
			get 
            {
				return null;
			}
		}

		public RegexMatch Exec(string s) 
        {
			return null;
		}

		public bool Test(string s) 
        {
			return false;
		}

		[Template("Bridge.regexpEscape({s})")]
		public static string Escape(string s) 
        {
			return null;
		}
	}
}
