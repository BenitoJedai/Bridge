using Bridge.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public class JumpInfo : IJumpInfo
    {
        public JumpInfo(StringBuilder output, int position, bool @break) 
        {
            this.Output = output;
            this.Position = position;
            this.Break = @break;
        }

        public StringBuilder Output
        {
            get;
            set;
        }

        public int Position
        {
            get;
            set;
        }

        public bool Break
        {
            get;
            set;
        }
    }
}
