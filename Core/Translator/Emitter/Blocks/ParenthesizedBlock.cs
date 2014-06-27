using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ParenthesizedBlock : AbstractEmitterBlock
    {
        public ParenthesizedBlock(Emitter emitter, ParenthesizedExpression parenthesizedExpression)
        {
            this.Emitter = emitter;
            this.ParenthesizedExpression = parenthesizedExpression;
        }

        public ParenthesizedExpression ParenthesizedExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.WriteOpenParentheses();

            this.ParenthesizedExpression.Expression.AcceptVisitor(this.Emitter);
            this.WriteCloseParentheses();
        }
    }
}
