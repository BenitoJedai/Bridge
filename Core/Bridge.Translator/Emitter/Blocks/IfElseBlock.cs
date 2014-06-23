using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class IfElseBlock : AbstractEmitterBlock
    {
        public IfElseBlock(Emitter emitter, IfElseStatement ifElseStatement)
        {
            this.Emitter = emitter;
            this.IfElseStatement = ifElseStatement;
        }

        public IfElseStatement IfElseStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitIfElseStatement();
        }

        protected void VisitIfElseStatement()
        {
            IfElseStatement ifElseStatement = this.IfElseStatement;
            this.WriteIf();
            this.WriteOpenParentheses();

            ifElseStatement.Condition.AcceptVisitor(this.Emitter);

            this.WriteCloseParentheses();
            this.EmitBlockOrIndentedLine(ifElseStatement.TrueStatement);

            if (ifElseStatement.FalseStatement != null && !ifElseStatement.FalseStatement.IsNull)
            {
                this.WriteElse();
                this.EmitBlockOrIndentedLine(ifElseStatement.FalseStatement);
            }
        }
    }
}
