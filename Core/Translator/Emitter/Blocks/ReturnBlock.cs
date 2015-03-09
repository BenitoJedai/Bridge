using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ReturnBlock : AbstractEmitterBlock
    {
        public ReturnBlock(IEmitter emitter, ReturnStatement returnStatement)
        {
            this.Emitter = emitter;
            this.ReturnStatement = returnStatement;
        }

        public ReturnStatement ReturnStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitReturnStatement();
        }

        protected void VisitReturnStatement()
        {
            ReturnStatement returnStatement = this.ReturnStatement;

            if (this.Emitter.IsAsync)
            {
                if (this.Emitter.AsyncBlock != null && this.Emitter.AsyncBlock.IsTaskReturn)
                {
                    this.WriteAwaiters(returnStatement.Expression);
                    
                    this.Write("$returnTask.setResult(");
                    if (!returnStatement.Expression.IsNull)
                    {
                        var oldValue = this.Emitter.ReplaceAwaiterByVar;
                        this.Emitter.ReplaceAwaiterByVar = true;
                        returnStatement.Expression.AcceptVisitor(this.Emitter);
                        this.Emitter.ReplaceAwaiterByVar = oldValue;
                    }
                    else
                    {
                        this.Write("null");
                    }
                    this.Write(");");

                    this.WriteNewLine();
                }

                this.WriteReturn(false);
                this.WriteSemiColon();
                this.WriteNewLine();
            }
            else
            {
                this.WriteReturn(false);

                if (!returnStatement.Expression.IsNull)
                {
                    this.WriteSpace();
                    returnStatement.Expression.AcceptVisitor(this.Emitter);
                }
                
                this.WriteSemiColon();
                this.WriteNewLine();
            }            
        }
    }
}
