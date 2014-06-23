using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class AssignmentBlock : AbstractEmitterBlock
    {
        public AssignmentBlock(Emitter emitter, AssignmentExpression assignmentExpression)
        {
            this.Emitter = emitter;
            this.AssignmentExpression = assignmentExpression;
        }

        public AssignmentExpression AssignmentExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitAssignmentExpression();
        }

        protected void VisitAssignmentExpression()
        {
            AssignmentExpression assignmentExpression = this.AssignmentExpression;

            var delegateAssigment = false;
            var initCount = this.Emitter.Writers.Count;

            if (assignmentExpression.Operator == AssignmentOperatorType.Add ||
                assignmentExpression.Operator == AssignmentOperatorType.Subtract)
            {
                var leftResolverResult = this.Emitter.Resolver.ResolveNode(assignmentExpression.Left, this.Emitter);
                var rightResolverResult = this.Emitter.Resolver.ResolveNode(assignmentExpression.Right, this.Emitter);
                var add = assignmentExpression.Operator == AssignmentOperatorType.Add;

                if (this.Emitter.Validator.IsDelegateOrLambda(leftResolverResult) && this.Emitter.Validator.IsDelegateOrLambda(rightResolverResult))
                {
                    this.Emitter.IsAssignment = true;
                    assignmentExpression.Left.AcceptVisitor(this.Emitter);
                    this.Emitter.IsAssignment = false;
                    this.Write(" = ");

                    delegateAssigment = true;
                    this.Write(Emitter.ROOT + "." + (add ? Emitter.DELEGATE_COMBINE : Emitter.DELEGATE_REMOVE));
                    this.WriteOpenParentheses();
                }
            }

            this.Emitter.IsAssignment = true;
            assignmentExpression.Left.AcceptVisitor(this.Emitter);
            this.Emitter.IsAssignment = false;

            if (this.Emitter.Writers.Count == 0 && !delegateAssigment)
            {
                this.WriteSpace();
            }

            if (!delegateAssigment)
            {
                switch (assignmentExpression.Operator)
                {
                    case AssignmentOperatorType.Assign:
                        break;
                    case AssignmentOperatorType.Add:
                        this.Write("+");
                        break;
                    case AssignmentOperatorType.BitwiseAnd:
                        this.Write("&");
                        break;
                    case AssignmentOperatorType.BitwiseOr:
                        this.Write("|");
                        break;
                    case AssignmentOperatorType.Divide:
                        this.Write("/");
                        break;
                    case AssignmentOperatorType.ExclusiveOr:
                        this.Write("^");
                        break;
                    case AssignmentOperatorType.Modulus:
                        this.Write("%");
                        break;
                    case AssignmentOperatorType.Multiply:
                        this.Write("*");
                        break;
                    case AssignmentOperatorType.ShiftLeft:
                        this.Write("<<");
                        break;
                    case AssignmentOperatorType.ShiftRight:
                        this.Write(">>");
                        break;
                    case AssignmentOperatorType.Subtract:
                        this.Write("-");
                        break;
                    default:
                        throw this.Emitter.CreateException(assignmentExpression, "Unsupported assignment operator: " + assignmentExpression.Operator.ToString());
                }

                int count = this.Emitter.Writers.Count;

                if (count == 0)
                {
                    this.Write("= ");
                }
            }
            else
            {
                this.WriteComma();
            }

            assignmentExpression.Right.AcceptVisitor(this.Emitter);

            if (this.Emitter.Writers.Count > initCount)
            {
                this.PopWriter();
            }

            if (delegateAssigment)
            {
                this.WriteCloseParentheses();
            }
        }
    }
}
