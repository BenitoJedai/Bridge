using Bridge.CLR;

namespace System 
{
	[Ignore]
	[Name("Number")]
	public struct Decimal : IComparable<Decimal>, IEquatable<Decimal>, IFormattable 
    {
		public const decimal MaxValue = 79228162514264337593543950335m;
		public const decimal MinValue = -79228162514264337593543950335m;

		[InlineConst]
		public const decimal Zero = 0;
        [InlineConst]
		public const decimal One = 1;
        [InlineConst]
		public const decimal MinusOne = -1;

		/*[InlineCode("0")]
		private Decimal(DummyTypeUsedToAddAttributeToDefaultValueTypeConstructor _) {
		}*/

		[Template("{d}")]
		public Decimal(double d) 
        {
		}

        [Template("{i}")]
		public Decimal(int i) 
        {
		}

        [Template("{i}")]
		public Decimal(uint i)
        {
		}

        [Template("{f}")]
		public Decimal(float f)
        {
		}

        [Template("{n}")]
		public Decimal(long n)
        {
		}

        [Template("{n}")]
		public Decimal(ulong n)
        {
		}

		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
        {
		}

		[Template("Bridge.formatNumber({this}, {format})")]
		public string Format(string format) 
        {
			return null;
		}

		[Template("Bridge.localeFormatNumber({this}, {format})")]
		public string LocaleFormat(string format) 
        {
			return null;
		}

		public string ToString(int radix) 
        {
			return null;
		}

		[Template("Bridge.formatNumber({this}, {format})")]
		public string ToString(string format) 
        {
			return null;
		}

		/*[Template("Bridge.netFormatNumber({this}, {format}, {provider})")]
		public string ToString(string format, IFormatProvider provider) {
			return null;
		}

		[InlineCode("{$System.Script}.netFormatNumber({this}, 'G', {provider})")]
		public string ToString(IFormatProvider provider) {
			return null;
		}*/

		public string ToExponential() 
        {
			return null;
		}

		public string ToExponential(int fractionDigits) 
        {
			return null;
		}

		public string ToFixed() 
        {
			return null;
		}

		public string ToFixed(int fractionDigits) 
        {
			return null;
		}

		public string ToPrecision() 
        {
			return null;
		}

		public string ToPrecision(int precision) 
        {
			return null;
		}

		public static implicit operator decimal(byte value) 
        {
			return 0;
		}

		public static implicit operator decimal(sbyte value) 
        {
			return 0;
		}

		public static implicit operator decimal(short value) 
        {
			return 0;
		}

		public static implicit operator decimal(ushort value) 
        {
			return 0;
		}

		public static implicit operator decimal(char value) 
        {
			return 0;
		}

		public static implicit operator decimal(int value) 
        {
			return 0;
		}

		public static implicit operator decimal(uint value) 
        {
			return 0;
		}

		public static implicit operator decimal(long value) 
        {
			return 0;
		}

		public static implicit operator decimal(ulong value) 
        {
			return 0;
		}

		public static explicit operator decimal(float value) 
        {
			return 0;
		}

		public static explicit operator decimal(double value) 
        {
			return 0;
		}

		public static explicit operator byte(decimal value) 
        {
		  return 0;
		}

		public static explicit operator sbyte(decimal value) 
        {
		  return 0;
		}

		public static explicit operator char(decimal value) 
        {
			return '\0';
		}

		public static explicit operator short(decimal value) 
        {
			return 0;
		}

		public static explicit operator ushort(decimal value) 
        {
			return 0;
		}

		public static explicit operator int(decimal value) 
        {
			return 0;
		}

		public static explicit operator uint(decimal value) 
        {
			return 0;
		}

		public static explicit operator long(decimal value) 
        {
			return 0;
		}

		public static explicit operator ulong(decimal value) 
        {
			return 0;
		}

		public static explicit operator float(decimal value) 
        {
			return 0;
		}

		public static explicit operator double(decimal value) 
        {
			return 0;
		}

		public static decimal operator +(decimal d) 
        {
			return d;
		}

		public static decimal operator -(decimal d) 
        {
			return d;
		}

		public static decimal operator +(decimal d1, decimal d2) 
        {
			return d1;
		}

		public static decimal operator -(decimal d1, decimal d2) 
        {
			return d1;
		}

		public static decimal operator ++(decimal d) 
        {
			return d;
		}

		public static decimal operator --(decimal d) 
        {
			return d;
		}

		public static decimal operator *(decimal d1, decimal d2) 
        {
			return d1;
		}

		public static decimal operator /(decimal d1, decimal d2) 
        {
			return d1;
		}

		public static decimal operator %(decimal d1, decimal d2) 
        {
			return d1;
		}

		public static bool operator ==(decimal d1, decimal d2) 
        {
			return false;
		}

		public static bool operator !=(decimal d1, decimal d2) 
        {
			return false;
		}

		public static bool operator >(decimal d1, decimal d2) 
        {
			return false;
		}

		public static bool operator >=(decimal d1, decimal d2) 
        {
			return false;
		}

		public static bool operator <(decimal d1, decimal d2) 
        {
			return false;
		}

		public static bool operator <=(decimal d1, decimal d2) 
        {
			return false;
		}

		[Template("{d1} + {d2}")]
		public static decimal Add(decimal d1, decimal d2) 
        {
			return 0;
		}

        [Template("Math.ceil({d})")]
		public static decimal Ceiling(decimal d) 
        {
			return 0;
		}

        [Template("{d1} / {d2}")]
		public static decimal Divide(decimal d1, decimal d2) 
        {
			return 0;
		}

		[Template("Math.floor({d})")]
		public static decimal Floor(decimal d) 
        {
			return 0;
		}

		[Template("{d1} % {d2}")]
		public static decimal Remainder(decimal d1, decimal d2) 
        {
			return 0;
		}

        [Template("{d1} * {d2}")]
		public static decimal Multiply(decimal d1, decimal d2) 
        {
			return 0;
		}

        [Template("-{d}")]
		public static decimal Negate(decimal d) 
        {
			return 0;
		}

        [Template("Math.round({d})")]
		public static decimal Round(decimal d) 
        {
			return 0;
		}

        [Template("{d1} - {d2}")]
		public static decimal Subtract(decimal d1, decimal d2) 
        {
			return 0;
		}


        [Template("Bridge.compare({this}, {other})")]
		public int CompareTo(decimal other) 
        {
			return 0;
		}

        [Template("Bridge.equalsT({this}, {other})")]
		public bool Equals(decimal other) 
        {
			return false;
		}
	}
}
