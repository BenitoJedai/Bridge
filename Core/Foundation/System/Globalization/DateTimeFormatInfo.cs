using Bridge.Foundation;

namespace System.Globalization 
{

	[Namespace("Bridge")]
	[Ignore]
	public sealed class DateTimeFormatInfo : IFormatProvider, ICloneable 
    {
		public DateTimeFormatInfo() 
        {
		}

		[FieldProperty]
		public static DateTimeFormatInfo InvariantInfo 
        {
			get 
            {
				return null;
			}
		}

        [FieldProperty]
        public string AMDesignator
        {
            get;
            set;
        }

		[FieldProperty]
		public string PMDesignator 
        {
            get;
            set;
		}

		[FieldProperty]
		public string DateSeparator 
        {
            get;
            set;
		}

		[FieldProperty]
		public string TimeSeparator 
        {
            get;
            set;
		}

		[FieldProperty]
        public string UniversalSortableDateTimePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string SortableDateTimePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string FullDateTimePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string LongDatePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string ShortDatePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string LongTimePattern 
        {
            get;
            set;
		}

		[FieldProperty]
		public string ShortTimePattern 
        {
            get;
            set;
		}

        [FieldProperty]
        public int FirstDayOfWeek
        {
            get;
            set;
        }

		[FieldProperty]
		public string[] DayNames 
        {
			get;
            set;
		}

		[FieldProperty]
        public string[] AbbreviatedDayNames 
        {
            get;
            set;
		}

		[FieldProperty]
        public string[] ShortestDayNames 
        {
            get;
            set;
		}

		[FieldProperty]
		public string[] MonthNames 
        {
            get;
            set;
		}

        [FieldProperty]
        public string[] MonthGenitiveNames
        {
            get;
            set;
        }

		[FieldProperty]
        public string[] AbbreviatedMonthNames 
        {
            get;
            set;
		}

        [FieldProperty]
        public string[] AbbreviatedMonthGenitiveNames
        {
            get;
            set;
        }

        [FieldProperty]
        public string MonthDayPattern
        {
            get;
            set;
        }

        [FieldProperty]
        public string RFC1123Pattern
        {
            get;
            set;
        }

        [FieldProperty]
        public string YearMonthPattern
        {
            get;
            set;
        }

        [FieldProperty]
        public string RoundtripFormat
        {
            get;
            set;
        }

		public object GetFormat(Type formatType) 
        {
			return null;
		}

        public object Clone()
        {
            return null;
        }

        [FieldProperty]
        public static DateTimeFormatInfo CurrentInfo 
        {
            get
            {
                return null;
            }
        }

        public string GetAbbreviatedDayName(DayOfWeek dayofweek)
        {
            return null;
        }

        public string GetAbbreviatedMonthName(int month)
        {
            return null;
        }

        public string[] GetAllDateTimePatterns()
        {
            return null;
        }

        public string[] GetAllDateTimePatterns(string format)
        {
            return null;
        }

        public string GetDayName(DayOfWeek dayofweek)
        {
            return null;
        }

        public string GetMonthName(int month)
        {
            return null;
        }

        public string GetShortestDayName(DayOfWeek dayOfWeek)
        {
            return null;
        }
	}
}
