using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class DefaultValueBlock : AbstractEmitterBlock
    {
        public DefaultValueBlock(IEmitter emitter, DefaultValueExpression defaultValueExpression)
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
            var resolveResult = this.Emitter.Resolver.ResolveNode(DefaultValueExpression.Type, this.Emitter);

            if (!resolveResult.IsError && resolveResult.Type.IsReferenceType.HasValue && resolveResult.Type.IsReferenceType.Value)
            {
                this.Write("null");
            }
            else
            {
                this.Write("Bridge.getDefaultValue(" + DefaultValueExpression.Type.ToString() + ")");
            }
        }
    }
}
