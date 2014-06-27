using ICSharpCode.NRefactory.CSharp;
using System;

namespace Bridge.NET
{
    public abstract partial class AbstractEmitterBlock
    {
        public Emitter Emitter
        {
            get;
            set;
        }

        public abstract void Emit();

        protected virtual void EmitBlockOrIndentedLine(AstNode node)
        {
            bool block = node is BlockStatement;

            if (!block)
            {
                this.WriteNewLine();
                this.Indent();
            }
            else
            {
                this.WriteSpace();
            }

            node.AcceptVisitor(this.Emitter);

            if (!block)
            {
                this.Outdent();
            }
        }
    }
}
