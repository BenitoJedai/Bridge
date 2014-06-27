using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public class CastBlock : AbstractEmitterBlock
    {
        public CastBlock(Emitter emitter, CastExpression castExpression)
        {
            this.Emitter = emitter;
            this.CastExpression = castExpression;
        }

        public CastBlock(Emitter emitter, AsExpression asExpression)
        {
            this.Emitter = emitter;
            this.AsExpression = asExpression;
        }

        public CastBlock(Emitter emitter, IsExpression isExpression)
        {
            this.Emitter = emitter;
            this.IsExpression = isExpression;
        }

        public CastBlock(Emitter emitter, IType iType)
        {
            this.Emitter = emitter;
            this.IType = iType;
        }

        public CastBlock(Emitter emitter, AstType astType)
        {
            this.Emitter = emitter;
            this.AstType = astType;
        }

        public CastExpression CastExpression 
        { 
            get; 
            set; 
        }

        public AsExpression AsExpression 
        { 
            get; 
            set; 
        }

        public IsExpression IsExpression
        {
            get;
            set;
        }

        public IType IType
        {
            get;
            set;
        }

        public AstType AstType
        {
            get;
            set;
        }

        public override void Emit()
        {
            if (this.CastExpression != null)
            {
                this.EmitCastExpression(this.CastExpression.Expression, this.CastExpression.Type, Emitter.CAST);
            }
            else if (this.AsExpression != null)
            {
                this.EmitCastExpression(this.AsExpression.Expression, this.AsExpression.Type, Emitter.AS);
            }
            else if (this.IsExpression != null)
            {
                this.EmitCastExpression(this.IsExpression.Expression, this.IsExpression.Type, Emitter.IS);
            }
            else if (this.IType != null)
            {
                this.EmitCastType(this.IType);
            }
            else if (this.AstType != null)
            {
                this.EmitCastType(this.AstType);
            }
        }

        protected virtual void EmitCastExpression(Expression expression, AstType type, string method)
        {
            bool isInlineCast;
            string castCode = this.GetCastCode(expression, type, out isInlineCast);

            if (isInlineCast)
            {
                this.EmitInlineCast(expression, type, castCode);
                return;
            }

            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.CAST);
            this.WriteOpenParentheses();
            expression.AcceptVisitor(this.Emitter);
            this.WriteComma();

            if (castCode != null)
            {
                this.Write(castCode);
            }
            else
            {
                this.EmitCastType(type);
            }
            this.WriteCloseParentheses();
        }        

        protected virtual void EmitCastType(AstType astType)
        {
            var resolveResult = this.Emitter.Resolver.ResolveNode(astType, this.Emitter);

            if (NullableType.IsNullable(resolveResult.Type))
            {
                this.Write(this.Emitter.ShortenTypeName(Helpers.ReplaceSpecialChars(NullableType.GetUnderlyingType(resolveResult.Type).FullName)));
            }
            else
            {
                astType.AcceptVisitor(this.Emitter);
            }
        }

        protected virtual void EmitCastType(IType iType)
        {
            if (NullableType.IsNullable(iType))
            {
                this.Write(this.Emitter.ShortenTypeName(Helpers.ReplaceSpecialChars(NullableType.GetUnderlyingType(iType).FullName)));
            }
            else if (iType.Kind == TypeKind.Array)
            {
                this.Write("Array");
            }
            else
            {
                this.Write(this.Emitter.ShortenTypeName(Helpers.ReplaceSpecialChars(iType.FullName + (iType.TypeParameterCount > 0 ? ("$" + iType.TypeParameterCount) : ""))));
            }
        }

        protected virtual string GetCastCode(Expression expression, AstType astType, out bool isInline)
        {
            var resolveResult = this.Emitter.Resolver.ResolveNode(astType, this.Emitter) as TypeResolveResult;
            var exprResolveResult = this.Emitter.Resolver.ResolveNode(expression, this.Emitter);
            string inline = null;
            isInline = false;

            var method = exprResolveResult.Type.GetMethods().FirstOrDefault(m =>
            {
                if (m.IsOperator && m.Name == "op_Explicit" &&
                    m.Parameters.Count == 1 &&
                    m.ReturnType.ReflectionName == resolveResult.Type.ReflectionName &&
                    m.Parameters[0].Type.ReflectionName == exprResolveResult.Type.ReflectionName
                    )
                {
                    string tmpInline = this.Emitter.GetInline(m);
                    if (!string.IsNullOrWhiteSpace(tmpInline))
                    {
                        inline = tmpInline;
                        return true;
                    }
                }

                return false;
            });

            if (inline != null)
            {
                isInline = true;
                return inline;
            }

            if (resolveResult != null)
            {
                IEnumerable<IAttribute> attributes = null;
                DefaultResolvedTypeDefinition type = resolveResult.Type as DefaultResolvedTypeDefinition;
                if (type != null)
                {
                    attributes = type.Attributes;
                }
                else
                {
                    ParameterizedType paramType = resolveResult.Type as ParameterizedType;
                    if (paramType != null)
                    {
                        attributes = paramType.GetDefinition().Attributes;
                    }
                }

                if (attributes != null)
                {
                    var attribute = this.Emitter.GetAttribute(attributes, Translator.CLR_ASSEMBLY + ".CastAttribute");

                    if (attribute != null)
                    {
                        return attribute.PositionalArguments[0].ConstantValue.ToString();
                    }
                }
            }
            return null;
        }

        protected virtual void EmitInlineCast(Expression expression, AstType astType, string castCode)
        {
            this.Write("");

            if (castCode.Contains("{this}"))
            {
                var oldBuilder = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();
                expression.AcceptVisitor(this.Emitter);
                castCode = castCode.Replace("{this}", this.Emitter.Output.ToString());
                this.Emitter.Output = oldBuilder;
            }

            if (castCode.Contains("{0}"))
            {
                var oldBuilder = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();
                this.EmitCastType(astType);
                castCode = castCode.Replace("{0}", this.Emitter.Output.ToString());
                this.Emitter.Output = oldBuilder;
            }

            this.Write(castCode);
        }   
    }
}
