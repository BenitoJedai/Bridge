using Bridge.Plugin;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class BinaryOperatorBlock : AbstractEmitterBlock
    {
        public BinaryOperatorBlock(IEmitter emitter, BinaryOperatorExpression binaryOperatorExpression)
        {
            this.Emitter = emitter;
            this.BinaryOperatorExpression = binaryOperatorExpression;
        }

        public BinaryOperatorExpression BinaryOperatorExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitBinaryOperatorExpression();
        }

        protected bool ResolveOperator(BinaryOperatorExpression binaryOperatorExpression)
        {
            var resolveOperator = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression, this.Emitter);

            if (resolveOperator != null && resolveOperator is OperatorResolveResult)
            {
                var orr = (OperatorResolveResult)resolveOperator;

                if (orr.UserDefinedOperatorMethod != null)
                {
                    var inline = this.Emitter.GetInline(orr.UserDefinedOperatorMethod);

                    if (!string.IsNullOrWhiteSpace(inline))
                    {
                        new InlineArgumentsBlock(this.Emitter, new ArgumentsInfo(this.Emitter, binaryOperatorExpression, orr), inline).Emit();
                        return true;
                    }
                }
            }

            return false;
        }

        protected void VisitBinaryOperatorExpression()
        {
            BinaryOperatorExpression binaryOperatorExpression = this.BinaryOperatorExpression;

            var delegateOperator = false;
            if (this.ResolveOperator(binaryOperatorExpression))
            {
                return;
            }
            else if (binaryOperatorExpression.Operator == BinaryOperatorType.Add ||
                binaryOperatorExpression.Operator == BinaryOperatorType.Subtract)
            {
                var leftResolverResult = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression.Left, this.Emitter);
                var rightResolverResult = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression.Right, this.Emitter);
                var add = binaryOperatorExpression.Operator == BinaryOperatorType.Add;

                if (this.Emitter.Validator.IsDelegateOrLambda(leftResolverResult) && this.Emitter.Validator.IsDelegateOrLambda(rightResolverResult))
                {
                    delegateOperator = true;
                    this.Write(Bridge.NET.Emitter.ROOT + "." + (add ? Bridge.NET.Emitter.DELEGATE_COMBINE : Bridge.NET.Emitter.DELEGATE_REMOVE));
                    this.WriteOpenParentheses();
                }
            }

            binaryOperatorExpression.Left.AcceptVisitor(this.Emitter);
            if (!delegateOperator)
            {
                this.WriteSpace();

                switch (binaryOperatorExpression.Operator)
                {
                    case BinaryOperatorType.Add:
                        this.Write("+");
                        break;
                    case BinaryOperatorType.BitwiseAnd:
                        this.Write("&");
                        break;
                    case BinaryOperatorType.BitwiseOr:
                        this.Write("|");
                        break;
                    case BinaryOperatorType.ConditionalAnd:
                        this.Write("&&");
                        break;
                    case BinaryOperatorType.NullCoalescing:
                    case BinaryOperatorType.ConditionalOr:
                        this.Write("||");
                        break;
                    case BinaryOperatorType.Divide:
                        this.Write("/");
                        break;
                    case BinaryOperatorType.Equality:
                        this.Write("===");
                        break;
                    case BinaryOperatorType.ExclusiveOr:
                        this.Write("^");
                        break;
                    case BinaryOperatorType.GreaterThan:
                        this.Write(">");
                        break;
                    case BinaryOperatorType.GreaterThanOrEqual:
                        this.Write(">=");
                        break;
                    case BinaryOperatorType.InEquality:
                        this.Write("!==");
                        break;
                    case BinaryOperatorType.LessThan:
                        this.Write("<");
                        break;
                    case BinaryOperatorType.LessThanOrEqual:
                        this.Write("<=");
                        break;
                    case BinaryOperatorType.Modulus:
                        this.Write("%");
                        break;
                    case BinaryOperatorType.Multiply:
                        this.Write("*");
                        break;
                    case BinaryOperatorType.ShiftLeft:
                        this.Write("<<");
                        break;
                    case BinaryOperatorType.ShiftRight:
                        this.Write(">>");
                        break;
                    case BinaryOperatorType.Subtract:
                        this.Write("-");
                        break;
                    default:
                        throw (Exception)this.Emitter.CreateException(binaryOperatorExpression, "Unsupported binary operator: " + binaryOperatorExpression.Operator.ToString());
                }
            }
            else
            {
                this.WriteComma();
            }

            this.WriteSpace();
            binaryOperatorExpression.Right.AcceptVisitor(this.Emitter);

            if (delegateOperator)
            {
                this.WriteCloseParentheses();
            }
        }
    }
}
