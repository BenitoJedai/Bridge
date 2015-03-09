using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class ThrowBlock : AbstractEmitterBlock
    {
        public ThrowBlock(IEmitter emitter, ThrowStatement throwStatement)
        {
            this.Emitter = emitter;
            this.ThrowStatement = throwStatement;
        }

        public ThrowStatement ThrowStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            var oldValue = this.Emitter.ReplaceAwaiterByVar;
            if (this.Emitter.IsAsync)
            {                
                this.WriteAwaiters(this.ThrowStatement.Expression);
                this.Emitter.ReplaceAwaiterByVar = true;
            }
            
            this.WriteThrow();            
            this.ThrowStatement.Expression.AcceptVisitor(this.Emitter);
            this.WriteSemiColon();
            this.WriteNewLine();
            this.Emitter.ReplaceAwaiterByVar = oldValue;
        }
    }
}
