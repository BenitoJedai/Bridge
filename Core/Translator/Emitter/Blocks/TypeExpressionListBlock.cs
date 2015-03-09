using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class TypeExpressionListBlock : AbstractEmitterBlock
    {
        public TypeExpressionListBlock(IEmitter emitter, IEnumerable<TypeParamExpression> expressions)
        {
            this.Emitter = emitter;
            this.Expressions = expressions;
        }

        public TypeExpressionListBlock(IEmitter emitter, IEnumerable<AstType> types)
        {
            this.Emitter = emitter;
            this.Types = types;
        }

        public IEnumerable<TypeParamExpression> Expressions
        {
            get;
            set;
        }

        public IEnumerable<AstType> Types
        {
            get;
            set;
        }

        public override void Emit()
        {
            if (this.Expressions != null)
            {
                this.EmitExpressionList(this.Expressions);
            }
            else if (this.Types != null)
            {
                this.EmitExpressionList(this.Types);
            }
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
                this.Write(Helpers.TranslateTypeReference(expr.AstType, this.Emitter));
            }
        }

        protected virtual void EmitExpressionList(IEnumerable<AstType> types)
        {
            bool needComma = false;

            foreach (var type in types)
            {
                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(Helpers.TranslateTypeReference(type, this.Emitter));
            }
        }
    }
}
