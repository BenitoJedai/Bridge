using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class DoWhileBlock : AbstractEmitterBlock
    {
        public DoWhileBlock(Emitter emitter, DoWhileStatement doWhileStatement)
        {
            this.Emitter = emitter;
            this.DoWhileStatement = doWhileStatement;
        }

        public DoWhileStatement DoWhileStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitDoWhileStatement();
        }

        protected void VisitDoWhileStatement()
        {
            DoWhileStatement doWhileStatement = this.DoWhileStatement;

            this.WriteDo();
            this.EmitBlockOrIndentedLine(doWhileStatement.EmbeddedStatement);

            if (doWhileStatement.EmbeddedStatement is BlockStatement)
            {
                this.WriteSpace();
            }

            this.WriteWhile();
            this.WriteOpenCloseParentheses();

            doWhileStatement.Condition.AcceptVisitor(this.Emitter);

            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();

            this.WriteNewLine();
        }
    }
}
