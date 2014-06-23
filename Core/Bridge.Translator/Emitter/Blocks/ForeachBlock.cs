using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ForeachBlock : AbstractEmitterBlock
    {
        public ForeachBlock(Emitter emitter, ForeachStatement foreachStatement)
        {
            this.Emitter = emitter;
            this.ForeachStatement = foreachStatement;
        }

        public ForeachStatement ForeachStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitForeachStatement();
        }

        protected virtual string GetNextIteratorName()
        {
            var index = this.Emitter.IteratorCount++;
            var result = "$i";

            if (index > 0)
            {
                result += index;
            }

            return result;
        }

        protected void VisitForeachStatement()
        {
            ForeachStatement foreachStatement = this.ForeachStatement;

            if (foreachStatement.EmbeddedStatement is EmptyStatement)
            {
                return;
            }

            var iteratorName = this.GetNextIteratorName();

            this.WriteVar();
            this.Write(iteratorName, " = ", Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.ENUMERATOR);

            this.WriteOpenParentheses();
            foreachStatement.InExpression.AcceptVisitor(this.Emitter);
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.WriteWhile();
            this.WriteOpenParentheses();
            this.Write(iteratorName);
            this.WriteDot();
            this.Write(Emitter.HAS_NEXT);
            this.WriteOpenCloseParentheses();
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();

            this.WriteVar();
            this.Write(foreachStatement.VariableName, " = ", iteratorName);

            this.WriteDot();
            this.Write(Emitter.NEXT);

            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.PushLocals();
            this.Emitter.Locals.Add(foreachStatement.VariableName, foreachStatement.VariableType);

            BlockStatement block = foreachStatement.EmbeddedStatement as BlockStatement;

            if (block != null)
            {
                block.AcceptChildren(this.Emitter);
            }
            else
            {
                foreachStatement.EmbeddedStatement.AcceptVisitor(this.Emitter);
            }

            this.PopLocals();

            this.EndBlock();
            this.WriteNewLine();
        }
    }
}
