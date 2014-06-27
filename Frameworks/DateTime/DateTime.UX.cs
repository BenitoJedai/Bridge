using Bridge.CLR;

namespace System
{
    [FileName("DateTime.UX.js")]
    [Name("Bridge.DateTime.UX")]
    public static class Extensions
    {
        public static DateTime Add(this DateTime instance, DateTimeConfig config)
        {
            return instance.AddYears(config.Year)
                    .AddMonths(config.Month)
                    .AddDays(config.Day)
                    .AddHours(config.Hour)
                    .AddMinutes(config.Minute)
                    .AddSeconds(config.Second)
                    .AddMilliseconds(config.Millisecond);
        }

        public static DateTime Set(this DateTime instance, DateTimeConfig config)
        {
            if (config.Year >= 0)
            {
                instance.SetYear(config.Year);
            }

            if (config.Month >= 1 && config.Month < 12)
            {
                instance.SetMonth(config.Month);
            }

            if (config.Day >= 1 && config.Day <= DateTime.DaysInMonth(instance.Year, instance.Month))
            {
                instance.SetDay(config.Day);
            }

            if (config.Hour >= 0 && config.Hour <= 23)
            {
                instance.SetHour(config.Hour);
            }

            if (config.Minute >= 0 && config.Minute <= 59)
            {
                instance.SetMinute(config.Minute);
            }

            if (config.Second >= 0 && config.Second <= 59)
            {
                instance.SetSeconds(config.Second);
            }

            if (config.Millisecond >= 0 && config.Millisecond <= 999)
            {
                instance.SetMilliseconds(config.Millisecond);
            }

            return instance;
        }

        public static DateTime ClearTime(this DateTime instance)
        {
            return instance.Set(new DateTimeConfig {
                Hour = 0,
                Minute = 0,
                Second = 0,
                Millisecond = 0
            });
        }

        public static DateTime ResetTime(this DateTime instance)
        {
            var now = DateTime.Now;

            return instance.Set(new DateTimeConfig { 
                Hour = now.Hour,
                Minute = now.Minute,
                Second = now.Second,
                Millisecond = now.Millisecond
            });
        }

        public static DateTime Clone(this DateTime instance)
        {
            return new DateTime(instance.DateData.GetTime());
        }

        public static DateTime SetYear(this DateTime instance, int year)
        {
            return instance.AddYears(-(instance.Year - year));
        }

        public static DateTime SetMonth(this DateTime instance, int month)
        {
            return instance.AddMonths(-(instance.Month - month));
        }

        public static DateTime SetDay(this DateTime instance, int day)
        {
            return instance.AddDays(-(instance.Day - day));
        }

        public static DateTime SetHour(this DateTime instance, int hour)
        {
            return instance.AddHours(-(instance.Hour - hour));
        }

        public static DateTime SetMinute(this DateTime instance, int minute)
        {
            return instance.AddMinutes(-(instance.Minute - minute));
        }

        public static DateTime SetSeconds(this DateTime instance, int second)
        {
            return instance.AddSeconds(-(instance.Second - second));
        }

        public static DateTime SetMilliseconds(this DateTime instance, int millisecond)
        {
            return instance.AddMilliseconds(-(instance.Millisecond - millisecond));
        }
    }
}