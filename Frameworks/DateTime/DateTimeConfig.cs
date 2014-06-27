using Bridge.CLR;

namespace System
{
    [ObjectLiteral]
    public class DateTimeConfig
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }
    }
}