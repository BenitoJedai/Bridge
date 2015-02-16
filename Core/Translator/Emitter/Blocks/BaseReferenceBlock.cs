using Bridge.Plugin;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class BaseReferenceBlock : ConversionBlock
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

        protected override Expression GetExpression()
        {
            return this.BaseReferenceExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.Write("base");
        }
    }
}
