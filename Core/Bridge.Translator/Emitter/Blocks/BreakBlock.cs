using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class BreakBlock : AbstractEmitterBlock
    {
        public BreakBlock(Emitter emitter, BreakStatement breakStatement)
        {
            this.Emitter = emitter;
            this.BreakStatement = breakStatement;
        }

        public BreakStatement BreakStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.Write("break");
            this.WriteSemiColon();
            this.WriteNewLine();
        }
    }
}
