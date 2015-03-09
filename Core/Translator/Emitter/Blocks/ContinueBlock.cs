using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ContinueBlock : AbstractEmitterBlock
    {
        public ContinueBlock(IEmitter emitter, ContinueStatement continueStatement)
        {
            this.Emitter = emitter;
            this.ContinueStatement = continueStatement;
        }

        public ContinueStatement ContinueStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            if (this.Emitter.JumpStatements != null)
            {
                this.Write("$step = ");
                this.Emitter.JumpStatements.Add(new JumpInfo(this.Emitter.Output, this.Emitter.Output.Length, false));
                
                this.WriteSemiColon();
                this.WriteNewLine();
            }
            
            this.Write("continue");
            this.WriteSemiColon();
            this.WriteNewLine();
        }
    }
}
