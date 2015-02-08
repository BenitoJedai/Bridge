using Bridge.Plugin;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class UnaryOperatorBlock : AbstractEmitterBlock
    {
        public UnaryOperatorBlock(IEmitter emitter, UnaryOperatorExpression unaryOperatorExpression)
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
                case UnaryOperatorType.Await:
                    if (this.Emitter.ReplaceAwaiterByVar)
                    {
                        var index = System.Array.IndexOf(this.Emitter.AsyncBlock.AwaitExpressions, unaryOperatorExpression.Expression) + 1;
                        this.Write("$taskResult" + index);
                    }
                    else
                    {
                        var oldValue = this.Emitter.ReplaceAwaiterByVar;
                        var oldAsyncExpressionHandling = this.Emitter.AsyncExpressionHandling;

                        if (this.Emitter.IsAsync && !this.Emitter.AsyncExpressionHandling)
                        {
                            this.WriteAwaiters(unaryOperatorExpression.Expression);
                            this.Emitter.ReplaceAwaiterByVar = true;
                            this.Emitter.AsyncExpressionHandling = true;
                        }   

                        this.WriteAwaiter(unaryOperatorExpression.Expression);

                        this.Emitter.ReplaceAwaiterByVar = oldValue;
                        this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                    }
                    break;
                default:
                    throw (Exception)this.Emitter.CreateException(unaryOperatorExpression, "Unsupported unary operator: " + unaryOperatorExpression.Operator.ToString());
            }
        }           
    }
}
