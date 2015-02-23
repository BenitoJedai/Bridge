using Bridge.CLR;

namespace System 
{
    [Ignore]
	[Name("Bridge.Int")]
    public struct SByte : IComparable, IComparable<SByte>, IEquatable<SByte>, IFormattable 
    {
        private SByte(int i) 
        {
		}

		[InlineConst]
        public const sbyte MinValue = -128;

        [InlineConst]
        public const sbyte MaxValue = 127;

        [Template("parseInt({s})")]
		public static sbyte Parse(string s) 
        {
			return 0;
		}

        [Template("parseInt({s}, {radix})")]
		public static sbyte Parse(string s, int radix) 
        {
			return 0;
		}

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
		public static bool TryParse(string s, out sbyte result) 
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
		public int CompareTo(sbyte other) 
        {
			return 0;
		}

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
		public bool Equals(sbyte other) 
        {
			return false;
		}
	}
}
