using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ExpressionBlock : AbstractEmitterBlock
    {
        public ExpressionBlock(IEmitter emitter, ExpressionStatement expressionStatement)
        {
            this.Emitter = emitter;
            this.ExpressionStatement = expressionStatement;
        }

        public ExpressionStatement ExpressionStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            if (this.ExpressionStatement.IsNull)
            {
                return;
            }

            var oldSemiColon = this.Emitter.EnableSemicolon;
            
            List<Expression> awaiters = null;
            if (this.Emitter.IsAsync)
            {
                var awaitSearch = new AwaitSearchVisitor();
                this.ExpressionStatement.Expression.AcceptVisitor(awaitSearch);
                awaiters = awaitSearch.GetAwaitExpressions();
            }
            bool isAwaiter = this.ExpressionStatement.Expression is UnaryOperatorExpression && ((UnaryOperatorExpression)this.ExpressionStatement.Expression).Operator == UnaryOperatorType.Await;


            this.ExpressionStatement.Expression.AcceptVisitor(this.Emitter);

            if (this.Emitter.EnableSemicolon && !isAwaiter)
            {
                this.WriteSemiColon(true);
            }

            if (oldSemiColon)
            {
                this.Emitter.EnableSemicolon = true;
            }
        }        
    }
}
