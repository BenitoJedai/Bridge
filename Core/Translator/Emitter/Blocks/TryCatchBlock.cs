using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;

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
            this.VisitTryCatchStatement();
        }

        protected void VisitTryCatchStatement()
        {
            TryCatchStatement tryCatchStatement = this.TryCatchStatement;

            if (tryCatchStatement.CatchClauses.Count > 1)
            {
                throw this.Emitter.CreateException(tryCatchStatement, "Multiple catch statements are not supported");
            }

            this.WriteTry();

            tryCatchStatement.TryBlock.AcceptVisitor(this.Emitter);

            foreach (var clause in tryCatchStatement.CatchClauses)
            {
                this.PushLocals();

                if (!clause.Type.IsNull)
                {
                    if (this.Emitter.ResolveType(clause.Type.ToString()) != "System.Exception")
                    {
                        throw this.Emitter.CreateException(clause, "Only System.Exception type is allowed in catch clauses");
                    }
                }

                var varName = clause.VariableName;

                if (String.IsNullOrEmpty(varName))
                {
                    varName = "$e";
                }
                else
                {
                    this.Emitter.Locals.Add(varName, clause.Type);
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
    }
}
