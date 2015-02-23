using Bridge.CLR;

namespace System 
{
    [Ignore]
	[Name("Bridge.Int")]
    public struct Int16 : IComparable, IComparable<Int16>, IEquatable<Int16>, IFormattable 
    {
		private Int16(int i) 
        {
		}

		[InlineConst]
        public const short MinValue = -32768;

        [InlineConst]
        public const short MaxValue = 32767;

        [Template("parseInt({s})")]
		public static short Parse(string s) 
        {
			return 0;
		}

        [Template("parseInt({s}, {radix})")]
		public static short Parse(string s, int radix) 
        {
			return 0;
		}

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
		public static bool TryParse(string s, out short result) 
        {
			result = 0;
			return false;
		}

		public string ToString(int radix) 
        {
			return null;
		}

        [Template("Bridge.Int.format({this}, {format})")]
        public string Format(string format)
        {
            return null;
        }

		[Template("Bridge.Int.format({this}, {format})")]
		public string ToString(string format) 
        {
			return null;
		}

		[Template("Bridge.compare({this}, {other})")]
		public int CompareTo(short other) 
        {
			return 0;
		}

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
		public bool Equals(short other) 
        {
			return false;
		}
	}
}
