using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public partial class AbstractEmitterBlock
    {
        protected virtual void PushLocals()
        {
            if (this.Emitter.LocalsStack == null)
            {
                this.Emitter.LocalsStack = new Stack<Dictionary<string, AstType>>();
            }

            this.Emitter.LocalsStack.Push(this.Emitter.Locals);
            this.Emitter.Locals = new Dictionary<string, AstType>(this.Emitter.Locals);
        }

        protected virtual void PopLocals()
        {
            this.Emitter.Locals = this.Emitter.LocalsStack.Pop();
        }

        protected virtual void ResetLocals()
        {
            this.Emitter.Locals = new Dictionary<string, AstType>();
            this.Emitter.IteratorCount = 0;
        }

        protected virtual void AddLocals(IEnumerable<ParameterDeclaration> declarations)
        {
            declarations.ToList().ForEach(item =>
            {
                this.AddLocal(item.Name, item.Type);
            });
        }

        protected void AddLocal(string name, AstType type)
        {
            this.Emitter.Locals.Add(name, type);
            if (this.Emitter.IsAsync)
            {
                this.Emitter.AsyncVariables.Add(name);
            }
        }
    }
}
