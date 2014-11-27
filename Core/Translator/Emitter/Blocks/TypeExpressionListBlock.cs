using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class TypeExpressionListBlock : AbstractEmitterBlock
    {
        public TypeExpressionListBlock(Emitter emitter, IEnumerable<TypeParamExpression> expressions)
        {
            this.Emitter = emitter;
            this.Expressions = expressions;
        }

        public IEnumerable<TypeParamExpression> Expressions
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.EmitExpressionList(this.Expressions);
        }

        protected virtual void EmitExpressionList(IEnumerable<TypeParamExpression> expressions)
        {
            bool needComma = false;

            foreach (var expr in expressions)
            {
                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(TypeBlock.TranslateTypeReference(expr.AstType, this.Emitter));
            }
        }
    }
}
