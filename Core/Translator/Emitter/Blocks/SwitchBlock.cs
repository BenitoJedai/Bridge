using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class SwitchBlock : AbstractEmitterBlock
    {
        public SwitchBlock(Emitter emitter, SwitchStatement switchStatement)
        {
            this.Emitter = emitter;
            this.SwitchStatement = switchStatement;
        }

        public SwitchBlock(Emitter emitter, SwitchSection switchSection)
        {
            this.Emitter = emitter;
            this.SwitchSection = switchSection;
        }

        public SwitchBlock(Emitter emitter, CaseLabel caseLabel)
        {
            this.Emitter = emitter;
            this.CaseLabel = caseLabel;
        }

        public SwitchStatement SwitchStatement 
        { 
            get; 
            set; 
        }

        public SwitchSection SwitchSection
        {
            get;
            set;
        }

        public CaseLabel CaseLabel
        {
            get;
            set;
        }

        public override void Emit()
        {
            if (this.SwitchStatement != null)
            {
                this.VisitSwitchStatement();
            }
            else if (this.SwitchSection != null)
            {
                this.VisitSwitchSection();
            }
            else
            {
                this.VisitCaseLabel();
            }
        }

        protected void VisitSwitchStatement()
        {
            SwitchStatement switchStatement = this.SwitchStatement;

            this.WriteSwitch();
            this.WriteOpenParentheses();

            switchStatement.Expression.AcceptVisitor(this.Emitter);

            this.WriteCloseParentheses();
            this.WriteSpace();

            this.BeginBlock();
            switchStatement.SwitchSections.ToList().ForEach(s => s.AcceptVisitor(this.Emitter));
            this.EndBlock();
            this.WriteNewLine();
        }

        protected void VisitSwitchSection()
        {
            SwitchSection switchSection = this.SwitchSection;

            switchSection.CaseLabels.ToList().ForEach(l => l.AcceptVisitor(this.Emitter));
            this.Indent();
            switchSection.AcceptChildren(this.Emitter);
            this.Outdent();
        }

        protected void VisitCaseLabel()
        {
            CaseLabel caseLabel = this.CaseLabel;

            if (caseLabel.Expression.IsNull)
            {
                this.Write("default");
            }
            else
            {
                this.Write("case ");
                caseLabel.Expression.AcceptVisitor(this.Emitter);
            }

            this.WriteColon();
            this.WriteNewLine();
        }
    }
}
