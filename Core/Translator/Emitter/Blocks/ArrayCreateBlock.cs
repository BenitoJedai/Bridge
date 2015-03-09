using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class ArrayCreateBlock : AbstractEmitterBlock
    {
        public ArrayCreateBlock(IEmitter emitter, ArrayCreateExpression arrayCreateExpression)
        {
            this.Emitter = emitter;
            this.ArrayCreateExpression = arrayCreateExpression;
        }

        public ArrayCreateExpression ArrayCreateExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitArrayCreateExpression();
        }

        protected void VisitArrayCreateExpression()
        {
            ArrayCreateExpression arrayCreateExpression = this.ArrayCreateExpression;

            if (arrayCreateExpression.Arguments.Count > 1)
            {
                throw (Exception)this.Emitter.CreateException(arrayCreateExpression, "Multi-dimensional arrays are not supported");
            }

            if (arrayCreateExpression.Initializer.IsNull && arrayCreateExpression.Arguments.Count > 0)
            {
                this.Write("new Array(");
                arrayCreateExpression.Arguments.First().AcceptVisitor(this.Emitter);
                this.Write(")");

                return;
            }

            this.WriteOpenBracket();
            var elements = arrayCreateExpression.Initializer.Elements;
            new ExpressionListBlock(this.Emitter, elements, null).Emit();
            this.WriteCloseBracket();
        }
    }
}
