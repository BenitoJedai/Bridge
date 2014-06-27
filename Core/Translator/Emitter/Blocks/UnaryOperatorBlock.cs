using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class UnaryOperatorBlock : AbstractEmitterBlock
    {
        public UnaryOperatorBlock(Emitter emitter, UnaryOperatorExpression unaryOperatorExpression)
        {
            this.Emitter = emitter;
            this.UnaryOperatorExpression = unaryOperatorExpression;
        }

        public UnaryOperatorExpression UnaryOperatorExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitUnaryOperatorExpression();
        }

        protected void VisitUnaryOperatorExpression()
        {
            var unaryOperatorExpression = this.UnaryOperatorExpression;
            switch (unaryOperatorExpression.Operator)
            {
                case UnaryOperatorType.BitNot:
                    this.Write("~");
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    break;
                case UnaryOperatorType.Decrement:
                    this.Write("--");
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    break;
                case UnaryOperatorType.Increment:
                    this.Write("++");
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    this.Write("");
                    break;
                case UnaryOperatorType.Minus:
                    this.Write("-");
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    break;
                case UnaryOperatorType.Not:
                    this.Write("!");
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    break;
                case UnaryOperatorType.Plus:
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    break;
                case UnaryOperatorType.PostDecrement:
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    this.Write("--");
                    break;
                case UnaryOperatorType.PostIncrement:
                    unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    this.Write("++");
                    break;
                default:
                    throw this.Emitter.CreateException(unaryOperatorExpression, "Unsupported unary operator: " + unaryOperatorExpression.Operator.ToString());
            }
        }        
    }
}
