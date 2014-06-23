using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ContinueBlock : AbstractEmitterBlock
    {
        public ContinueBlock(Emitter emitter, ContinueStatement continueStatement)
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
            this.Write("continue");
            this.WriteSemiColon();
            this.WriteNewLine();
        }
    }
}
