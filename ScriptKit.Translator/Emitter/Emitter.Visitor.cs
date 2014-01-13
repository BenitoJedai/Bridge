using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
using System.Text.RegularExpressions;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;
using Newtonsoft.Json;
using Ext.Net.Utilities;

namespace ScriptKit.NET
{
    public partial class Emitter : Visitor
    {
        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            this.EnsureComma();
            this.ResetLocals();
            this.AddLocals(methodDeclaration.Parameters);

            if (methodDeclaration.HasModifier(Modifiers.Static))
            {
                this.Write(Helpers.GetScriptName(methodDeclaration));
            }
            else
            {
                this.Write(Helpers.GetScriptName(methodDeclaration));
            }

            this.Write(" : function");
            this.EmitMethodParameters(methodDeclaration.Parameters, methodDeclaration);
            this.Write(" ");

            var script = this.GetScript(methodDeclaration);
            if (script == null)
            {
                methodDeclaration.Body.AcceptVisitor(this);
            }
            else
            {
                this.BeginBlock();
                foreach (var line in script)
                {
                    this.Write(line);
                    this.NewLine();
                }
                this.EndBlock();
            }

            this.Comma = true;
        }

        public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            this.EmitPropertyMethod(propertyDeclaration, propertyDeclaration.Getter, false);
            this.EmitPropertyMethod(propertyDeclaration, propertyDeclaration.Setter, true);
        }

        public override void VisitBlockStatement(BlockStatement blockStatement)
        {
            this.PushLocals();
            this.BeginBlock();

            blockStatement.Children.ToList().ForEach(child => child.AcceptVisitor(this));
            this.EndBlock();

            if (!this.KeepLineAfterBlock(blockStatement))
            {
                NewLine();
            }

            this.PopLocals();
        }

        public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            this.Write("var ");
            bool needComma = false;
            foreach (var variable in variableDeclarationStatement.Variables)
            {
                this.CheckIdentifier(variable.Name, variableDeclarationStatement);
                this.Locals.Add(variable.Name, variableDeclarationStatement.Type);

                if (needComma)
                {
                    this.WriteComma();
                }
                needComma = true;

                this.Write(variable.Name);
                if (!variable.Initializer.IsNull)
                {
                    this.Write(" = ");
                    variable.Initializer.AcceptVisitor(this);
                }
            }

            if (this.EnableSemicolon)
            {
                this.Write(";");
                this.NewLine();
            }
        }

        public override void VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            if (primitiveExpression.IsNull)
            {
                return;
            }
            this.WriteScript(primitiveExpression.Value);
        }

        public override void VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            if (expressionStatement.IsNull)
            {
                return;
            }

            expressionStatement.Expression.AcceptVisitor(this);

            if (this.EnableSemicolon)
            {
                this.Write(";");
                this.NewLine();
            }
        }

        public override void VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            this.Write(";");
            this.NewLine();
        }

        public override void VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            switch (unaryOperatorExpression.Operator)
            {
                case UnaryOperatorType.BitNot:
                    this.Write("~");
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    break;
                case UnaryOperatorType.Decrement:
                    this.Write("--");
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    break;
                case UnaryOperatorType.Increment:
                    this.Write("++");
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    this.Write("");
                    break;
                case UnaryOperatorType.Minus:
                    this.Write("-");
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    break;
                case UnaryOperatorType.Not:
                    this.Write("!");
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    break;
                case UnaryOperatorType.Plus:
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    break;
                case UnaryOperatorType.PostDecrement:
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    this.Write("--");
                    break;
                case UnaryOperatorType.PostIncrement:
                    unaryOperatorExpression.Expression.AcceptVisitor(this);
                    this.Write("++");
                    break;
                default:
                    throw this.CreateException(unaryOperatorExpression, "Unsupported unary operator: " + unaryOperatorExpression.Operator.ToString());
            }
        }

        public override void VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            binaryOperatorExpression.Left.AcceptVisitor(this);
            this.Write(" ");
            switch (binaryOperatorExpression.Operator)
            {
                case BinaryOperatorType.Add:
                    this.Write("+");
                    break;
                case BinaryOperatorType.BitwiseAnd:
                    this.Write("&");
                    break;
                case BinaryOperatorType.BitwiseOr:
                    this.Write("|");
                    break;
                case BinaryOperatorType.ConditionalAnd:
                    this.Write("&&");
                    break;
                case BinaryOperatorType.NullCoalescing:
                case BinaryOperatorType.ConditionalOr:
                    this.Write("||");
                    break;
                case BinaryOperatorType.Divide:
                    this.Write("/");
                    break;
                case BinaryOperatorType.Equality:
                    this.Write("==");
                    break;
                case BinaryOperatorType.ExclusiveOr:
                    this.Write("^");
                    break;
                case BinaryOperatorType.GreaterThan:
                    this.Write(">");
                    break;
                case BinaryOperatorType.GreaterThanOrEqual:
                    this.Write(">=");
                    break;
                case BinaryOperatorType.InEquality:
                    this.Write("!=");
                    break;
                case BinaryOperatorType.LessThan:
                    this.Write("<");
                    break;
                case BinaryOperatorType.LessThanOrEqual:
                    this.Write("<=");
                    break;
                case BinaryOperatorType.Modulus:
                    this.Write("%");
                    break;
                case BinaryOperatorType.Multiply:
                    this.Write("*");
                    break;
                case BinaryOperatorType.ShiftLeft:
                    this.Write("<<");
                    break;
                case BinaryOperatorType.ShiftRight:
                    this.Write(">>");
                    break;
                case BinaryOperatorType.Subtract:
                    this.Write("-");
                    break;
                default:
                    throw this.CreateException(binaryOperatorExpression, "Unsupported binary operator: " + binaryOperatorExpression.Operator.ToString());
            }
            this.Write(" ");
            binaryOperatorExpression.Right.AcceptVisitor(this);
        }

        public override void VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            var id = identifierExpression.Identifier;
            this.CheckIdentifier(id, identifierExpression);

            if (this.Locals.ContainsKey(id))
            {
                this.Write(id);
                return;
            }

            IMemberDefinition member = this.ResolveFieldOrMethod(id, identifierExpression.TypeArguments.Count);

            if (member != null)
            {
                MethodDefinition method = member as MethodDefinition;
                FieldDefinition field = member as FieldDefinition;
                bool isStatic = method != null && method.IsStatic || field != null && field.IsStatic;
                if (isStatic)
                {
                    this.Write(Helpers.GetScriptFullName(member.DeclaringType));
                }
                else
                {
                    this.WriteThis();
                }

                this.Write(".");

                if (method != null)
                {
                    this.Write(Helpers.GetScriptName(method));
                }
                else
                {
                    this.Write(id);
                }
                return;
            }

            string resolved = this.ResolveNamespaceOrType(id, true);
            if (!String.IsNullOrEmpty(resolved))
            {
                if (this.TypeDefinitions.ContainsKey(resolved))
                {
                    resolved = this.ShortenTypeName(resolved);
                }
                this.Write(resolved);
                return;
            }

            throw this.CreateException(identifierExpression, "Cannot resolve identifier: " + id);
        }

        public override void VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            PropertyDeclaration prop = this.GetPropertyMember(memberReferenceExpression);

            if (prop != null)
            {
                string inline = this.GetInline(this.IsAssignment ? prop.Setter : prop.Getter);
                if (!string.IsNullOrEmpty(inline))
                {
                    this.Write(inline);
                }
                else
                {
                    memberReferenceExpression.Target.AcceptVisitor(this);
                    this.Write(".");
                    this.Write(this.IsAssignment ? "set" : "get");
                    this.Write(memberReferenceExpression.MemberName);
                    this.Write("(");

                    if (!this.IsAssignment)
                    {
                        this.Write(")");
                    }
                    else
                    {
                        this.CloseAssignment = true;
                    }
                }
            }
            else
            {
                memberReferenceExpression.Target.AcceptVisitor(this);
                this.Write(".");
                this.Write(Helpers.GetScriptName(memberReferenceExpression));
            }
        }

        public override void VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            this.WriteThis();
        }

        public override void VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            this.Write("base");
        }

        public override void VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            string inlineCode = this.GetInlineCode(invocationExpression);

            if (!String.IsNullOrEmpty(inlineCode))
            {
                this.Write("");
                StringBuilder savedBuilder = this.Output;
                var args = new List<string>(invocationExpression.Arguments.Count);
                foreach (var arg in invocationExpression.Arguments)
                {
                    this.Output = new StringBuilder();
                    arg.AcceptVisitor(this);
                    args.Add(this.Output.ToString());
                }
                this.Output = savedBuilder;
                this.Output.Append(String.Format(inlineCode, args.ToArray()));
                return;
            }

            MemberReferenceExpression targetMember = invocationExpression.Target as MemberReferenceExpression;

            if (targetMember != null && targetMember.Target is BaseReferenceExpression)
            {
                var baseType = this.GetBaseMethodOwnerTypeDefinition(targetMember.MemberName, targetMember.TypeArguments.Count);
                string currentMethod = "";
                var method = invocationExpression.GetParent<MethodDeclaration>();
                if (method != null)
                {
                    currentMethod = method.Name;
                }

                if (this.Validator.IsIgnoreType(baseType))
                {
                    throw this.CreateException(targetMember.Target, "Cannot call base method, because parent class code is ignored");
                }

                string baseMethod = Helpers.GetScriptName(targetMember);
                bool needComma = false;

                if (currentMethod == baseMethod)
                {
                    this.WriteThis();
                    this.Write(".base(");
                }
                else
                {
                    this.Write(Helpers.GetScriptFullName(baseType), ".", baseMethod);
                    this.Write(".call(");
                    this.WriteThis();
                    needComma = true;
                }

                foreach (var arg in invocationExpression.Arguments)
                {
                    if (needComma)
                    {
                        this.WriteComma();
                    }
                    needComma = true;
                    arg.AcceptVisitor(this);
                }
                this.Write(")");
            }
            else
            {
                invocationExpression.Target.AcceptVisitor(this);
                this.Write("(");
                this.EmitExpressionList(invocationExpression.Arguments);
                this.Write(")");
            }
        }

        public override void VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            this.IsAssignment = true;
            assignmentExpression.Left.AcceptVisitor(this);
            this.IsAssignment = false;
            if (!this.CloseAssignment)
            {
                this.Write(" ");
            }
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
                    throw this.CreateException(assignmentExpression, "Unsupported assignment operator: " + assignmentExpression.Operator.ToString());
            }

            if (!this.CloseAssignment)
            {
                this.Write("= ");
            }
            assignmentExpression.Right.AcceptVisitor(this);

            if (this.CloseAssignment)
            {
                this.Write(")");
            }
            this.CloseAssignment = false;
        }

        public override void VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            this.EmitTypeReference(typeReferenceExpression.Type);
        }

        public override void VisitComposedType(ComposedType composedType)
        {
            this.EmitTypeReference(composedType);
        }

        public override void VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            if (arrayCreateExpression.Arguments.Count > 1)
            {
                throw this.CreateException(arrayCreateExpression, "Multi-dimensional arrays are not supported");
            }

            this.Write("[ ");
            var elements = arrayCreateExpression.Initializer.Elements;
            this.EmitExpressionList(elements);
            if (elements.Count > 0)
            {
                this.Write(" ");
            }
            this.Write("]");
        }

        public override void VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            this.EmitLambda(lambdaExpression.Parameters, lambdaExpression.Body, lambdaExpression);
        }

        public override void VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            this.EmitLambda(anonymousMethodExpression.Parameters, anonymousMethodExpression.Body, anonymousMethodExpression);
        }

        public override void VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            var type = this.GetTypeDefinition(objectCreateExpression.Type);
            if (type.BaseType != null && type.BaseType.FullName == "System.MulticastDelegate")
            {
                objectCreateExpression.Arguments.First().AcceptVisitor(this);
                return;
            }

            var customCtor = this.Validator.GetCustomConstructor(type) ?? "";
            var hasInitializer = !objectCreateExpression.Initializer.IsNull && objectCreateExpression.Initializer.Elements.Count > 0;

            if (Regex.Match(customCtor, @"\s*\{\s*\}\s*").Success)
            {
                this.Write("{ ");
                if (hasInitializer)
                {
                    this.WriteObjectInitializer(objectCreateExpression.Initializer.Elements);
                    this.Write(" }");
                }
                else
                {
                    this.Write("}");
                }
            }
            else
            {
                if (hasInitializer)
                {
                    this.Write(Emitter.ROOT, ".", Emitter.APPLY_OBJECT, "(");
                }

                if (String.IsNullOrEmpty(customCtor))
                {
                    this.Write("new ");
                    objectCreateExpression.Type.AcceptVisitor(this);
                }
                else
                {
                    this.Write(customCtor);
                }

                this.Write("(");
                this.EmitExpressionList(objectCreateExpression.Arguments);
                this.Write(")");

                if (hasInitializer)
                {
                    this.WriteComma();
                    this.Write("{ ");
                    var elements = objectCreateExpression.Initializer.Elements;
                    bool needComma = false;
                    foreach (NamedArgumentExpression item in elements)
                    {
                        if (needComma)
                        {
                            this.WriteComma();
                        }
                        needComma = true;
                        this.Write(item.Name, ": ");
                        item.Expression.AcceptVisitor(this);
                    }
                    this.Write(" }");
                    this.Write(")");
                }
            }
        }


        public override void VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            this.Write("if(");
            ifElseStatement.Condition.AcceptVisitor(this);
            this.Write(")");
            this.EmitBlockOrIndentedLine(ifElseStatement.TrueStatement);
            if (ifElseStatement.FalseStatement != null && !ifElseStatement.FalseStatement.IsNull)
            {
                this.Write("else");
                this.EmitBlockOrIndentedLine(ifElseStatement.FalseStatement);
            }
        }

        public override void VisitForStatement(ForStatement forStatement)
        {
            if (forStatement.Initializers.Count > 1)
            {
                throw this.CreateException(forStatement, "Too many initializers");
            }

            this.PushLocals();

            this.EnableSemicolon = false;
            this.Write("for(");

            foreach (var item in forStatement.Initializers)
            {
                if (item != forStatement.Initializers.First())
                {
                    WriteComma();
                }
                item.AcceptVisitor(this);
            }
            this.Write("; ");

            forStatement.Condition.AcceptVisitor(this);
            Write("; ");

            foreach (var item in forStatement.Iterators)
            {
                if (item != forStatement.Iterators.First())
                {
                    this.WriteComma();
                }
                item.AcceptVisitor(this);
            }
            this.Write(")");
            this.EnableSemicolon = true;

            this.EmitBlockOrIndentedLine(forStatement.EmbeddedStatement);

            this.PopLocals();
        }

        public override void VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            if (indexerExpression.Arguments.Count != 1)
            {
                throw this.CreateException(indexerExpression, "Only one index is supported");
            }

            indexerExpression.Target.AcceptVisitor(this);

            var index = indexerExpression.Arguments.First();

            var primitive = index as PrimitiveExpression;
            if (primitive != null && primitive.Value != null && Regex.Match(primitive.Value.ToString(), "^[_$a-z][_$a-z0-9]*$", RegexOptions.IgnoreCase).Success)
            {
                this.Write(".", primitive.Value);
            }
            else
            {
                this.Write("[");
                index.AcceptVisitor(this);
                this.Write("]");
            }
        }

        public override void VisitCastExpression(CastExpression castExpression)
        {
            this.Write(Emitter.ROOT);
            this.Write(".");
            this.Write(Emitter.CAST);
            this.Write("(");
            castExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            castExpression.Type.AcceptVisitor(this);
            this.Write(")");
        }

        public override void VisitAsExpression(AsExpression asExpression)
        {
            this.Write(Emitter.ROOT);
            this.Write(".");
            this.Write(Emitter.AS);
            this.Write("(");
            asExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            asExpression.Type.AcceptVisitor(this);
            this.Write(")");
        }

        public override void VisitIsExpression(IsExpression isExpression)
        {
            this.Write(Emitter.ROOT, ".", Emitter.IS, "(");
            isExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            isExpression.Type.AcceptVisitor(this);
            this.Write(")");
        }

        public override void VisitReturnStatement(ReturnStatement returnStatement)
        {
            this.Write("return");
            if (!returnStatement.Expression.IsNull)
            {
                this.Write(" ");
                returnStatement.Expression.AcceptVisitor(this);
            }
            this.Write(";");
            this.NewLine();
        }

        public override void VisitThrowStatement(ThrowStatement throwStatement)
        {
            this.Write("throw ");
            throwStatement.Expression.AcceptVisitor(this);
            this.Write(";");
            this.NewLine();
        }

        public override void VisitForeachStatement(ForeachStatement foreachStatement)
        {
            if (foreachStatement.EmbeddedStatement is EmptyStatement)
            {
                return;
            }

            var iteratorName = this.GetNextIteratorName();

            this.Write("var ", iteratorName, " = ", Emitter.ROOT, ".", Emitter.ITERATOR);
            this.Write("(");
            foreachStatement.InExpression.AcceptVisitor(this);
            this.Write(");");
            this.NewLine();

            this.Write("while(", iteratorName, ".", Emitter.HAS_NEXT, "()", ") ");
            this.BeginBlock();

            this.Write("var ", foreachStatement.VariableName, " = ", iteratorName, ".", Emitter.NEXT, "();");
            this.NewLine();

            this.PushLocals();
            this.Locals.Add(foreachStatement.VariableName, foreachStatement.VariableType);

            BlockStatement block = foreachStatement.EmbeddedStatement as BlockStatement;
            if (block != null)
            {
                block.AcceptChildren(this);
            }
            else
            {
                foreachStatement.EmbeddedStatement.AcceptVisitor(this);
            }

            this.PopLocals();

            this.EndBlock();
            this.NewLine();
        }

        public override void VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            conditionalExpression.Condition.AcceptVisitor(this);
            this.Write(" ? ");
            conditionalExpression.TrueExpression.AcceptVisitor(this);
            this.Write(" : ");
            conditionalExpression.FalseExpression.AcceptVisitor(this);
        }

        public override void VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            if (tryCatchStatement.CatchClauses.Count > 1)
            {
                throw this.CreateException(tryCatchStatement, "Multiple catch statements are not supported");
            }

            this.Write("try ");
            tryCatchStatement.TryBlock.AcceptVisitor(this);
            foreach (var clause in tryCatchStatement.CatchClauses)
            {
                this.PushLocals();

                if (!clause.Type.IsNull)
                {
                    if (this.ResolveType(clause.Type.ToString()) != "System.Exception")
                    {
                        throw this.CreateException(clause, "Only System.Exception type is allowed in catch clauses");
                    }
                }
                var varName = clause.VariableName;
                if (String.IsNullOrEmpty(varName))
                {
                    varName = "$e";
                }
                else
                {
                    this.Locals.Add(varName, clause.Type);
                }
                this.Write("catch(", varName, ") ");
                clause.Body.AcceptVisitor(this);

                this.PopLocals();
            }
            if (!tryCatchStatement.FinallyBlock.IsNull)
            {
                this.Write("finally ");
                tryCatchStatement.FinallyBlock.AcceptVisitor(this);
            }
        }

        public override void VisitWhileStatement(WhileStatement whileStatement)
        {
            this.Write("while(");
            whileStatement.Condition.AcceptVisitor(this);
            this.Write(")");
            this.EmitBlockOrIndentedLine(whileStatement.EmbeddedStatement);
        }

        public override void VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            this.Write("do");
            this.EmitBlockOrIndentedLine(doWhileStatement.EmbeddedStatement);
            if (doWhileStatement.EmbeddedStatement is BlockStatement)
            {
                Write(" ");
            }
            this.Write("while(");
            doWhileStatement.Condition.AcceptVisitor(this);
            this.Write(");");
            this.NewLine();
        }

        public override void VisitSwitchStatement(SwitchStatement switchStatement)
        {
            this.Write("switch(");
            switchStatement.Expression.AcceptVisitor(this);
            this.Write(") ");
            this.BeginBlock();
            switchStatement.SwitchSections.ToList().ForEach(s => s.AcceptVisitor(this));
            this.EndBlock();
            this.NewLine();
        }

        public override void VisitSwitchSection(SwitchSection switchSection)
        {
            switchSection.CaseLabels.ToList().ForEach(l => l.AcceptVisitor(this));
            this.Indent();
            switchSection.AcceptChildren(this);
            this.Outdent();
        }

        public override void VisitCaseLabel(CaseLabel caseLabel)
        {
            if (caseLabel.Expression.IsNull)
            {
                this.Write("default");
            }
            else
            {
                this.Write("case ");
                caseLabel.Expression.AcceptVisitor(this);
            }
            this.Write(":");
            this.NewLine();
        }

        public override void VisitBreakStatement(BreakStatement breakStatement)
        {
            this.Write("break;");
            this.NewLine();
        }

        public override void VisitContinueStatement(ContinueStatement continueStatement)
        {
            this.Write("continue;");
            this.NewLine();
        }

        public override void VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            this.Write("(");
            parenthesizedExpression.Expression.AcceptVisitor(this);
            this.Write(")");
        }

        public override void VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            typeOfExpression.Type.AcceptVisitor(this);
        }

        public override void VisitComment(Comment comment)
        {
            if (comment.CommentType != CommentType.SingleLine
                && comment.CommentType != CommentType.MultiLine)
            {
                return;
            }

            this.WriteComment(comment.Content);            
        }
    }
}
