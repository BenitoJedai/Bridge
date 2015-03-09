using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ThisReferenceBlock : ConversionBlock
    {
        public ThisReferenceBlock(IEmitter emitter, ThisReferenceExpression thisReferenceExpression)
        {
            this.Emitter = emitter;
            this.ThisReferenceExpression = thisReferenceExpression;
        }

        public ThisReferenceExpression ThisReferenceExpression 
        { 
            get; 
            set; 
        }

        protected override Expression GetExpression()
        {
            return this.ThisReferenceExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.WriteThis();
        }  
    }
}
