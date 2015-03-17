using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class BinaryOperatorBlock : ConversionBlock
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

        protected override Expression GetExpression()
        {
            return this.BinaryOperatorExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.VisitBinaryOperatorExpression();
        }  

        protected bool ResolveOperator(BinaryOperatorExpression binaryOperatorExpression, OperatorResolveResult orr)
        {
            if (orr != null && orr.UserDefinedOperatorMethod != null)
            {
                var method = orr.UserDefinedOperatorMethod;
                var inline = this.Emitter.GetInline(method);

                if (!string.IsNullOrWhiteSpace(inline))
                {
                    new InlineArgumentsBlock(this.Emitter, new ArgumentsInfo(this.Emitter, binaryOperatorExpression, orr), inline).Emit();
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
                    
                    new ExpressionListBlock(this.Emitter, new Expression[] { binaryOperatorExpression.Left, binaryOperatorExpression.Right }, null).Emit();
                    this.WriteCloseParentheses();

                    return true;
                }
            }

            return false;
        }

        protected void VisitBinaryOperatorExpression()
        {
            BinaryOperatorExpression binaryOperatorExpression = this.BinaryOperatorExpression;
            var resolveOperator = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression, this.Emitter);

            if (resolveOperator is ConstantResolveResult)
            {
                this.WriteScript(((ConstantResolveResult)resolveOperator).ConstantValue);
                return;
            }

            OperatorResolveResult orr = resolveOperator as OperatorResolveResult;

            var delegateOperator = false;
            if (this.ResolveOperator(binaryOperatorExpression, orr))
            {
                return;
            }

            var leftResolverResult = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression.Left, this.Emitter);
            var rightResolverResult = this.Emitter.Resolver.ResolveNode(binaryOperatorExpression.Right, this.Emitter);

            if (binaryOperatorExpression.Operator == BinaryOperatorType.Divide &&
                (
                    (Helpers.IsIntegerType(leftResolverResult.Type, this.Emitter.Resolver) && 
                    Helpers.IsIntegerType(rightResolverResult.Type, this.Emitter.Resolver)) ||

                    (Helpers.IsIntegerType(this.Emitter.Resolver.Resolver.GetExpectedType(binaryOperatorExpression.Left), this.Emitter.Resolver) &&
                    Helpers.IsIntegerType(this.Emitter.Resolver.Resolver.GetExpectedType(binaryOperatorExpression.Right), this.Emitter.Resolver))
                ))
            {
                this.Write("Bridge.Int.div(");
                binaryOperatorExpression.Left.AcceptVisitor(this.Emitter);
                this.Write(", ");
                binaryOperatorExpression.Right.AcceptVisitor(this.Emitter);
                this.Write(")");
                return;
            }
            
            if (binaryOperatorExpression.Operator == BinaryOperatorType.Add ||
                binaryOperatorExpression.Operator == BinaryOperatorType.Subtract)
            {
                
                var add = binaryOperatorExpression.Operator == BinaryOperatorType.Add;

                if (this.Emitter.Validator.IsDelegateOrLambda(leftResolverResult) && this.Emitter.Validator.IsDelegateOrLambda(rightResolverResult))
                {
                    delegateOperator = true;
                    this.Write(Bridge.NET.Emitter.ROOT + "." + (add ? Bridge.NET.Emitter.DELEGATE_COMBINE : Bridge.NET.Emitter.DELEGATE_REMOVE));
                    this.WriteOpenParentheses();
                }
            }

            bool nullable = NullableType.IsNullable(leftResolverResult.Type) || NullableType.IsNullable(rightResolverResult.Type);
            if (nullable)
            {
                this.Write(Bridge.NET.Emitter.ROOT + ".nullable.");
            }
            else
            {
                binaryOperatorExpression.Left.AcceptVisitor(this.Emitter);
            }

            if (!delegateOperator)
            {
                if (!nullable)
                {
                    this.WriteSpace();
                }

                switch (binaryOperatorExpression.Operator)
                {
                    case BinaryOperatorType.Add:
                        this.Write(nullable ? "add" : "+");
                        break;
                    case BinaryOperatorType.BitwiseAnd:
                        this.Write(nullable ? "band" : "&");
                        break;
                    case BinaryOperatorType.BitwiseOr:
                        this.Write(nullable ? "bor" : "|");
                        break;
                    case BinaryOperatorType.ConditionalAnd:
                        this.Write(nullable ? "and" : "&&");
                        break;
                    case BinaryOperatorType.NullCoalescing:
                    case BinaryOperatorType.ConditionalOr:
                        this.Write(nullable ? "or" : "||");
                        break;
                    case BinaryOperatorType.Divide:
                        this.Write(nullable ? "div" : "/");
                        break;
                    case BinaryOperatorType.Equality:
                        this.Write(nullable ? "eq" : "===");
                        break;
                    case BinaryOperatorType.ExclusiveOr:
                        this.Write(nullable ? "xor" : "^");
                        break;
                    case BinaryOperatorType.GreaterThan:
                        this.Write(nullable ? "gt" : ">");
                        break;
                    case BinaryOperatorType.GreaterThanOrEqual:
                        this.Write(nullable ? "gte" : ">=");
                        break;
                    case BinaryOperatorType.InEquality:
                        this.Write(nullable ? "neq" : "!==");
                        break;
                    case BinaryOperatorType.LessThan:
                        this.Write(nullable ? "lt" : "<");
                        break;
                    case BinaryOperatorType.LessThanOrEqual:
                        this.Write(nullable ? "lte" : "<=");
                        break;
                    case BinaryOperatorType.Modulus:
                        this.Write(nullable ? "mod" : "%");
                        break;
                    case BinaryOperatorType.Multiply:
                        this.Write(nullable ? "mul" : "*");
                        break;
                    case BinaryOperatorType.ShiftLeft:
                        this.Write(nullable ? "sl" : "<<");
                        break;
                    case BinaryOperatorType.ShiftRight:
                        this.Write(nullable ? "sr" : ">>");
                        break;
                    case BinaryOperatorType.Subtract:
                        this.Write(nullable ? "sub" : "-");
                        break;
                    default:
                        throw (Exception)this.Emitter.CreateException(binaryOperatorExpression, "Unsupported binary operator: " + binaryOperatorExpression.Operator.ToString());
                }
            }
            else
            {
                this.WriteComma();
            }

            if (nullable)
            {
                this.WriteOpenParentheses();
                binaryOperatorExpression.Left.AcceptVisitor(this.Emitter);
                this.WriteComma();
            }
            else
            {
                this.WriteSpace();
            }
            
            binaryOperatorExpression.Right.AcceptVisitor(this.Emitter);

            if (delegateOperator || nullable)
            {
                this.WriteCloseParentheses();
            }
        }
    }
}
