using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class ForBlock : AbstractEmitterBlock
    {
        public ForBlock(Emitter emitter, ForStatement forStatement)
        {
            this.Emitter = emitter;
            this.ForStatement = forStatement;
        }

        public ForStatement ForStatement 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitForStatement();
        }

        protected void VisitForStatement()
        {
            ForStatement forStatement = this.ForStatement;

            if (forStatement.Initializers.Count > 1)
            {
                throw this.Emitter.CreateException(forStatement, "Too many initializers");
            }

            this.PushLocals();

            this.Emitter.EnableSemicolon = false;
            this.WriteFor();
            this.WriteOpenParentheses();

            foreach (var item in forStatement.Initializers)
            {
                if (item != forStatement.Initializers.First())
                {
                    this.WriteComma();
                }

                item.AcceptVisitor(this.Emitter);
            }

            this.WriteSemiColon();
            this.WriteSpace();

            forStatement.Condition.AcceptVisitor(this.Emitter);

            this.WriteSemiColon();
            this.WriteSpace();

            foreach (var item in forStatement.Iterators)
            {
                if (item != forStatement.Iterators.First())
                {
                    this.WriteComma();
                }

                item.AcceptVisitor(this.Emitter);
            }

            this.WriteCloseParentheses();

            this.Emitter.EnableSemicolon = true;

            this.EmitBlockOrIndentedLine(forStatement.EmbeddedStatement);

            this.PopLocals();
        }
    }
}
