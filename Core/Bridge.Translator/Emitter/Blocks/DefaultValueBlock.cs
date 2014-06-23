using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class DefaultValueBlock : AbstractEmitterBlock
    {
        public DefaultValueBlock(Emitter emitter, DefaultValueExpression defaultValueExpression)
        {
            this.Emitter = emitter;
            this.DefaultValueExpression = defaultValueExpression;
        }

        public DefaultValueExpression DefaultValueExpression 
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
