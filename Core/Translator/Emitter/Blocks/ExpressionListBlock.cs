using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ExpressionListBlock : AbstractEmitterBlock
    {
        public ExpressionListBlock(IEmitter emitter, IEnumerable<Expression> expressions, Expression paramArg)
        {
            this.Emitter = emitter;
            this.Expressions = expressions;
            this.ParamExpression = paramArg;
        }

        public IEnumerable<Expression> Expressions
        {
            get;
            set;
        }

        public Expression ParamExpression
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.EmitExpressionList(this.Expressions, this.ParamExpression);
        }

        protected virtual void EmitExpressionList(IEnumerable<Expression> expressions, Expression paramArg)
        {
            bool needComma = false;
            int count = this.Emitter.Writers.Count;

            foreach (var expr in expressions)
            {
                if (needComma)
                {
                    this.WriteComma();
                }

                if (expr == paramArg)
                {
                    this.WriteOpenBracket();
                }

                needComma = true;
                expr.AcceptVisitor(this.Emitter);

                if (this.Emitter.Writers.Count != count)
                {
                    this.PopWriter();
                    count = this.Emitter.Writers.Count;
                }
            }

            if (paramArg != null)
            {
                this.WriteCloseBracket();
            }
        }
    }
}
