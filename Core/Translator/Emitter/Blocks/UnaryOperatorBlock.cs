using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class UnaryOperatorBlock : ConversionBlock
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

        protected override Expression GetExpression()
        {
            return this.UnaryOperatorExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.VisitUnaryOperatorExpression();
        }  

        protected bool ResolveOperator(UnaryOperatorExpression unaryOperatorExpression, OperatorResolveResult orr)
        {
            if (orr != null && orr.UserDefinedOperatorMethod != null)
            {
                var method = orr.UserDefinedOperatorMethod;
                var inline = this.Emitter.GetInline(method);

                if (!string.IsNullOrWhiteSpace(inline))
                {
                    new InlineArgumentsBlock(this.Emitter, new ArgumentsInfo(this.Emitter, unaryOperatorExpression, orr), inline).Emit();
                    return true;
                }
                else
                {
                    if (orr.IsLiftedOperator)
                    {
                        this.Write(Bridge.NET.Emitter.ROOT + ".nullable.lift(");
                    }

                    this.Write(this.Emitter.ShortenTypeName(method.DeclaringType.FullName));
                    this.WriteDot();

                    this.Write(OverloadsCollection.Create(this.Emitter, method).GetOverloadName());

                    if (orr.IsLiftedOperator)
                    {
                        this.WriteComma();
                    }
                    else
                    {
                        this.WriteOpenParentheses();
                    }

                    new ExpressionListBlock(this.Emitter, new Expression[] { unaryOperatorExpression.Expression }, null).Emit();
                    this.WriteCloseParentheses();

                    return true;
                }
            }

            return false;
        }

        protected void VisitUnaryOperatorExpression()
        {
            var unaryOperatorExpression = this.UnaryOperatorExpression;

            var resolveOperator = this.Emitter.Resolver.ResolveNode(unaryOperatorExpression, this.Emitter);
            OperatorResolveResult orr = resolveOperator as OperatorResolveResult;

            if (resolveOperator is ConstantResolveResult)
            {
                this.WriteScript(((ConstantResolveResult)resolveOperator).ConstantValue);
                return;
            }

            if (this.ResolveOperator(unaryOperatorExpression, orr))
            {
                return;
            }

            var op = unaryOperatorExpression.Operator;
            var argResolverResult = this.Emitter.Resolver.ResolveNode(unaryOperatorExpression.Expression, this.Emitter);
            bool nullable = NullableType.IsNullable(argResolverResult.Type);
            if (nullable)
            {
                if (op != UnaryOperatorType.Increment && 
                    op != UnaryOperatorType.Decrement &&
                    op != UnaryOperatorType.PostIncrement &&
                    op != UnaryOperatorType.PostDecrement)
                {
                    this.Write(Bridge.NET.Emitter.ROOT + ".nullable.");
                }                
            }

            switch (op)
            {
                case UnaryOperatorType.BitNot:
                    if (nullable)
                    {
                        this.Write("bnot(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(")");
                    }
                    else
                    {
                        this.Write("~");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }
                    break;
                case UnaryOperatorType.Decrement:
                    if (nullable)
                    {
                        this.Write("(Bridge.hasValue(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(") ? --");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(" : null)");                        
                    }
                    else
                    {
                        this.Write("--");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }
                    break;
                case UnaryOperatorType.Increment:
                    if (nullable)
                    {
                        this.Write("(Bridge.hasValue(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(") ? ++");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(" : null)");                        
                    }
                    else
                    {
                        this.Write("++");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }                    
                    break;
                case UnaryOperatorType.Minus:
                    if (nullable)
                    {
                        this.Write("neg(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(")");
                    }
                    else
                    {
                        this.Write("-");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }                         
                    break;
                case UnaryOperatorType.Not:
                    if (nullable)
                    {
                        this.Write("not(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(")");
                    }
                    else
                    {
                        this.Write("!");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }                            
                    break;
                case UnaryOperatorType.Plus:
                    if (nullable)
                    {
                        this.Write("pos(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(")");
                    }
                    else
                    {
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                    }         
                    
                    break;
                case UnaryOperatorType.PostDecrement:
                    if (nullable)
                    {
                        this.Write("(Bridge.hasValue(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(") ? ");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write("-- : null)");
                    }
                    else
                    {
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write("--");
                    }
                    break;
                case UnaryOperatorType.PostIncrement:
                    if (nullable)
                    {
                        this.Write("(Bridge.hasValue(");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write(") ? ");
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write("++ : null)");
                    }
                    else
                    {                        
                        unaryOperatorExpression.Expression.AcceptVisitor(this.Emitter);
                        this.Write("++");
                    }                   
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
