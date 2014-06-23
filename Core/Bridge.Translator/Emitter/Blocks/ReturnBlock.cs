using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ReturnBlock : AbstractEmitterBlock
    {
        public ReturnBlock(Emitter emitter, ReturnStatement returnStatement)
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
