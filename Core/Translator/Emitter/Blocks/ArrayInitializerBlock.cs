using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

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
            var elements = this.ArrayInitializerExpression.Elements;
            var isCollectionInitializer = elements.Count > 0 && elements.First() is ArrayInitializerExpression;

            if (isCollectionInitializer || this.ArrayInitializerExpression.IsSingleElement)
            {
                this.Write("[");
            }
            else
            {
                this.BeginBlock();
            }
            
            new ExpressionListBlock(this.Emitter, elements, null).Emit();

            if (isCollectionInitializer || this.ArrayInitializerExpression.IsSingleElement)
            {
                this.Write("]");
            }
            else
            {
                this.WriteNewLine();
                this.EndBlock();
            }
        }
    }
}
