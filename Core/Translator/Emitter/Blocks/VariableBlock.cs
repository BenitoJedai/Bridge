using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class VariableBlock : AbstractEmitterBlock
    {
        public VariableBlock(Emitter emitter, VariableDeclarationStatement variableDeclarationStatement)
        {
            this.Emitter = emitter;
            this.VariableDeclarationStatement = variableDeclarationStatement;
        }

        public VariableDeclarationStatement VariableDeclarationStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitVariableDeclarationStatement();
        }

        protected virtual void VisitVariableDeclarationStatement()
        {
            bool needVar = true;
            bool needComma = false;
            bool addSemicolon = !this.Emitter.IsAsync;

            var oldSemiColon = this.Emitter.EnableSemicolon;
            var asyncExpressionHandling = this.Emitter.AsyncExpressionHandling;

            foreach (var variable in this.VariableDeclarationStatement.Variables)
            {
                this.Emitter.Validator.CheckIdentifier(variable.Name, this.VariableDeclarationStatement);
                this.AddLocal(variable.Name, this.VariableDeclarationStatement.Type);

                if (variable.Initializer != null && !variable.Initializer.IsNull && variable.Initializer.ToString().Contains(Emitter.FIX_ARGUMENT_NAME))
                {
                    continue;
                }

                if (needVar)
                {
                    this.WriteVar();
                    needVar = false;
                }

                this.WriteAwaiters(variable.Initializer);

                if (!this.Emitter.IsAsync || !variable.Initializer.IsNull)
                {
                    if (needComma)
                    {
                        if (this.Emitter.IsAsync)
                        {
                            this.WriteSemiColon();
                        }
                        else
                        {
                            this.WriteComma();
                        }
                    }

                    needComma = true;

                    this.Write(variable.Name);
                }

                if (!variable.Initializer.IsNull)
                {
                    addSemicolon = true;
                    this.Write(" = ");
                    var oldValue = this.Emitter.ReplaceAwaiterByVar;
                    this.Emitter.ReplaceAwaiterByVar = true;
                    variable.Initializer.AcceptVisitor(this.Emitter);
                    this.Emitter.ReplaceAwaiterByVar = oldValue;
                }
            }

            this.Emitter.AsyncExpressionHandling = asyncExpressionHandling;

            if (this.Emitter.EnableSemicolon && !needVar && addSemicolon)
            {
                this.WriteSemiColon(true);
            }

            if (oldSemiColon)
            {
                this.Emitter.EnableSemicolon = true;
            }
        }        
    }
}
