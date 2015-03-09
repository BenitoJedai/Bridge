using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ConditionalBlock : AbstractEmitterBlock
    {
        public ConditionalBlock(IEmitter emitter, ConditionalExpression conditionalExpression)
        {
            this.Emitter = emitter;
            this.ConditionalExpression = conditionalExpression;
        }

        public ConditionalExpression ConditionalExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            var conditionalExpression = this.ConditionalExpression;

            conditionalExpression.Condition.AcceptVisitor(this.Emitter);
            this.Write(" ? ");
            conditionalExpression.TrueExpression.AcceptVisitor(this.Emitter);
            this.Write(" : ");
            conditionalExpression.FalseExpression.AcceptVisitor(this.Emitter);
        }
    }
}
