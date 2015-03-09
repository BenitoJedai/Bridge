using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ParenthesizedBlock : AbstractEmitterBlock
    {
        public ParenthesizedBlock(IEmitter emitter, ParenthesizedExpression parenthesizedExpression)
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
            var ignoreParentheses = this.IgnoreParentheses(this.ParenthesizedExpression.Expression);

            if (!ignoreParentheses)
            {
                this.WriteOpenParentheses();
            }

            this.ParenthesizedExpression.Expression.AcceptVisitor(this.Emitter);

            if (!ignoreParentheses)
            {
                this.WriteCloseParentheses();
            }
        }

        protected bool IgnoreParentheses(Expression expression)
        {
            if (expression is CastExpression)
            {
                var simpleType = ((CastExpression)expression).Type as SimpleType;

                if (simpleType != null && simpleType.Identifier == "dynamic")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
