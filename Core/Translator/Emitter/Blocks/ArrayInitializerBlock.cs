using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class ArrayInitializerBlock : AbstractEmitterBlock
    {
        public ArrayInitializerBlock(IEmitter emitter, ArrayInitializerExpression arrayInitializerExpression)
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
            var first = elements.Count > 0 ? elements.First() : null;
            var isCollectionInitializer = first is ArrayInitializerExpression;
            var isObjectInitializer = first is NamedExpression || first is NamedArgumentExpression;

            if (!isObjectInitializer || this.ArrayInitializerExpression.IsSingleElement)
            {
                this.Write("[");
            }
            else
            {
                this.BeginBlock();
            }
            
            new ExpressionListBlock(this.Emitter, elements, null).Emit();

            if (!isObjectInitializer || this.ArrayInitializerExpression.IsSingleElement)
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
