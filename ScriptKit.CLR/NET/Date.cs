namespace System
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Date")]
    public class Date
    {
        public static double operator -(Date d1, Date d2)
        {
            return 0;
        }

        public static double operator -(Date d1, int d2)
        {
            return 0;
        }

        public static double operator -(Date d1, double d2)
        {
            return 0;
        }

        public static bool operator <(Date d1, Date d2)
        {
            return false;
        }

        public static bool operator <(Date d1, int d2)
        {
            return false;
        }

        public static bool operator <(Date d1, double d2)
        {
            return false;
        }

        public static bool operator <(int d1, Date d2)
        {
            return false;
        }

        public static bool operator <(double d1, Date d2)
        {
            return false;
        }

        public static bool operator >(Date d1, Date d2)
        {
            return false;
        }

        public static bool operator >(Date d1, int d2)
        {
            return false;
        }

        public static bool operator >(Date d1, double d2)
        {
            return false;
        }

        public static bool operator >(int d1, Date d2)
        {
            return false;
        }

        public static bool operator >(double d1, Date d2)
        {
            return false;
        }

        public static bool operator <=(Date d1, Date d2)
        {
            return false;
        }

        public static bool operator <=(Date d1, int d2)
        {
            return false;
        }

        public static bool operator <=(Date d1, double d2)
        {
            return false;
        }

        public static bool operator <=(int d1, Date d2)
        {
            return false;
        }

        public static bool operator <=(double d1, Date d2)
        {
            return false;
        }

        public static bool operator >=(Date d1, Date d2)
        {
            return false;
        }

        public static bool operator >=(Date d1, int d2)
        {
            return false;
        }

        public static bool operator >=(Date d1, double d2)
        {
            return false;
        }

        public static bool operator >=(int d1, Date d2)
        {
            return false;
        }

        public static bool operator >=(double d1, Date d2)
        {
            return false;
        }

        public static bool operator ==(Date d1, object d2)
        {
            return false;
        }

        public static bool operator ==(Date d1, Date d2)
        {
            return false;
        }

        public static bool operator !=(Date d1, object d2)
        {
            return false;
        }

        public static bool operator !=(Date d1, Date d2)
        {
            return false;
        }

        public Date(int year, int month, int date, int hours, int minutes, int seconds, int ms)
        {
        }

        public Date(int year, int month, int date, int hours, int minutes, int seconds)
        {
        }

        public Date(int year, int month, int date, int hours, int minutes)
        {
        }

        public Date(int year, int month, int date, int hours)
        {
        }

        public Date(int year, int month, int date)
        {
        }

        public Date(int year, int month)
        {
        }

        public static Date Parse(string value)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1},{2},{3},{4},{5},{6}")]
        public static Date UTC(int year, int month, int date, int hours, int minutes, int seconds, int ms)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1},{2},{3},{4},{5}")]
        public static Date UTC(int year, int month, int date, int hours, int minutes, int seconds)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1},{2},{3},{4}")]
        public static Date UTC(int year, int month, int date, int hours, int minutes)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1},{2},{3}")]
        public static Date UTC(int year, int month, int date, int hours)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1},{2}")]
        public static Date UTC(int year, int month, int date)
        {
            return null;
        }

        [ScriptKit.CLR.Inline("{0},{1}")]
        public static Date UTC(int year, int month)
        {
            return null;
        }

        public string ToDateString()
        {
            return null;
        }

        public string ToTimeString()
        {
            return null;
        }

        public string ToLocaleDateString(string value)
        {
            return null;
        }

        public string ToLocaleTimeString()
        {
            return null;
        }

        public string ToUTCString()
        {
            return null;
        }

        public double GetTime()
        {
            return 0;
        }

        public void SetTime(double time)
        {
        }

        public int GetTimezoneOffset()
        {
            return 0;
        }

        public int GetFullYear()
        {
            return 0;
        }

        public int GetUTCFullYear()
        {
            return 0;
        }

        public int GetMonth()
        {
            return 0;
        }

        public int GetUTCMonth()
        {
            return 0;
        }

        public int GetDate()
        {
            return 0;
        }

        public int GetUTCDate()
        {
            return 0;
        }

        public int GetDay()
        {
            return 0;
        }

        public int GetUTCDay()
        {
            return 0;
        }

        public int GetHours()
        {
            return 0;
        }

        public int GetUTCHours()
        {
            return 0;
        }

        public int GetMinutes()
        {
            return 0;
        }

        public int GetUTCMinutes()
        {
            return 0;
        }

        public int GetSeconds()
        {
            return 0;
        }

        public int GetUTCSeconds()
        {
            return 0;
        }

        public int GetMilliseconds()
        {
            return 0;
        }

        public int GetUTCMilliseconds()
        {
            return 0;
        }

        public void SetMilliseconds(int ms)
        {
        }

        public void SetUTCMilliseconds(int ms)
        {
        }

        public void SetSeconds(int sec)
        {
        }

        public void SetSeconds(int sec, int ms)
        {
        }

        public void SetUTCSeconds(int sec)
        {
        }

        public void SetUTCSeconds(int sec, int ms)
        {
        }

        public void SetMinutes(int min)
        {
        }

        public void SetMinutes(int min, int sec)
        {
        }

        public void SetMinutes(int min, int sec, int ms)
        {
        }

        public void SetUTCMinutes(int min)
        {
        }

        public void SetUTCMinutes(int min, int sec)
        {
        }

        public void SetUTCMinutes(int min, int sec, int ms)
        {
        }

        public void SetHours(int hour)
        {
        }

        public void SetHours(int hour, int min)
        {
        }

        public void SetHours(int hour, int min, int sec)
        {
        }

        public void SetHours(int hour, int min, int sec, int ms)
        {
        }

        public void SetUTCHours(int hour)
        {
        }

        public void SetUTCHours(int hour, int min)
        {
        }

        public void SetUTCHours(int hour, int min, int sec)
        {
        }

        public void SetUTCHours(int hour, int min, int sec, int ms)
        {
        }

        public void SetDate(int day)
        {
        }

        public void SetUTCDate(int day)
        {
        }

        public void SetMonth(int month)
        {
        }

        public void SetMonth(int month, int date)
        {
        }

        public void SetUTCMonth(int month)
        {
        }

        public void SetUTCMonth(int month, int date)
        {
        }

        public void SetFullYear(int year)
        {
        }

        public void SetFullYear(int year, int month)
        {
        }

        public void SetFullYear(int year, int month, int date)
        {
        }

        public void SetUTCFullYear(int year)
        {
        }

        public void SetUTCFullYear(int year, int month)
        {
        }

        public void SetUTCFullYear(int year, int month, int date)
        {
        }        
    }
}