using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using System;

namespace Bridge.NET
{
    public abstract partial class AbstractEmitterBlock : IAbstractEmitterBlock
    {        
        public abstract void Emit();

        public IEmitter Emitter
        {
            get;
            set;
        }

        public virtual void EmitBlockOrIndentedLine(AstNode node)
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

        public bool NoValueableSiblings(AstNode node)
        {
            while (node.NextSibling != null)
            {
                var sibling = node.NextSibling;

                if (sibling is NewLineNode || sibling is CSharpTokenNode || sibling is Comment)
                {
                    node = sibling;
                }
                else
                {
                    return false;
                }
            }
            
            return true;
        }

        protected Expression[] GetAwaiters(AstNode node)
        {
            var awaitSearch = new AwaitSearchVisitor();
            node.AcceptVisitor(awaitSearch);
            return awaitSearch.GetAwaitExpressions().ToArray();
        }

        protected bool IsDirectAsyncBlockChild(AstNode node)
        {
            var block = node.GetParent<BlockStatement>();
            if (block != null && (block.Parent is MethodDeclaration || block.Parent is AnonymousMethodExpression || block.Parent is LambdaExpression))
            {
                return true;
            }

            return false;
        }

        protected IAsyncStep WriteAwaiter(AstNode node)
        {
            var index = System.Array.IndexOf(this.Emitter.AsyncBlock.AwaitExpressions, node) + 1;
            this.Write("$task" + index + " = ");

            var oldValue = this.Emitter.ReplaceAwaiterByVar;
            this.Emitter.ReplaceAwaiterByVar = true;
            node.AcceptVisitor(this.Emitter);
            this.Emitter.ReplaceAwaiterByVar = oldValue;

            this.WriteSemiColon();
            this.WriteNewLine();
            this.Write("$step = " + this.Emitter.AsyncBlock.Step + ";");
            this.WriteNewLine();
            this.Write("$task" + index + ".continueWith($asyncBody);");
            this.WriteNewLine();
            this.Write("return;");

            var asyncStep = this.Emitter.AsyncBlock.AddAsyncStep(index);
            if (this.Emitter.AsyncBlock.EmittedAsyncSteps != null)
            {
                this.Emitter.AsyncBlock.EmittedAsyncSteps.Add(asyncStep);
            }

            return asyncStep;
        }

        protected void WriteAwaiters(AstNode node)
        {
            var awaiters = this.Emitter.IsAsync && !node.IsNull ? this.GetAwaiters(node) : null;

            if (awaiters != null && awaiters.Length > 0)
            {
                var oldValue = this.Emitter.AsyncExpressionHandling;
                this.Emitter.AsyncExpressionHandling = true;

                foreach (var awaiter in awaiters)
                {
                    this.WriteAwaiter(awaiter);
                }

                this.Emitter.AsyncExpressionHandling = oldValue;
            }
        }
    }
}
