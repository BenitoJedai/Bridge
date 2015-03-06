using Bridge.CLR;

namespace System 
{
	[Ignore]
	[Name("Number")]
    public struct Decimal : IComparable, IComparable<Decimal>, IEquatable<Decimal>, IFormattable 
    {
        [Name("MAX_VALUE")]
        public const decimal MaxValue = 0;
        [Name("MIN_VALUE")]
        public const decimal MinValue = 0;

		[InlineConst]
		public const decimal Zero = 0;
        [InlineConst]
		public const decimal One = 1;
        [InlineConst]
		public const decimal MinusOne = -1;

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

		[Template("Bridge.Int.format({this}, {format})")]
		public string Format(string format) 
        {
			return null;
		}

        [Template("Bridge.Int.format({this}, {format}, {provider})")]
        public string Format(string format, IFormatProvider provider)
        {
            return null;
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

        [Template("Bridge.Int.format({this}, {format}, {provider})")]
        public string ToString(string format, IFormatProvider provider)
        {
            return null;
        }

        [Template("Bridge.Int.parseFloat({s})")]
        public static decimal Parse(string s)
        {
            return 0;
        }

        [Template("Bridge.Int.parseFloat({s}, {provider})")]
        public static decimal Parse(string s, IFormatProvider provider)
        {
            return 0;
        }

        [Template("Bridge.Int.tryParseFloat({s}, null, {result})")]
        public static bool TryParse(string s, out decimal result)
        {
            result = 0;
            return false;
        }

        [Template("Bridge.Int.tryParseFloat({s}, {provider}, {result})")]
        public static bool TryParse(string s, IFormatProvider provider, out decimal result)
        {
            result = 0;
            return false;
        }

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

        [Template("Bridge.compare({this}, {obj})")]
        public int CompareTo(object obj)
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
