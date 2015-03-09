using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class BreakBlock : AbstractEmitterBlock
    {
        public BreakBlock(IEmitter emitter, BreakStatement breakStatement)
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
            if (this.Emitter.JumpStatements != null)
            {
                this.Write("$step = ");
                this.Emitter.JumpStatements.Add(new JumpInfo(this.Emitter.Output, this.Emitter.Output.Length, true));

                this.WriteSemiColon();
                this.WriteNewLine();
                this.Write("continue");
            }
            else
            {
                this.Write("break");
            }

            this.WriteSemiColon();
            this.WriteNewLine();
        }
    }
}
