using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class NullReferenceBlock : AbstractEmitterBlock
    {
        public NullReferenceBlock(Emitter emitter, NullReferenceExpression nullReferenceExpression)
        {
            this.Emitter = emitter;
            this.NullReferenceExpression = nullReferenceExpression;
        }

        public NullReferenceExpression NullReferenceExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.Write("null");
        }
    }
}
