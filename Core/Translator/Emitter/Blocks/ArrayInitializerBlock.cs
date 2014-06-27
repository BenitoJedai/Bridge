using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ArrayInitializerBlock : AbstractEmitterBlock
    {
        public ArrayInitializerBlock(Emitter emitter, ArrayInitializerExpression arrayInitializerExpression)
        {
            this.Emitter = emitter;
            this.ArrayInitializerExpression = arrayInitializerExpression;
        }

        public ArrayInitializerExpression ArrayInitializerExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.BeginBlock();
            var elements = this.ArrayInitializerExpression.Elements;
            new ExpressionListBlock(this.Emitter, elements, null).Emit();
            this.WriteNewLine();
            this.EndBlock();
        }
    }
}
