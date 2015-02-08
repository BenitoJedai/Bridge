using Bridge.CLR;
namespace System 
{
    [Ignore]
	[Name("Bridge.Int")]
    public struct UInt16 : IComparable, IComparable<UInt16>, IEquatable<UInt16>, IFormattable 
    {
		private UInt16(int i) 
        {
		}

		[InlineConst]
		public const ushort MinValue = 0;

        [InlineConst]
		public const ushort MaxValue = 0xFFFF;

		[Script("parseInt({s})")]
		public static ushort Parse(string s) 
        {
			return 0;
		}

        [Script("parseInt({s}, {radix})")]
		public static ushort Parse(string s, int radix) 
        {
			return 0;
		}

        [Template("Bridge.Int.tryParse({s}, {result}, 0, 32768)")]
		public static bool TryParse(string s, out ushort result) 
        {
			result = 0;
			return false;
		}

		public string ToString(int radix) 
        {
			return null;
		}

		[Template("Bridge.Int.format({this}, {format})")]
		public string ToString(string format) 
        {
			return null;
		}

		[Template("Bridge.compare({this}, {other})")]
		public int CompareTo(ushort other) 
        {
			return 0;
		}

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
        {
            return 0;
        }

        [Template("Bridge.equalsT({this}, {other})")]
		public bool Equals(ushort other) 
        {
			return false;
		}
	}
}
