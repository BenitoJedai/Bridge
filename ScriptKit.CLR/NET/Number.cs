namespace System 
{

    [ScriptKit.CLR.Ignore] 
    [ScriptKit.CLR.Name("Number")]
    [ScriptKit.CLR.Constructor("~~")]
    public struct Int32  
    {
        public const int MAX_VALUE = 0;
        public const int MIN_VALUE = 0;

        public Int32(object value) 
        { 
        }

        public string ToString(int radix) 
        { 
            return null; 
        }
    }

    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Number")]
    [ScriptKit.CLR.Constructor("Number")]
    public struct Double 
    {
        public const double MAX_VALUE = 0;
        public const double MIN_VALUE = 0;
        public const double NaN = 0;
        public const double NEGATIVE_INFINITY = 0;
        public const double POSITIVE_INFINITY = 0;

        public Double(object value) { }

        public string ToString(int radix) 
        { 
            return null; 
        }

        public string ToFixed(int fractionDigits) 
        { 
            return null; 
        }

        public string ToExponential(int fractionDigits) 
        { 
            return null; 
        }

        public string ToPrecision(int precision) 
        { 
            return null; 
        }
    }
}