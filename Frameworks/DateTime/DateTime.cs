using Bridge.CLR;
using Bridge.Html5;

namespace System
{
    [FileName("DateTime.js")]
    [Name("Bridge.DateTime")]
    public struct DateTime
    {
        //public DateTime()
        //{
        //    this.Date = new Date();
        //}

        public DateTime(double time)
        {
            this.DateData = new Date(time);
        }

        public DateTime(int year, int month, int day)
        {
            this.DateData = new Date(year, month, day);
        }

        public Date DateData;

        /// <summary>
        /// Gets the year component of the date represented by this instance.
        /// </summary>
        public int Year
        {
            get
            {
                return this.DateData.GetFullYear();
            }
        }

        /// <summary>
        /// Gets the month component of the date represented by this instance.
        /// </summary>
        public int Month
        {
            get
            {
                return this.DateData.GetMonth() + 1;
            }
        }

        /// <summary>
        /// Gets the day of the month represented by this instance.
        /// </summary>
        public int Day
        {
            get
            {
                return this.DateData.GetDate();
            }
        }

        /// <summary>
        /// Gets the hour component of the date represented by this instance.
        /// </summary>
        public int Hour
        {
            get
            {
                return this.DateData.GetHours();
            }
        }

        /// <summary>
        /// Gets the minute component of the date represented by this instance.
        /// </summary>
        public int Minute
        {
            get
            {
                return this.DateData.GetMinutes();
            }
        }

        /// <summary>
        /// Gets the seconds component of the date represented by this instance.
        /// </summary>
        public int Second
        {
            get
            {
                return this.DateData.GetSeconds();
            }
        }

        /// <summary>
        /// Gets the milliseconds component of the date represented by this instance.
        /// </summary>
        public int Millisecond
        {
            get
            {
                return this.DateData.GetMilliseconds();
            }
        }

        /// <summary>
        /// Gets the date component of this instance.
        /// </summary>
        /// <remarks>A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).</remarks>
        public DateTime Date
        {
            get
            {
                var clone = new DateTime(this.DateData.GetTime());

                clone.DateData.SetHours(0);
                clone.DateData.SetMinutes(0);
                clone.DateData.SetSeconds(0);
                clone.DateData.SetMilliseconds(0);

                return clone;
            }
        }

        /// <summary>
        /// Returns a new DateTime that adds the specified number of years to the value of this instance.
        /// </summary>
        /// <param name="value">A number of years. The value parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by value.</returns>
        public DateTime AddYears(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetFullYear(this.DateData.GetFullYear() + value);
            }

            return this;
        }

        /// <summary>
        /// Returns a new DateTime that adds the specified number of months to the value of this instance.
        /// </summary>
        /// <param name="value">A number of months. The months parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and months.</returns>
        public DateTime AddMonths(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetMonth(this.DateData.GetMonth() + value);
            }

