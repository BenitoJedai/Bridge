using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class ForeachBlock : AbstractEmitterBlock
    {
        public ForeachBlock(IEmitter emitter, ForeachStatement foreachStatement)
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
            var awaiters = this.Emitter.IsAsync ? this.GetAwaiters(this.ForeachStatement) : null;

            if (awaiters != null && awaiters.Length > 0)
            {
                this.VisitAsyncForeachStatement();
            }
            else
            {
                this.VisitForeachStatement();
            }
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

        protected void VisitAsyncForeachStatement()
        {
            ForeachStatement foreachStatement = this.ForeachStatement;

            if (foreachStatement.EmbeddedStatement is EmptyStatement)
            {
                return;
            }

            var oldValue = this.Emitter.ReplaceAwaiterByVar;
            var jumpStatements = this.Emitter.JumpStatements;
            this.Emitter.JumpStatements = new List<IJumpInfo>();
            this.WriteAwaiters(foreachStatement.InExpression);

            bool containsAwaits = false;
            var awaiters = this.GetAwaiters(foreachStatement.EmbeddedStatement);

            if (awaiters != null && awaiters.Length > 0)
            {
                containsAwaits = true;
            }

            this.Emitter.ReplaceAwaiterByVar = true;

            if (!containsAwaits)
            {
                this.VisitForeachStatement(oldValue);
                return;
            }

            var iteratorName = this.GetNextIteratorName();
            this.AddLocal(iteratorName, AstType.Null);

            this.WriteVar();
            this.Write(iteratorName, " = ", Bridge.NET.Emitter.ROOT);
            this.WriteDot();
            this.Write(Bridge.NET.Emitter.ENUMERATOR);

            this.WriteOpenParentheses();
            foreachStatement.InExpression.AcceptVisitor(this.Emitter);
            this.Emitter.ReplaceAwaiterByVar = oldValue;
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();
            this.Write("$step = " + this.Emitter.AsyncBlock.Step + ";");
            this.WriteNewLine();
            this.Write("continue;");
            this.WriteNewLine();

            IAsyncStep conditionStep = this.Emitter.AsyncBlock.AddAsyncStep();

            this.WriteIf();
            this.WriteOpenParentheses();
            this.Write(iteratorName);
            this.WriteDot();
            this.Write(Bridge.NET.Emitter.MOVE_NEXT);
            this.WriteOpenCloseParentheses();
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();

            this.WriteVar();
            this.Write(foreachStatement.VariableName, " = ", iteratorName);

            this.WriteDot();
            this.Write(Bridge.NET.Emitter.GET_CURRENT);

            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.PushLocals();
            this.AddLocal(foreachStatement.VariableName, foreachStatement.VariableType);

            BlockStatement block = foreachStatement.EmbeddedStatement as BlockStatement;

            var writer = this.SaveWriter();
            this.Emitter.IgnoreBlock = foreachStatement.EmbeddedStatement;
            var startCount = this.Emitter.AsyncBlock.Steps.Count;

            if (block != null)
            {
                block.AcceptChildren(this.Emitter);
            }
            else
            {
                foreachStatement.EmbeddedStatement.AcceptVisitor(this.Emitter);
            }

            IAsyncStep loopStep = null;
            if (this.Emitter.AsyncBlock.Steps.Count > startCount)
            {
                loopStep = this.Emitter.AsyncBlock.Steps.Last();
                loopStep.JumpToStep = conditionStep.Step;
            }
            this.RestoreWriter(writer);

            if (!AbstractEmitterBlock.IsJumpStatementLast(this.Emitter.Output.ToString()))
            {
                this.Write("$step = " + conditionStep.Step + ";");
                this.WriteNewLine();
                this.Write("continue;");
                this.WriteNewLine();
            }

            this.PopLocals();

            this.WriteNewLine();
            this.EndBlock();
            this.WriteNewLine();

            var nextStep = this.Emitter.AsyncBlock.AddAsyncStep();
            conditionStep.JumpToStep = nextStep.Step;

            if (this.Emitter.JumpStatements.Count > 0)
            {
                foreach (var jump in this.Emitter.JumpStatements)
                {
                    jump.Output.Insert(jump.Position, jump.Break ? nextStep.Step : conditionStep.Step);
                }
            }

            this.Emitter.JumpStatements = jumpStatements;
        }

        protected void VisitForeachStatement(bool? replaceAwaiterByVar = null)
        {
            ForeachStatement foreachStatement = this.ForeachStatement;

            if (foreachStatement.EmbeddedStatement is EmptyStatement)
            {
                return;
            }

            var iteratorName = this.GetNextIteratorName();

            this.WriteVar();
            this.Write(iteratorName, " = ", Bridge.NET.Emitter.ROOT);
            this.WriteDot();
            this.Write(Bridge.NET.Emitter.ENUMERATOR);

            this.WriteOpenParentheses();
            foreachStatement.InExpression.AcceptVisitor(this.Emitter);
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.WriteWhile();
            this.WriteOpenParentheses();
            this.Write(iteratorName);
            this.WriteDot();
            this.Write(Bridge.NET.Emitter.MOVE_NEXT);
            this.WriteOpenCloseParentheses();
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();

            this.WriteVar();
            this.Write(foreachStatement.VariableName, " = ", iteratorName);

            this.WriteDot();
            this.Write(Bridge.NET.Emitter.GET_CURRENT);

            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.PushLocals();
            this.AddLocal(foreachStatement.VariableName, foreachStatement.VariableType);

            BlockStatement block = foreachStatement.EmbeddedStatement as BlockStatement;
            
            if (replaceAwaiterByVar.HasValue)
            {
                this.Emitter.ReplaceAwaiterByVar = replaceAwaiterByVar.Value;
            }

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
