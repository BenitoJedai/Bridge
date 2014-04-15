namespace System
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Date")]
    public struct DateTime
    {
        public static double operator -(DateTime d1, DateTime d2)
        {
            return 0;
        }

        public static double operator -(DateTime d1, int d2)
        {
            return 0;
        }

        public static double operator -(DateTime d1, double d2)
        {
            return 0;
        }

        public static bool operator <(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator <(DateTime d1, int d2)
        {
            return false;
        }

        public static bool operator <(DateTime d1, double d2)
        {
            return false;
        }

        public static bool operator <(int d1, DateTime d2)
        {
            return false;
        }

        public static bool operator <(double d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >(DateTime d1, int d2)
        {
            return false;
        }

        public static bool operator >(DateTime d1, double d2)
        {
            return false;
        }

        public static bool operator >(int d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >(double d1, DateTime d2)
        {
            return false;
        }

        public static bool operator <=(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator <=(DateTime d1, int d2)
        {
            return false;
        }

        public static bool operator <=(DateTime d1, double d2)
        {
            return false;
        }

        public static bool operator <=(int d1, DateTime d2)
        {
            return false;
        }

        public static bool operator <=(double d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >=(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >=(DateTime d1, int d2)
        {
            return false;
        }

        public static bool operator >=(DateTime d1, double d2)
        {
            return false;
        }

        public static bool operator >=(int d1, DateTime d2)
        {
            return false;
        }

        public static bool operator >=(double d1, DateTime d2)
        {
            return false;
        }

        public static bool operator ==(DateTime d1, object d2)
        {
            return false;
        }

        public static bool operator ==(DateTime d1, DateTime d2)
        {
            return false;
        }

        public static bool operator !=(DateTime d1, object d2)
        {
            return false;
        }

        public static bool operator !=(DateTime d1, DateTime d2)
        {
            return false;
        }

        public DateTime(int year, int month, int date, int hours, int minutes, int seconds, int ms)
        {
        }

        public DateTime(int year, int month, int date, int hours, int minutes, int seconds)
        {
        }

        public DateTime(int year, int month, int date, int hours, int minutes)
        {
        }

        public DateTime(int year, int month, int date, int hours)
        {
        }

        public DateTime(int year, int month, int date)
        {
        }

        public DateTime(int year, int month)
        {
        }

        public static DateTime parse(string value)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month, int date, int hours, int minutes, int seconds, int ms)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month, int date, int hours, int minutes, int seconds)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month, int date, int hours, int minutes)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month, int date, int hours)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month, int date)
        {
            return new DateTime();
        }

        public static DateTime UTC(int year, int month)
        {
            return new DateTime();
        }

        public string toDateString()
        {
            return null;
        }

        public string toTimeString()
        {
            return null;
        }

        public string toLocaleDateString(string value)
        {
            return null;
        }

        public string toLocaleTimeString()
        {
            return null;
        }

        public string toUTCString()
        {
            return null;
        }

        public double getTime()
        {
            return 0;
        }

        public void setTime(double time)
        {
        }

        public int getTimezoneOffset()
        {
            return 0;
        }

        public int getFullYear()
        {
            return 0;
        }

        public int getUTCFullYear()
        {
            return 0;
        }

        public int getMonth()
        {
            return 0;
        }

        public int getUTCMonth()
        {
            return 0;
        }

        public int getDate()
        {
            return 0;
        }

        public int getUTCDate()
        {
            return 0;
        }

        public int getDay()
        {
            return 0;
        }

        public int getUTCDay()
        {
            return 0;
        }

        public int getHours()
        {
            return 0;
        }

        public int getUTCHours()
        {
            return 0;
        }

        public int getMinutes()
        {
            return 0;
        }

        public int getUTCMinutes()
        {
            return 0;
        }

        public int getSeconds()
        {
            return 0;
        }

        public int getUTCSeconds()
        {
            return 0;
        }

        public int getMilliseconds()
        {
            return 0;
        }

        public int getUTCMilliseconds()
        {
            return 0;
        }

        public void setMilliseconds(int ms)
        {
        }

        public void setUTCMilliseconds(int ms)
        {
        }

        public void setSeconds(int sec)
        {
        }

        public void setSeconds(int sec, int ms)
        {
        }

        public void setUTCSeconds(int sec)
        {
        }

        public void setUTCSeconds(int sec, int ms)
        {
        }

        public void setMinutes(int min)
        {
        }

        public void setMinutes(int min, int sec)
        {
        }

        public void setMinutes(int min, int sec, int ms)
        {
        }

        public void setUTCMinutes(int min)
        {
        }

        public void setUTCMinutes(int min, int sec)
        {
        }

        public void setUTCMinutes(int min, int sec, int ms)
        {
        }

        public void setHours(int hour)
        {
        }

        public void setHours(int hour, int min)
        {
        }

        public void setHours(int hour, int min, int sec)
        {
        }

        public void setHours(int hour, int min, int sec, int ms)
        {
        }

        public void setUTCHours(int hour)
        {
        }

        public void setUTCHours(int hour, int min)
        {
        }

        public void setUTCHours(int hour, int min, int sec)
        {
        }

        public void setUTCHours(int hour, int min, int sec, int ms)
        {
        }

        public void setDate(int day)
        {
        }

        public void setUTCDate(int day)
        {
        }

        public void setMonth(int month)
        {
        }

        public void setMonth(int month, int date)
        {
        }

        public void setUTCMonth(int month)
        {
        }

        public void setUTCMonth(int month, int date)
        {
        }

        public void setFullYear(int year)
        {
        }

        public void setFullYear(int year, int month)
        {
        }

        public void setFullYear(int year, int month, int date)
        {
        }

        public void setUTCFullYear(int year)
        {
        }

        public void setUTCFullYear(int year, int month)
        {
        }

        public void setUTCFullYear(int year, int month, int date)
        {
        }        
    }
}