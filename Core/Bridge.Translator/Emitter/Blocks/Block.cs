using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class Block : AbstractEmitterBlock
    {
        public Block(Emitter emitter, BlockStatement blockStatement)
        {
            this.Emitter = emitter;
            this.BlockStatement = blockStatement;
        }

        public BlockStatement BlockStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.EmitBlock();
        }

        protected virtual bool KeepLineAfterBlock(BlockStatement block)
        {
            var parent = block.Parent;

            if (parent is AnonymousMethodExpression)
            {
                return true;
            }

            if (parent is LambdaExpression)
            {
                return true;
            }

            if (parent is MethodDeclaration)
            {
                return true;
            }

            if (parent is Accessor && parent.Parent is PropertyDeclaration)
            {
                return true;
            }

            var loop = parent as DoWhileStatement;

            if (loop != null)
            {
                return true;
            }

            return false;
        }

        public void EmitBlock()
        {
            var addEndBlock = this.Emitter.InjectMethodDetectors && this.BlockStatement.Children.ToList().Count > 0;
            this.PushLocals();
            this.BeginBlock();

            if (this.Emitter.InjectMethodDetectors)
            {
                new InjectMethodDetectorBlock(this.Emitter, this.BlockStatement).Emit();
            }

            this.BlockStatement.Children.ToList().ForEach(child => child.AcceptVisitor(this.Emitter));
            this.EndBlock();

            if (addEndBlock)
            {
                this.WriteNewLine();
                this.EndBlock();
            }

            if (!this.KeepLineAfterBlock(this.BlockStatement))
            {
                this.WriteNewLine();
            }

            this.PopLocals();
        }        
    }
}
