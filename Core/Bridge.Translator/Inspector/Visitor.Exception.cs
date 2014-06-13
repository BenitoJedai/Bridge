using System;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;

namespace Bridge.NET
{
    public abstract partial class Visitor : IAstVisitor 
    {
        protected Exception CreateException(AstNode node, string message) 
        {
            if (String.IsNullOrEmpty(message))
            {
                message = String.Format("Language construction {0} is not supported", node.GetType().Name);
            }

            return Exception.Create("{0} {1}: {2}", message, node.StartLocation, node.GetText()); 
        }

        protected Exception CreateException(AstNode node) 
        {
            return this.CreateException(node, null);
        }

        public virtual void VisitAccessor(Accessor accessor)
        {
            throw this.CreateException(accessor);
        }

        public virtual void VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            throw this.CreateException(anonymousMethodExpression);
        }

        public virtual void VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            throw this.CreateException(anonymousTypeCreateExpression);
        }

        public virtual void VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            throw this.CreateException(arrayCreateExpression);
        }

        public virtual void VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            throw this.CreateException(arrayInitializerExpression);
        }

        public virtual void VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            throw this.CreateException(arraySpecifier);
        }

        public virtual void VisitAsExpression(AsExpression asExpression)
        {
            throw this.CreateException(asExpression);
        }

        public virtual void VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            throw this.CreateException(assignmentExpression);
        }

        public virtual void VisitAttribute(ICSharpCode.NRefactory.CSharp.Attribute attribute)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitAttributeSection(AttributeSection attributeSection)
        {
            //throw this.CreateException(attributeSection);
        }

        public virtual void VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            throw this.CreateException(baseReferenceExpression);
        }

        public virtual void VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            throw this.CreateException(binaryOperatorExpression);
        }

        public virtual void VisitBlockStatement(BlockStatement blockStatement)
        {
            throw this.CreateException(blockStatement);
        }

        public virtual void VisitBreakStatement(BreakStatement breakStatement)
        {
            throw this.CreateException(breakStatement);
        }

        public virtual void VisitCaseLabel(CaseLabel caseLabel)
        {
            throw this.CreateException(caseLabel);
        }

        public virtual void VisitCastExpression(CastExpression castExpression)
        {
            throw this.CreateException(castExpression);
        }

        public virtual void VisitCatchClause(CatchClause catchClause)
        {
            throw this.CreateException(catchClause);
        }

        public virtual void VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            throw this.CreateException(checkedExpression);
        }

        public virtual void VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            throw this.CreateException(checkedStatement);
        }

        public virtual void VisitComposedType(ComposedType composedType)
        {
            throw this.CreateException(composedType);
        }

        public virtual void VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            throw this.CreateException(conditionalExpression);
        }

        public virtual void VisitConstraint(Constraint constraint)
        {
            throw this.CreateException(constraint);
        }

        public virtual void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            throw this.CreateException(constructorDeclaration);
        }

        public virtual void VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            throw this.CreateException(constructorInitializer);
        }

        public virtual void VisitContinueStatement(ContinueStatement continueStatement)
        {
            throw this.CreateException(continueStatement);
        }

        public virtual void VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            throw this.CreateException(customEventDeclaration);
        }

        public virtual void VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            throw this.CreateException(delegateDeclaration);
        }

        public virtual void VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            throw this.CreateException(destructorDeclaration);
        }

        public virtual void VisitDirectionExpression(DirectionExpression directionExpression)
        {
            throw this.CreateException(directionExpression);
        }

        public virtual void VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            throw this.CreateException(doWhileStatement);
        }

        public virtual void VisitDocumentationReference(DocumentationReference documentationReference)
        {
            throw this.CreateException(documentationReference);
        }

        public virtual void VisitEmptyExpression(EmptyExpression emptyExpression)
        {
            throw this.CreateException(emptyExpression);
        }

        public virtual void VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            throw this.CreateException(emptyStatement);
        }

        public virtual void VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            throw this.CreateException(enumMemberDeclaration);
        }

        public virtual void VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            throw this.CreateException(eventDeclaration);
        }

        public virtual void VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            throw this.CreateException(expressionStatement);
        }

        public virtual void VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            throw this.CreateException(externAliasDeclaration);
        }

        public virtual void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            throw this.CreateException(fieldDeclaration);
        }

        public virtual void VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            throw this.CreateException(fixedFieldDeclaration);
        }

        public virtual void VisitFixedStatement(FixedStatement fixedStatement)
        {
            throw this.CreateException(fixedStatement);
        }

        public virtual void VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            throw this.CreateException(fixedVariableInitializer);
        }

        public virtual void VisitForStatement(ForStatement forStatement)
        {
            throw this.CreateException(forStatement);
        }

        public virtual void VisitForeachStatement(ForeachStatement foreachStatement)
        {
            throw this.CreateException(foreachStatement);
        }

        public virtual void VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            throw this.CreateException(gotoCaseStatement);
        }

        public virtual void VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            throw this.CreateException(gotoDefaultStatement);
        }

        public virtual void VisitGotoStatement(GotoStatement gotoStatement)
        {
            throw this.CreateException(gotoStatement);
        }

        public virtual void VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            throw this.CreateException(identifierExpression);
        }

        public virtual void VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            throw this.CreateException(ifElseStatement);
        }

        public virtual void VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            throw this.CreateException(indexerDeclaration);
        }

        public virtual void VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            throw this.CreateException(indexerExpression);
        }

        public virtual void VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            throw this.CreateException(invocationExpression);
        }

        public virtual void VisitIsExpression(IsExpression isExpression)
        {
            throw this.CreateException(isExpression);
        }

        public virtual void VisitLabelStatement(LabelStatement labelStatement)
        {
            throw this.CreateException(labelStatement);
        }

        public virtual void VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            throw this.CreateException(lambdaExpression);
        }

        public virtual void VisitLockStatement(LockStatement lockStatement)
        {
            throw this.CreateException(lockStatement);
        }

        public virtual void VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            throw this.CreateException(memberReferenceExpression);
        }

        public virtual void VisitMemberType(MemberType memberType)
        {
            throw this.CreateException(memberType);
        }

        public virtual void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            throw this.CreateException(methodDeclaration);
        }

        public virtual void VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            throw this.CreateException(namedArgumentExpression);
        }

        public virtual void VisitNamedExpression(NamedExpression namedExpression)
        {
            throw this.CreateException(namedExpression);
        }

        public virtual void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            throw this.CreateException(namespaceDeclaration);
        }        

        public virtual void VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            throw this.CreateException(objectCreateExpression);
        }

        public virtual void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            throw this.CreateException(operatorDeclaration);
        }

        public virtual void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            throw this.CreateException(parameterDeclaration);
        }

        public virtual void VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            throw this.CreateException(parenthesizedExpression);
        }

        public virtual void VisitPatternPlaceholder(AstNode placeholder, ICSharpCode.NRefactory.PatternMatching.Pattern pattern)
        {
            throw new NotImplementedException();
        }

        public virtual void VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            throw this.CreateException(pointerReferenceExpression);
        }

        public virtual void VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            throw this.CreateException(primitiveExpression);
        }

        public virtual void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            throw this.CreateException(propertyDeclaration);
        }

        public virtual void VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            throw this.CreateException(queryContinuationClause);
        }

        public virtual void VisitQueryExpression(QueryExpression queryExpression)
        {
            throw this.CreateException(queryExpression);
        }

        public virtual void VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            throw this.CreateException(queryFromClause);
        }

        public virtual void VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            throw this.CreateException(queryGroupClause);
        }

        public virtual void VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            throw this.CreateException(queryJoinClause);
        }

        public virtual void VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            throw this.CreateException(queryLetClause);
        }

        public virtual void VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            throw this.CreateException(queryOrderClause);
        }

        public virtual void VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            throw this.CreateException(queryOrdering);
        }

        public virtual void VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            throw this.CreateException(querySelectClause);
        }

        public virtual void VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            throw this.CreateException(queryWhereClause);
        }

        public virtual void VisitReturnStatement(ReturnStatement returnStatement)
        {
            throw this.CreateException(returnStatement);
        }

        public virtual void VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            throw this.CreateException(sizeOfExpression);
        }

        public virtual void VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            throw this.CreateException(stackAllocExpression);
        }

        public virtual void VisitSwitchSection(SwitchSection switchSection)
        {
            throw this.CreateException(switchSection);
        }

        public virtual void VisitSwitchStatement(SwitchStatement switchStatement)
        {
            throw this.CreateException(switchStatement);
        }

        public virtual void VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            throw this.CreateException(syntaxTree);
        }

        public virtual void VisitText(TextNode textNode)
        {
            throw this.CreateException(textNode);
        }

        public virtual void VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            throw this.CreateException(thisReferenceExpression);
        }

        public virtual void VisitThrowStatement(ThrowStatement throwStatement)
        {
            throw this.CreateException(throwStatement);
        }

        public virtual void VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            throw this.CreateException(tryCatchStatement);
        }

        public virtual void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            throw this.CreateException(typeDeclaration);
        }

        public virtual void VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            throw this.CreateException(typeOfExpression);
        }

        public virtual void VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            throw this.CreateException(typeReferenceExpression);
        }

        public virtual void VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            throw this.CreateException(unaryOperatorExpression);
        }

        public virtual void VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            throw this.CreateException(uncheckedExpression);
        }

        public virtual void VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            throw this.CreateException(uncheckedStatement);
        }

        public virtual void VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            throw this.CreateException(undocumentedExpression);
        }

        public virtual void VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            throw this.CreateException(unsafeStatement);
        }

        public virtual void VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw this.CreateException(usingAliasDeclaration);
        }

        public virtual void VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            throw this.CreateException(usingDeclaration);
        }

        public virtual void VisitUsingStatement(UsingStatement usingStatement)
        {
            throw this.CreateException(usingStatement);
        }

        public virtual void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            throw this.CreateException(variableDeclarationStatement);
        }

        public virtual void VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            throw this.CreateException(variableInitializer);
        }

        public virtual void VisitWhileStatement(WhileStatement whileStatement)
        {
            throw this.CreateException(whileStatement);
        }

        public virtual void VisitWhitespace(WhitespaceNode whitespaceNode)
        {
            throw this.CreateException(whitespaceNode);
        }

        public virtual void VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            throw this.CreateException(yieldBreakStatement);
        }

        public virtual void VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            throw this.CreateException(yieldReturnStatement);
        }        
    }
}