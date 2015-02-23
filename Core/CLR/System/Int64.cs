using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Bridge.Int")]
    public struct Int64 : IComparable, IComparable<Int64>, IEquatable<Int64>, IFormattable 
    {
		private Int64(int i) 
        {
		}

        [InlineConst]
        public const long MinValue = -9007199254740991;

        [InlineConst]
        public const long MaxValue = 9007199254740991;

        [Template("parseInt({s})")]
        public static long Parse(string s)
        {
            return 0;
        }

        [Template("parseInt({s}, {radix})")]
        public static long Parse(string s, int radix)
        {
            return 0;
        }

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
        public static bool TryParse(string s, out long result)
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
        public int CompareTo(long other)
        {
            return 0;
        }

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
        public bool Equals(long other)
        {
            return false;
        }
	}
}
