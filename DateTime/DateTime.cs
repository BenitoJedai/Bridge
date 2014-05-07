using Bridge.CLR;
using Bridge.CLR.Html;
using System;

namespace Js
{
    [Name("DateTime")]
    public class DateTime
    {
        //public DateTime()
        //{
        //    this.Date = new Date();
        //}

        public DateTime(double time)
        {
            this.Date = new Date(time);
        }

        public Date Date;

        /// <summary>
        /// Returns the backing Date object as a Plain Old Date Object.
        /// </summary>
        /// <returns>The Date object</returns>
        public Date ToDate()
        {
            return this.Date;
        }

        /// <summary>
        /// Returns a new DateTime object that is an exact date and time copy of the original instance.
        /// </summary>
        /// <returns>A new DateTime instance</returns>
        public DateTime Clone()
        {
            return new DateTime(this.Date.GetTime());
        }

        /// <summary>
        /// Resets the time of this DateTime object to 12:00 AM (00:00), which is the start of the day.
        /// </summary>
        /// <returns>this</returns>
        public DateTime ClearTime()
        {
            //* @param {Boolean} .clone() this DateTime instance before clearing Time
            //return this.hours(0).minutes(0).seconds(0).milliseconds(0);
            this.Date.SetHours(0);
            this.Date.SetMinutes(0);
            this.Date.SetSeconds(0);
            this.Date.SetMilliseconds(0);

            return this;
        }

        /// <summary>
        /// Resets the time of this DateTime object to the current time ('now').
        /// </summary>
        /// <returns>this</returns>
        public DateTime ResetTime()
        {
            var n = new Date();

            this.Date.SetHours(n.GetHours());
            this.Date.SetMinutes(n.GetMinutes());
            this.Date.SetSeconds(n.GetSeconds());
            this.Date.SetMilliseconds(n.GetMilliseconds());

            // return this.hours(n.getHours()).minutes(n.getMinutes()).seconds(n.getSeconds()).milliseconds(n.getMilliseconds());
            return this;
        }

        /******************
         *     Statics    *
         * ****************/
        
        public const string VERSION = "2.0.0-beta";

        /// <summary>
        /// Represents the largest possible value of DateTime. This field is read-only.
        /// </summary>
        [Name("MaxValue")]
        public static readonly DateTime MaxValue = new DateTime(1234567890123);

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
                return new DateTime(new Date().GetTime()).ClearTime();
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
                return this.ToDate().GetTime();
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
            //return new int [12] { 31, (DateTime.IsLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }[month - 1];
            return 31;
        }

        /// <summary>
        /// Converts the string representation of a date and time to its DateTime equivalent.
        /// </summary>
        /// <param name="s">A string that contains a date and time to convert. </param>
        /// <returns>An object that is equivalent to the date and time contained in s.</returns>
        public static DateTime Parse(string s)
        {
            return new DateTime(Date.Parse(s).GetTime());
        }
    }
}