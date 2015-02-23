using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Bridge.Int")]
    public struct Int32 : IComparable, IComparable<Int32>, IEquatable<Int32>, IFormattable 
    {
		private Int32(int i) 
        {
		}

        [InlineConst]
        public const int MinValue = -2147483648;

        [InlineConst]
        public const int MaxValue = 2147483647;

        [Template("parseInt({s})")]
        public static int Parse(string s)
        {
            return 0;
        }

        [Template("parseInt({s}, {radix})")]
        public static int Parse(string s, int radix)
        {
            return 0;
        }

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
        public static bool TryParse(string s, out int result)
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
        public int CompareTo(int other)
        {
            return 0;
        }

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
        public bool Equals(int other)
        {
            return false;
        }
	}
}
