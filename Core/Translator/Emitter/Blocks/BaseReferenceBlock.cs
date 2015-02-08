using Bridge.Plugin;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class BaseReferenceBlock : AbstractEmitterBlock
    {
        public BaseReferenceBlock(IEmitter emitter, BaseReferenceExpression baseReferenceExpression)
        {
            this.Emitter = emitter;
            this.BaseReferenceExpression = baseReferenceExpression;
        }

        public BaseReferenceExpression BaseReferenceExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.Write("base");
        }
    }
}
