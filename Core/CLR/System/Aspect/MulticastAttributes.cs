using System;
using Bridge.CLR;

namespace Bridge.Aspects
{    
    [Ignore]
    public class MulticastAttributes
    {
        public const int Default = 0;
        public const int Private = 2;
        public const int Protected = 4;
        public const int Internal = 8;            
        public const int Public = 16;            
        public const int Static = 32;
        public const int Instance = 64;
        public const int Virtual = 128;
        public const int NonVirtual	= 256;                                    
        public const int OutParameter = 512;
        public const int RefParameter = 1024;
    }
}
