using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class WhileBlock : AbstractEmitterBlock
    {
        public WhileBlock(Emitter emitter, WhileStatement whileStatement)
        {
            this.Emitter = emitter;
            this.WhileStatement = whileStatement;
        }

        public WhileStatement WhileStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.WriteWhile();
            this.WriteOpenParentheses();
            this.WhileStatement.Condition.AcceptVisitor(this.Emitter);
            this.WriteOpenCloseParentheses();
            this.EmitBlockOrIndentedLine(this.WhileStatement.EmbeddedStatement);
        }
    }
}
