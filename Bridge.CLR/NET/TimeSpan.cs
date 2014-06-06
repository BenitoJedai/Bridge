using Bridge.CLR;

namespace System
{
    [Ignore]
    [Name("TimeSpan")]
    public struct TimeSpan
    {
        public const long TicksPerDay = 864000000000;
        public const long TicksPerHour = 36000000000;
        public const long TicksPerMillisecond = 10000;
        public const long TicksPerMinute = 600000000;
        public const long TicksPerSecond = 10000000;

        public static readonly TimeSpan MaxValue;
        public static readonly TimeSpan MinValue;
        public static readonly TimeSpan Zero;

        public TimeSpan(long ticks)
        {
        }

        public TimeSpan(int hours, int minutes, int seconds)
        {
        }

        public TimeSpan(int days, int hours, int minutes, int seconds)
        {
        }

        public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
        {
        }

        public static TimeSpan operator -(TimeSpan t)
        {
            return new TimeSpan();
        }

        public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
        {
            return new TimeSpan();
        }

        public static bool operator !=(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static TimeSpan operator +(TimeSpan t)
        {
            return new TimeSpan();
        }

        public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
        {
            return new TimeSpan();
        }

        public static bool operator <(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static bool operator <=(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static bool operator ==(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static bool operator >(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static bool operator >=(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }


        public int Days 
        {
            get
            {
                return 0;
            }
        }

        public int Hours
        {
            get
            {
                return 0;
            }
        }

        public int Milliseconds
        {
            get
            {
                return 0;
            }
        }

        public int Minutes
        {
            get
            {
                return 0;
            }
        }

        public int Seconds
        {
            get
            {
                return 0;
            }
        }

        public long Ticks
        {
            get
            {
                return 0;
            }
        }

        public double TotalDays
        {
            get
            {
                return 0;
            }
        }

        public double TotalHours
        {
            get
            {
                return 0;
            }
        }

        public double TotalMilliseconds
        {
            get
            {
                return 0;
            }
        }

        public double TotalMinutes
        {
            get
            {
                return 0;
            }
        }

        public double TotalSeconds
        {
            get
            {
                return 0;
            }
        }

        public TimeSpan Add(TimeSpan ts)
        {
            return new TimeSpan();
        }

        public static int Compare(TimeSpan t1, TimeSpan t2)
        {
            return 0;
        }

        public int CompareTo(object value)
        {
            return 0;
        }

        public int CompareTo(TimeSpan value)
        {
            return 0;
        }
        public TimeSpan Duration()
        {
            return new TimeSpan();
        }

        public bool Equals(TimeSpan obj)
        {
            return false;
        }

        public static bool Equals(TimeSpan t1, TimeSpan t2)
        {
            return false;
        }

        public static TimeSpan FromDays(double value)
        {
            return new TimeSpan();
        }

        public static TimeSpan FromHours(double value)
        {
            return new TimeSpan();
        }

        public static TimeSpan FromMilliseconds(double value)
        {
            return new TimeSpan();
        }

        public static TimeSpan FromMinutes(double value)
        {
            return new TimeSpan();
        }

        public static TimeSpan FromSeconds(double value)
        {
            return new TimeSpan();
        }

        public static TimeSpan FromTicks(long value)
        {
            return new TimeSpan();
        }

        public TimeSpan Negate()
        {
            return new TimeSpan();
        }

        public static TimeSpan Parse(string s)
        {
            return new TimeSpan();
        }

        /*public static TimeSpan Parse(string input, IFormatProvider formatProvider);
        public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider);
        public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider);
        public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles);
        public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles);*/

        public TimeSpan Subtract(TimeSpan ts)
        {
            return new TimeSpan();
        }

        public string ToString(string format)
        {
            return null;
        }
    }
}
