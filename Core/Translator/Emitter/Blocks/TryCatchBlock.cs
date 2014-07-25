using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class TryCatchBlock : AbstractEmitterBlock
    {
        public TryCatchBlock(Emitter emitter, TryCatchStatement tryCatchStatement)
        {
            this.Emitter = emitter;
            this.TryCatchStatement = tryCatchStatement;
        }

        public TryCatchStatement TryCatchStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            var awaiters = this.Emitter.IsAsync ? this.GetAwaiters(this.TryCatchStatement) : null;

            if (awaiters != null && awaiters.Length > 0)
            {
                this.VisitAsyncTryCatchStatement();
            }
            else
            {
                this.VisitTryCatchStatement();
            }
        }

        protected void VisitAsyncTryCatchStatement()
        {
            TryCatchStatement tryCatchStatement = this.TryCatchStatement;
            this.Validate();

            this.Emitter.AsyncBlock.Steps.Last().JumpToStep = this.Emitter.AsyncBlock.Step;

            var tryStep = this.Emitter.AsyncBlock.AddAsyncStep();
            AsyncTryInfo tryInfo = new AsyncTryInfo();
            tryInfo.StartStep = tryStep.Step;

            this.Emitter.IgnoreBlock = tryCatchStatement.TryBlock;
            tryCatchStatement.TryBlock.AcceptVisitor(this.Emitter);
            tryStep = this.Emitter.AsyncBlock.Steps.Last();
            tryInfo.EndStep = tryStep.Step;

            List<AsyncStep> catchSteps = new List<AsyncStep>();
            foreach (var clause in tryCatchStatement.CatchClauses)
            {
                var catchStep = this.Emitter.AsyncBlock.AddAsyncStep();
                catchSteps.Add(catchStep);

                this.PushLocals();
                var varName = clause.VariableName;

                if (String.IsNullOrEmpty(varName))
                {
                    varName = "$e";                    
                }

                tryInfo.VarName = varName;

                if (!this.Emitter.Locals.ContainsKey(varName))
                {
                    this.AddLocal(varName, clause.Type);
                }

                this.Emitter.IgnoreBlock = clause.Body;
                clause.Body.AcceptVisitor(this.Emitter);
                this.PopLocals();
            }

            if (tryCatchStatement.CatchClauses.Count == 0 && !this.Emitter.Locals.ContainsKey("$e"))
            {
                this.AddLocal("$e", AstType.Null);
            }

            AsyncStep finalyStep = null;
            if (!tryCatchStatement.FinallyBlock.IsNull)
            {
                finalyStep = this.Emitter.AsyncBlock.AddAsyncStep();
                this.Emitter.IgnoreBlock = tryCatchStatement.FinallyBlock;
                tryCatchStatement.FinallyBlock.AcceptVisitor(this.Emitter);

                if (catchSteps.Count == 0)
                {
                    this.WriteNewLine();
                    this.Write("throw $e;");

                    if (!this.Emitter.Locals.ContainsKey("$e"))
                    {
                        this.AddLocal("$e", AstType.Null);
                    }
                }
            }

            var nextStep = this.Emitter.AsyncBlock.AddAsyncStep();
            tryInfo.JumpToStep = catchSteps.Count > 0 ? catchSteps.First().Step : finalyStep.Step;

            if (finalyStep != null)
            {
                finalyStep.JumpToStep = nextStep.Step;
            }

            tryStep.JumpToStep = finalyStep != null ? finalyStep.Step : nextStep.Step;

            foreach (var step in catchSteps)
            {
                step.JumpToStep = finalyStep != null ? finalyStep.Step : nextStep.Step;
            }

            this.Emitter.AsyncBlock.TryInfos.Add(tryInfo);
        }

        protected void VisitTryCatchStatement()
        {
            TryCatchStatement tryCatchStatement = this.TryCatchStatement;

            this.Validate();

            this.WriteTry();

            tryCatchStatement.TryBlock.AcceptVisitor(this.Emitter);

            foreach (var clause in tryCatchStatement.CatchClauses)
            {
                this.PushLocals();

                var varName = clause.VariableName;

                if (String.IsNullOrEmpty(varName))
                {
                    varName = "$e";
                }
                else
                {
                    this.AddLocal(varName, clause.Type);
                }

                this.WriteCatch();
                this.WriteOpenParentheses();
                this.Write(varName);
                this.WriteCloseParentheses();

                clause.Body.AcceptVisitor(this.Emitter);

                this.PopLocals();
            }

            if (!tryCatchStatement.FinallyBlock.IsNull)
            {
                this.WriteFinally();
                tryCatchStatement.FinallyBlock.AcceptVisitor(this.Emitter);
            }
        }

        protected void Validate()
        {
            TryCatchStatement tryCatchStatement = this.TryCatchStatement;

            if (tryCatchStatement.CatchClauses.Count > 1)
            {
                throw this.Emitter.CreateException(tryCatchStatement, "Multiple catch statements are not supported");
            }

            foreach (var clause in tryCatchStatement.CatchClauses)
            {
                if (!clause.Type.IsNull)
                {
                    if (this.Emitter.ResolveType(clause.Type.ToString()) != "System.Exception")
                    {
                        throw this.Emitter.CreateException(clause, "Only System.Exception type is allowed in catch clauses");
                    }
                }
            }
        }
    }

    public class AsyncTryInfo
    {
        public int StartStep
        {
            get;
            set;
        }

        public int EndStep
        {
            get;
            set;
        }

        public int JumpToStep
        {
            get;
            set;
        }

        public string VarName
        {
            get;
            set;
        }
    }
}
