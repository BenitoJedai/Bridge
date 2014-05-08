namespace System
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.DateTime")]
    public struct DateTime
    {
        public static readonly DateTime MaxValue;
        public static readonly DateTime MinValue;

        public DateTime(long ticks) 
        { 
        }

        //public DateTime(long ticks, DateTimeKind kind) { }

        public DateTime(int year, int month, int day)
        {
        }

        public DateTime(int year, int month, int day, int hour, int minute, int second)
        {
        }

        //public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind);

        public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
        }

        //public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind);

        public static TimeSpan operator -(DateTime d1, DateTime d2)
        {
            return new TimeSpan();
        }

        public static DateTime operator -(DateTime d, TimeSpan t)
        {
            return new DateTime();
        }

        public static bool operator !=(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static DateTime operator +(DateTime d, TimeSpan t)
        {
            return new DateTime();
        }

        public static bool operator <(DateTime t1, DateTime t2)
        {
            return false;
        }

        public static bool operator <=(DateTime t1, DateTime t2)
        {
            return false;
        }

        public static bool operator ==(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >(DateTime t1, DateTime t2)
        {
            return false;
        }

        public static bool operator >=(DateTime t1, DateTime t2)
        {
            return false;
        }

        public DateTime Date 
        {
            get
            {
                return new DateTime();
            }
        }

        public int Day 
        {
            get
            {
                return 0;
            }
        }

        public int DayOfWeek 
        {
            get
            {
                return 0;
            }
        }
        public int DayOfYear 
        {
            get
            {
                return 0;
            }
        }

        public int Hour
        {
            get
            {
                return 0;
            }
        }

        //public DateTimeKind Kind { get; }
        public int Millisecond
        {
            get
            {
                return 0;
            }
        }

        public int Minute
        {
            get
            {
                return 0;
            }
        }

        public int Month
        {
            get
            {
                return 0;
            }
        }

        public static DateTime Now 
        {
            get
            {
                return new DateTime();
            }
        }
        public int Second
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

        public TimeSpan TimeOfDay
        {
            get
            {
                return new TimeSpan();
            }
        }

        public static DateTime Today
        {
            get
            {
                return new DateTime();
            }
        }

        public static DateTime UtcNow
        {
            get
            {
                return new DateTime();
            }
        }
        public int Year
        {
            get
            {
                return 0;
            }
        }

        public DateTime Add(TimeSpan value)
        {
            return new DateTime();
        }

        public DateTime AddDays(double value)
        {
            return new DateTime();
        }

        public DateTime AddHours(double value)
        {
            return new DateTime();
        }

        public DateTime AddMilliseconds(double value)
        {
            return new DateTime();
        }

        public DateTime AddMinutes(double value)
        {
            return new DateTime();
        }

        public DateTime AddMonths(int months)
        {
            return new DateTime();
        }

        public DateTime AddSeconds(double value)
        {
            return new DateTime();
        }

        public DateTime AddTicks(long value)
        {
            return new DateTime();
        }

        public DateTime AddYears(int value)
        {
            return new DateTime();
        }

        public static int Compare(DateTime t1, DateTime t2)
        {
            return 0;
        }

        public int CompareTo(DateTime value)
        {
            return 0;
        }

        public int CompareTo(object value)
        {
            return 0;
        }

        public static int DaysInMonth(int year, int month)
        {
            return 0;
        }

        public bool Equals(DateTime value)
        {
            return false;
        }

        public static bool Equals(DateTime t1, DateTime t2)
        {
            return false;
        }

        public static DateTime FromBinary(long dateData)
        {
            return new DateTime();
        }

        public static DateTime FromFileTime(long fileTime)
        {
            return new DateTime();
        }

        public static DateTime FromFileTimeUtc(long fileTime)
        {
            return new DateTime();
        }

        public static DateTime FromOADate(double d)
        {
            return new DateTime();
        }

        public bool IsDaylightSavingTime()
        {
            return false;
        }

        public static bool IsLeapYear(int year)
        {
            return false;
        }

        public static DateTime Parse(string s)
        {
            return new DateTime();
        }

        /*public static DateTime Parse(string s, IFormatProvider provider);
        public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles);
        public static DateTime ParseExact(string s, string format, IFormatProvider provider);
        public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style);
        public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style);
        public static DateTime SpecifyKind(DateTime value, DateTimeKind kind);*/

        public TimeSpan Subtract(DateTime value)
        {
            return new TimeSpan();
        }

        public DateTime Subtract(TimeSpan value)
        {
            return new DateTime();
        }

        public long ToBinary()
        {
            return 0;
        }

        public long ToFileTime()
        {
            return 0;
        }

        public long ToFileTimeUtc()
        {
            return 0;
        }

        public DateTime ToLocalTime()
        {
            return new DateTime();
        }

        public string ToLongDateString()
        {
            return null;
        }

        public string ToLongTimeString()
        {
            return null;
        }

        public double ToOADate()
        {
            return 0;
        }

        public string ToShortDateString()
        {
            return null;
        }

        public string ToShortTimeString()
        {
            return null;
        }

        //public string ToString(IFormatProvider provider);
        public string ToString(string format)
        {
            return "";
        }
        //public string ToString(string format, IFormatProvider provider);
        public DateTime ToUniversalTime()
        {
            return new DateTime();
        }
    }
}
