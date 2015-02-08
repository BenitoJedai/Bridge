using Bridge.Plugin;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class PrimitiveExpressionBlock : AbstractEmitterBlock
    {
        public PrimitiveExpressionBlock(IEmitter emitter, PrimitiveExpression primitiveExpression)
        {
            this.Emitter = emitter;
            this.PrimitiveExpression = primitiveExpression;
        }

        public PrimitiveExpression PrimitiveExpression
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            if (this.PrimitiveExpression.IsNull)
            {
                return;
            }

            this.WriteScript(this.PrimitiveExpression.Value);
        }        
    }
}
