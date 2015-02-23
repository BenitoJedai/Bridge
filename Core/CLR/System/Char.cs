using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Bridge.Int")]
    public struct Char : IComparable, IComparable<Char>, IEquatable<Char>, IFormattable 
    {
		private Char(int i) 
        {
		}

        [InlineConst]
		public const char MinValue = '\0';

        [InlineConst]
		public const char MaxValue = '\xFFFF';

        [Template("Bridge.Int.format({this}, {format})")]
		public string Format(string format) 
        {
			return null;
		}

		[Template("{s}.charCodeAt(0)")]
		public static int Parse(string s) 
        {
			return 0;
		}

		[Template("String.fromCharCode({ch})")]
		public static explicit operator String(char ch) 
        {
			return null;
		}

		[Template("String.fromCharCode({this})")]
		public override string ToString() 
        {
			return null;
		}

        [Template("Bridge.Int.format({this}, {format})")]
		public string ToString(string format) 
        {
			return null;
		}
        
		[Template("Bridge.compare({this}, {other})")]
		public int CompareTo(char other) 
        {
			return 0;
		}

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
		public bool Equals(char other) 
        {
			return false;
		}

        [Template("Bridge.isLower({ch})")]
		public static bool IsLower(char ch) 
        {
			return false;
		}

        [Template("Bridge.isUpper({ch})")]
		public static bool IsUpper(char ch) 
        {
			return false;
		}

        [Template("String.fromCharCode({ch}).toLowerCase().charCodeAt(0)")]
		public static char ToLower(char ch) 
        {
			return (char)0;
		}

        [Template("String.fromCharCode({ch}).toUpperCase().charCodeAt(0)")]
		public static char ToUpper(char ch) 
        {
			return (char)0;
		}

        [Template("({ch} >= 48 && {ch} <= 57)")]
		public static bool IsDigit(char ch) 
        {
			return false;
		}

        [Template("/\\s/.test(String.fromCharCode({ch}))")]
		public static bool IsWhiteSpace(char ch) 
        {
			return false;
		}
	}
}
