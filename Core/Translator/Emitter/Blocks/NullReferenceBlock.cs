using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class NullReferenceBlock : AbstractEmitterBlock
    {
        public NullReferenceBlock(IEmitter emitter, AstNode nullNode)
        {
            this.Emitter = emitter;
            this.NullNode = nullNode;
        }

        public AstNode NullNode 
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
