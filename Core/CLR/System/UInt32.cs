using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Bridge.Int")]
    public struct UInt32 : IComparable, IComparable<UInt32>, IEquatable<UInt32>, IFormattable 
    {
		private UInt32(int i) 
        {
		}

        [InlineConst]
        public const uint MinValue = 0;

        [InlineConst]
        public const uint MaxValue = 0xFFFFFFFF;

        [Template("parseInt({s})")]
        public static uint Parse(string s)
        {
            return 0;
        }

        [Template("parseInt({s}, {radix})")]
        public static uint Parse(string s, int radix)
        {
            return 0;
        }

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
        public static bool TryParse(string s, out uint result)
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
        public int CompareTo(uint other)
        {
            return 0;
        }

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
        public bool Equals(uint other)
        {
            return false;
        }
	}
}
