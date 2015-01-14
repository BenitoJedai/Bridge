using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.NET
{
    [Flags]
    public enum TranslatorMulticastAttributes
    {
        Default = 0,
        Private = 2,
        Protected = 4,
        Internal = 8,            
        Public = 16,            
        Static = 32,
        Instance = 64,
        Virtual	= 128,
        NonVirtual	= 256,                                    
        OutParameter = 512,
        RefParameter = 1024
    }
}
