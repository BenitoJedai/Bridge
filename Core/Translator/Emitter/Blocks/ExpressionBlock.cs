using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ExpressionBlock : AbstractEmitterBlock
    {
        public ExpressionBlock(Emitter emitter, ExpressionStatement expressionStatement)
        {
            this.Emitter = emitter;
            this.ExpressionStatement = expressionStatement;
        }

        public ExpressionStatement ExpressionStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            if (this.ExpressionStatement.IsNull)
            {
                return;
            }

            this.ExpressionStatement.Expression.AcceptVisitor(this.Emitter);

            if (this.Emitter.EnableSemicolon)
            {
                this.WriteSemiColon(true);
            }
        }        
    }
}
