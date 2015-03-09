using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class Block : AbstractEmitterBlock
    {
        public Block(IEmitter emitter, BlockStatement blockStatement)
        {
            this.Emitter = emitter;
            this.BlockStatement = blockStatement;

            if (this.Emitter.IgnoreBlock == blockStatement)
            {
                this.AsyncNoBraces = true;
            }

            if (this.Emitter.NoBraceBlock == blockStatement)
            {
                this.NoBraces = true;
            }
        }

        public BlockStatement BlockStatement 
        { 
            get; 
            set; 
        }

        protected bool AddEndBlock
        {
            get;
            set;
        }

        public bool AsyncNoBraces
        {
            get;
            set;
        }

        public bool NoBraces
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

            if (this.AsyncNoBraces || this.NoBraces)
            {
                return true;
            }

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

            if (parent is OperatorDeclaration)
            {
                return true;
            }

            if (parent is Accessor && (parent.Parent is PropertyDeclaration || parent.Parent is CustomEventDeclaration))
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
            this.BeginEmitBlock();
            this.DoEmitBlock();
            this.EndEmitBlock();
        }

        public void DoEmitBlock()
        {
            this.BlockStatement.Children.ToList().ForEach(child => child.AcceptVisitor(this.Emitter));
        }

        public void EndEmitBlock()
        {
            if (!this.NoBraces && (!this.Emitter.IsAsync || (!this.AsyncNoBraces && this.BlockStatement.Parent != this.Emitter.AsyncBlock.Node)))
            {
                this.EndBlock();
            }            

            if (this.AddEndBlock)
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

        public void BeginEmitBlock()
        {
            this.PushLocals();

            if (!this.NoBraces && (!this.Emitter.IsAsync || (!this.AsyncNoBraces && this.BlockStatement.Parent != this.Emitter.AsyncBlock.Node)))
            {
                this.BeginBlock();
            }            
        }        
    }
}
