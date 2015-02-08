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

                if (item.ParameterModifier == ParameterModifier.Out || item.ParameterModifier == ParameterModifier.Ref)
                {
                    var name = item.Name.StartsWith(Bridge.NET.Emitter.FIX_ARGUMENT_NAME) ? item.Name.Substring(Bridge.NET.Emitter.FIX_ARGUMENT_NAME.Length) : item.Name;
                    
                    if (!this.Emitter.LocalsMap.ContainsKey(name))
                    {
                        this.Emitter.LocalsMap.Add(name, name + ".v");
                    }
                }
            });
        }

        protected void AddLocal(string name, AstType type)
        {
            this.Emitter.Locals.Add(name, type);
            if (this.Emitter.IsAsync && !this.Emitter.AsyncVariables.Contains(name))
            {
                this.Emitter.AsyncVariables.Add(name);
            }
        }

        protected virtual Dictionary<string, string> BuildLocalsMap(AstNode statement)
        {
            var visitor = new ReferenceArgumentVisitor();
            statement.AcceptVisitor(visitor);
            var prevMap = this.Emitter.LocalsMap;
            this.Emitter.LocalsMap = new Dictionary<string, string>();
            foreach (IdentifierExpression expr in visitor.DirectionExpression)
            {
                if (!this.Emitter.LocalsMap.ContainsKey(expr.Identifier))
                {
                    this.Emitter.LocalsMap.Add(expr.Identifier, expr.Identifier + ".v");
                }
            }

            return prevMap;
        }

        protected virtual void ClearLocalsMap(Dictionary<string, string> prevMap = null)
        {
            this.Emitter.LocalsMap = prevMap;
        }
    }
}
