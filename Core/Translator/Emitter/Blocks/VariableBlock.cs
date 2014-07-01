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

            var oldSemiColon = this.Emitter.EnableSemicolon;

            foreach (var variable in this.VariableDeclarationStatement.Variables)
            {
                this.Emitter.Validator.CheckIdentifier(variable.Name, this.VariableDeclarationStatement);
                this.Emitter.Locals.Add(variable.Name, this.VariableDeclarationStatement.Type);

                if (variable.Initializer != null && !variable.Initializer.IsNull && variable.Initializer.ToString().Contains(Emitter.FIX_ARGUMENT_NAME))
                {
                    continue;
                }

                if (needVar)
                {
                    this.WriteVar();
                    needVar = false;
                }

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;

                this.Write(variable.Name);

                if (!variable.Initializer.IsNull)
                {
                    this.Write(" = ");
                    variable.Initializer.AcceptVisitor(this.Emitter);
                }
            }

            if (this.Emitter.EnableSemicolon && !needVar)
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
