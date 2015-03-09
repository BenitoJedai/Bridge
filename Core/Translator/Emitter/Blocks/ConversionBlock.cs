using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using System.Collections.Generic;

namespace Bridge.NET
{
    public abstract class ConversionBlock : AbstractEmitterBlock
    {
        public sealed override void Emit()
        {
            var expression = this.GetExpression();
            var isConversion = false;
            bool check = expression != null && !expression.IsNull && expression.Parent != null;
            if (check)
            {
                isConversion = this.CheckConversion(expression);   
            }
            
            this.EmitConversionExpression();
 
            if (isConversion)
            {
                this.Write(")");
            }
        }

        protected virtual bool CheckConversion(Expression expression)
        {
            return ConversionBlock.CheckConversion(this, expression);
        }

        public static bool IsUserDefinedConversion(AbstractEmitterBlock block, Expression expression)
        {
            Conversion conversion = null;
            try
            {
                conversion = block.Emitter.Resolver.Resolver.GetConversion(expression);

                if (conversion == null)
                {
                    return false;
                }

                return conversion.IsUserDefined;
            }
            catch
            {
            }

            return false;
        }

        public static bool CheckConversion(AbstractEmitterBlock block, Expression expression)
        {
            Conversion conversion = null;
            try
            {
                conversion = block.Emitter.Resolver.Resolver.GetConversion(expression);

                if (conversion == null)
                {
                    return false;
                }

                if (conversion.IsIdentityConversion)
                {
                    return false;
                }

                if (conversion.IsLifted)
                {
                    block.Write("Bridge.nullable.lift(");
                }

                if (conversion.IsUserDefined)
                {
                    var method = conversion.Method;

                    block.Write(block.Emitter.ShortenTypeName(method.DeclaringType.FullName));
                    block.WriteDot();

                    block.Write(block.Emitter.GetMemberOverloadName(method.MemberDefinition as IMethod));

                    if (conversion.IsLifted)
                    {
                        block.WriteComma();
                    }
                    else
                    {
                        block.WriteOpenParentheses();
                    }

                    return true;
                }
                else if (conversion.IsNumericConversion)
                {
                }
            }
            catch
            {
            }

            return false;
        }

        protected abstract void EmitConversionExpression();
        protected abstract Expression GetExpression();
    }
}
