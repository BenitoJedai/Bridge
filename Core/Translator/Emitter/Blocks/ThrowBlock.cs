using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ThrowBlock : AbstractEmitterBlock
    {
        public ThrowBlock(Emitter emitter, ThrowStatement throwStatement)
        {
            this.Emitter = emitter;
            this.ThrowStatement = throwStatement;
        }

        public ThrowStatement ThrowStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.WriteThrow();
            this.ThrowStatement.Expression.AcceptVisitor(this.Emitter);
            this.WriteSemiColon();
            this.WriteNewLine();
        }
    }
}
