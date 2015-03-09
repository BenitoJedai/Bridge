using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Contract
{
    public interface IConstructorBlock
    {
        IEmitter Emitter
        {
            get;
            set;
        }

        ITypeInfo TypeInfo
        {
            get;
            set;
        }

        bool StaticBlock
        {
            get;
            set;
        }
    }
}
