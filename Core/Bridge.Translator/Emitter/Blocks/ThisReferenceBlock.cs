using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ThisReferenceBlock : AbstractEmitterBlock
    {
        public ThisReferenceBlock(Emitter emitter, ThisReferenceExpression thisReferenceExpression)
        {
            this.Emitter = emitter;
            this.ThisReferenceExpression = thisReferenceExpression;
        }

        public ThisReferenceExpression ThisReferenceExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.WriteThis();
        }
    }
}