            return this;
        }

        /// <summary>
        /// Returns a new DateTime that adds the specified number of days to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional days. The value parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by value.</returns>
        public DateTime AddDays(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetDate(this.DateData.GetDate() + value);
            }

            return this;
        }
        
        /// <summary>
        /// Returns a new DateTime that adds the specified number of hours to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional hours. The value parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by value.</returns>
        public DateTime AddHours(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetHours(this.DateData.GetHours() + value);
            }

            return this;
        }

        /// <summary>
        /// Returns a new DateTime that adds the specified number of minutes to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional minutes. The value parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by value.</returns>
        public DateTime AddMinutes(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetMinutes(this.DateData.GetMinutes() + value);
            }            

            return this;
        }

        /// <summary>
        /// Returns a new DateTime that adds the specified number of seconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional seconds. The value parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by value.</returns>
        public DateTime AddSeconds(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetSeconds(this.DateData.GetSeconds() + value);
            }

            return this;
        }

        /// <summary>
        /// 	Returns a new DateTime that adds the specified number of milliseconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional milliseconds. The value parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by value.</returns>
        public DateTime AddMilliseconds(int value)
        {
            if (Script.IsDefined(value))
            {
                this.DateData.SetMilliseconds(this.DateData.GetMilliseconds() + value);
            }

            return this;
        }

        /// <summary>
        /// Compares the value of this instance to a specified DateTime value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified DateTime value.
        /// </summary>
        /// <param name="value">The object to compare to the current instance. </param>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
        public int CompareTo(DateTime value)
        {
            return DateTime.Compare(this, value);
        }

        /// <summary>
        /// Returns a value indicating whether the value of this instance is equal to the value of the specified DateTime instance.
        /// </summary>
        /// <param name="value">The object to compare to this instance. </param>
        /// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
        public bool Equals(DateTime value)
        {
            return DateTime.Equals(this, value);
        }

        /// <summary>
        /// Indicates whether this instance of DateTime is within the daylight saving time range for the current time zone.
        /// </summary>
        /// <returns>true if the value of the Kind property is Local or Unspecified and the value of this instance of DateTime is within the daylight saving time range for the local time zone; false if Kind is Utc.</returns>
        public bool IsDaylightSavingTime()
        {
            var temp = DateTime.Today.DateData;
            temp.SetMonth(0);
            temp.SetDate(1);

            return temp.GetTimezoneOffset() != this.DateData.GetTimezoneOffset();
        }

        /// <summary>
        /// Gets the day of the week represented by this instance.
        /// </summary>
        public DayOfWeek DayOfWeek
        {
            get
            {
                return (DayOfWeek)this.DateData.GetDay();
            }
        }

        /// <summary>
        /// The day of the year, expressed as a value between 1 and 366.
        /// </summary>
        public int DayOfYear
        {
            get
            {
                return (int)Math.Floor((this.Date.DateData - new Date(new Date().GetFullYear(), 0, 0, 0, 0, 0, 0)) / 864e5);
            }
        }


        /******************
         *     Statics    *
         * ****************/
        
        public const string VERSION = "2.0.0-beta";

        /// <summary>
        /// Represents the largest possible value of DateTime. This field is read-only.
        /// </summary>
        [Name("MaxValue")]
        public static readonly DateTime MaxValue = new DateTime(123);
        //public static readonly DateTime MaxValue = new DateTime(1).Set(new DateTimeConfig{
        //    Year = 9999,
        //    Month = 12,
        //    Day = 31,
        //    Hour = 23,
        //    Minute = 59,
        //    Second = 59, 
        //    Millisecond = 9999
        //});

        /// <summary>
        /// Represents the smallest possible value of DateTime. This field is read-only.
        /// </summary>
        [Name("MinValue")]
        public static readonly DateTime MinValue = new DateTime(0);

        /// <summary>
        /// Gets the current date.
        /// </summary>
        public static DateTime Today
        {
            get
            {
                return new DateTime(new Date().GetTime()).Date;
            }
        }

        /// <summary>
        /// Gets a DateTime object that is set to the current date and time on this computer, expressed as the local time.
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return new DateTime(new Date().GetTime());
            }
        }

        /// <summary>
        /// Returns an indication whether the specified year is a leap year.
        /// </summary>
        /// <param name="year">A 4-digit year.</param>
        /// <returns>true if year is a leap year; otherwise, false.</returns>
        public static bool IsLeapYear(int year)
        {
            return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0);
        }

        /// <summary>
        /// Compares two instances of DateTime and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.
        /// </summary>
        /// <param name="t1">The first object to compare.</param>
        /// <param name="t2">The second object to compare.</param>
        /// <returns>A signed number indicating the relative values of t1 and t2.</returns>
        public static int Compare(DateTime t1, DateTime t2)
        {
            return t1.Time < t2.Time ? -1 : t1.Time > t2.Time ? 1 : 0;
        }

        public double Time
        {
            get
            {
                return this.DateData.GetTime();
            }
        }

        /// <summary>
        /// Returns a value indicating whether two DateTime instances have the same date and time value.
        /// </summary>
        /// <param name="t1">The first object to compare.</param>
        /// <param name="t2">The second object to compare.</param>
        /// <returns>true if the two values are equal; otherwise, false.</returns>
        public static bool Equals(DateTime t1, DateTime t2)
        {
            return t1.Time == t2.Time;
        }

        /// <summary>
        /// Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month (a number ranging from 1 to 12).</param>
        /// <returns>The number of days in month for the specified year. For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a leap year.</returns>
        public static int DaysInMonth(int year, int month)
        {
            return new int[12] { 31, (DateTime.IsLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }[month - 1];
        }

        /// <summary>
        /// Converts the string representation of a date and time to its DateTime equivalent.
        /// </summary>
        /// <param name="s">A string that contains a date and time to convert. </param>
        /// <returns>An object that is equivalent to the date and time contained in s.</returns>
        public static DateTime Parse(string s)
        {
            return new DateTime(System.Date.Parse(s).GetTime());
        }
    }
}