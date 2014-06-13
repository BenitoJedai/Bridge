using Ext.Net.Utilities;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bridge.NET
{
    public partial class Emitter : Visitor
    {
        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            this.EnsureComma();
            this.ResetLocals();
            this.AddLocals(methodDeclaration.Parameters);

            if (this.MethodsGroup != null)
            {
                MethodDefinition methodDef = this.FindMethodDefinitionInGroup(methodDeclaration.Parameters, methodDeclaration.TypeParameters, this.MethodsGroup);
                string name = this.GetOverloadName(methodDef);
                this.EmitMethodDetector(this.MethodsGroupBuilder, methodDef, name);

                this.Write(name);
            }
            else
            {
                this.Write(this.GetEntityName(methodDeclaration));
            }

            this.WriteColon();
            this.WriteFunction();
            
            this.EmitMethodParameters(methodDeclaration.Parameters, methodDeclaration);
            
            this.WriteSpace();

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
                    this.WriteNewLine();
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
            var addEndBlock = false;
            this.PushLocals();
            this.BeginBlock();

            if (this.InjectMethodDetectors)
            {
                this.InjectMethodDetectors = false;

                string detectors = this.GetMethodsDetector(this.MethodsGroupBuilder);
                bool noChildren = blockStatement.Children.ToList().Count == 0;

                if (noChildren)
                {
                    detectors = Ext.Net.Utilities.StringUtils.ReplaceLastInstanceOf(detectors, Environment.NewLine, "");
                }
                
                detectors = this.WriteIndentToString(detectors);

                this.Write(detectors);

                if (noChildren)
                {
                    this.WriteNewLine();
                }
                else
                {
                    this.Write("else if (arguments.length == 0)");
                    this.WriteSpace();
                    this.BeginBlock();
                    addEndBlock = true;
                }
            }

            blockStatement.Children.ToList().ForEach(child => child.AcceptVisitor(this));
            this.EndBlock();

            if (addEndBlock)
            {
                this.WriteNewLine();
                this.EndBlock();                
            }

            if (!this.KeepLineAfterBlock(blockStatement))
            {
                this.WriteNewLine();
            }

            this.PopLocals();
        }

        public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {            
            bool needVar = true;
            bool needComma = false;

            foreach (var variable in variableDeclarationStatement.Variables)
            {                
                this.CheckIdentifier(variable.Name, variableDeclarationStatement);
                this.Locals.Add(variable.Name, variableDeclarationStatement.Type);

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
                    variable.Initializer.AcceptVisitor(this);
                }
            }

            if (this.EnableSemicolon && !needVar)
            {
                this.WriteSemiColon(true);
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
                this.WriteSemiColon(true);
            }
        }

        public override void VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            this.WriteSemiColon(true);
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

        protected virtual bool ResolveOperator(BinaryOperatorExpression binaryOperatorExpression)
        {            
            var resolveOperator = this.Resolver.ResolveNode(binaryOperatorExpression, this);
            
            if (resolveOperator != null && resolveOperator is OperatorResolveResult)
            {
                var orr = (OperatorResolveResult)resolveOperator;

                if (orr.UserDefinedOperatorMethod != null)
                {
                    var inline = this.GetInline(orr.UserDefinedOperatorMethod);

                    if (!string.IsNullOrWhiteSpace(inline))
                    {
                        this.PushWriter(inline);
                        this.EmitExpressionList(new[] { binaryOperatorExpression.Left, binaryOperatorExpression.Right }, null);
                        this.PopWriter();

                        return true;
                    }
                }
            }

            return false;
        }

        public override void VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            var delegateOperator = false;
            if (this.ResolveOperator(binaryOperatorExpression))
            {
                return;
            }
            else if (binaryOperatorExpression.Operator == BinaryOperatorType.Add ||
                binaryOperatorExpression.Operator == BinaryOperatorType.Subtract)
            {
                var leftResolverResult = this.Resolver.ResolveNode(binaryOperatorExpression.Left, this);
                var rightResolverResult = this.Resolver.ResolveNode(binaryOperatorExpression.Right, this);
                var add = binaryOperatorExpression.Operator == BinaryOperatorType.Add;

                if (this.Validator.IsDelegateOrLambda(leftResolverResult) && this.Validator.IsDelegateOrLambda(rightResolverResult))
                {
                    delegateOperator = true;
                    this.Write(Emitter.ROOT + "." + (add ? Emitter.DELEGATE_COMBINE : Emitter.DELEGATE_REMOVE));
                    this.WriteOpenParentheses();
                }
            }    
            
            binaryOperatorExpression.Left.AcceptVisitor(this);
            if (!delegateOperator)
            {
                this.WriteSpace();

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
                        this.Write("===");
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
                        this.Write("!==");
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
            }
            else
            {
                this.WriteComma();
            }  

            this.WriteSpace();
            binaryOperatorExpression.Right.AcceptVisitor(this);

            if (delegateOperator)
            {
                this.WriteCloseParentheses();
            }
        }

        public override void VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            var id = identifierExpression.Identifier;
            this.CheckIdentifier(id, identifierExpression);

            var resolveResult = this.Resolver.ResolveNode(identifierExpression, this);
            var isResolved = resolveResult != null && !(resolveResult is ErrorResolveResult);
            var memberResult = resolveResult as MemberResolveResult;         

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
                string appendAdditionalCode = null;
                
                if (method != null)
                {
                    string inline = this.GetInline(method);

                    if (!string.IsNullOrWhiteSpace(inline))
                    {
                        
                        return;
                    }
                    
                    if (memberResult != null &&
                         memberResult.Member is IMethod &&
                         !(
                            identifierExpression.Parent is InvocationExpression &&
                            identifierExpression.NextSibling != null &&
                            identifierExpression.NextSibling.Role is TokenRole &&
                            ((TokenRole)identifierExpression.NextSibling.Role).Token == "("
                         )
                    )
                    {
                        var resolvedMethod = (IMethod)memberResult.Member;
                        var isExtensionMethod = resolvedMethod.IsExtensionMethod;
                        this.Write(Emitter.ROOT + "." + (isExtensionMethod ? Emitter.DELEGATE_BIND_SCOPE : Emitter.DELEGATE_BIND) + "(");

                        if (isStatic)
                        {
                            this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(member.DeclaringType)));
                        }
                        else
                        {
                            this.WriteThis();
                        }

                        this.Write(", ");
                        appendAdditionalCode = ")";
                    }
                }

                if (isStatic)
                {
                    this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(member.DeclaringType)));
                }
                else
                {
                    this.WriteThis();
                }

                this.WriteDot();

                if (method != null)
                {
                    if (resolveResult is InvocationResolveResult)
                    {
                        InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                        this.Write(this.GetOverloadNameInvocationResolveResult(invocationResult));    
                    }
                    else
                    {
                        this.Write(this.GetMethodName(method));
                    }

                    if (appendAdditionalCode != null)
                    {
                        this.Write(appendAdditionalCode);
                    }
                }
                else
                {
                    bool isConst = this.IsMemberConst(memberResult.Member);

                    if (isConst && this.IsInlineConst(memberResult.Member))
                    {
                        this.WriteScript(memberResult.ConstantValue);
                    }
                    else
                    {
                        bool changeCase = (!this.IsNativeMember(memberResult.Member.FullName) ? this.ChangeCase : true) && !isConst;
                        this.Write(this.GetEntityName(memberResult.Member, !changeCase));
                    }                    
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

            if (memberResult != null && memberResult.Member.EntityType == EntityType.Property && memberResult.TargetResult.Type.Kind != TypeKind.Anonymous)
            {
                if (memberResult.Member.IsStatic)
                {
                    this.Write(this.ShortenTypeName(Helpers.ReplaceSpecialChars(memberResult.Member.DeclaringType.FullName)));
                }
                else
                {
                    this.WriteThis();
                }

                this.WriteDot();

                if (!this.IsAssignment)
                {
                    this.Write("get");
                    this.Write(id);
                    this.WriteOpenParentheses();
                    this.WriteCloseParentheses();
                }
                else
                {
                    this.PushWriter("set" + id + "({0})");
                }
            }
            else
            {
                throw this.CreateException(identifierExpression, "Cannot resolve identifier: " + id);
            }
        }        

        public override void VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            var resolveResult = this.Resolver.ResolveNode(memberReferenceExpression, this);

            if (resolveResult == null && !(resolveResult is ErrorResolveResult))
            {
                memberReferenceExpression.Target.AcceptVisitor(this);
                this.WriteDot();
                string name = Helpers.GetScriptName(memberReferenceExpression, false);
                this.Write(name.ToLowerCamelCase());
                return;
            }

            if (resolveResult is DynamicInvocationResolveResult)
            {
                resolveResult = ((DynamicInvocationResolveResult)resolveResult).Target;
            }

            if (resolveResult is MethodGroupResolveResult)
            {
                resolveResult = this.Resolver.ResolveNode(memberReferenceExpression.Parent, this);
            }

            MemberResolveResult member = resolveResult as MemberResolveResult;           
            
            string inline = member != null ? this.GetInline(member.Member) : null;
            bool hasInline = !string.IsNullOrEmpty(inline);
            bool hasThis = hasInline && inline.Contains("{this}");
            string appendAdditionalCode = null;

            if (hasThis) 
            {
                this.Write("");
                var oldBuilder = this.Output;
                this.Output = new StringBuilder();
                memberReferenceExpression.Target.AcceptVisitor(this);
                inline = inline.Replace("{this}", this.Output.ToString());
                this.Output = oldBuilder;

                if (resolveResult is InvocationResolveResult)
                {
                    this.PushWriter(inline);
                }
                else
                {
                    this.Write(inline);
                }
                
                return;
            }

            if (hasInline && member.Member.IsStatic)
            {
                this.PushWriter(inline);                
            }
            else
            {
                if (member != null && member.IsCompileTimeConstant && member.Member.DeclaringType.Kind == TypeKind.Enum)
                {
                    var typeDef = member.Member.DeclaringType as DefaultResolvedTypeDefinition;

                    if (typeDef != null)
                    {
                        var enumMode = this.Validator.EnumEmitMode(typeDef);

                        if ((this.Validator.IsIgnoreType(typeDef) && enumMode == -1) || enumMode == 2)
                        {
                            this.WriteScript(member.ConstantValue);
                            return;
                        }
                        
                        if (enumMode >= 3) 
                        {
                            string enumStringName = member.Member.Name;
                            var attr = this.GetAttribute(member.Member.Attributes, Translator.CLR_ASSEMBLY + ".NameAttribute");

                            if (attr != null)
                            {
                                enumStringName = this.GetEntityName(member.Member);
                            }
                            else
                            {
                                switch (enumMode)
                                {
                                    case 3:
                                        enumStringName = Ext.Net.Utilities.StringUtils.ToLowerCamelCase(member.Member.Name);
                                        break;
                                    case 4:
                                        break;
                                    case 5:
                                        enumStringName = enumStringName.ToLowerInvariant();
                                        break;
                                    case 6:
                                        enumStringName = enumStringName.ToUpperInvariant();
                                        break;
                                }
                            }
                            
                            this.WriteScript(enumStringName);
                            return;
                        }
                    }
                }
                
                if (resolveResult is TypeResolveResult)
                {
                    TypeResolveResult typeResolveResult = (TypeResolveResult)resolveResult;

                    this.Write(this.ShortenTypeName(typeResolveResult.Type.FullName));
                    return;
                }
                else if (member != null &&
                         member.Member is IMethod && 
                         !(
                            memberReferenceExpression.Parent is InvocationExpression && 
                            memberReferenceExpression.NextSibling != null && 
                            memberReferenceExpression.NextSibling.Role is TokenRole &&
                            ((TokenRole)memberReferenceExpression.NextSibling.Role).Token == "("
                         )
                    )
                {
                    var resolvedMethod = (IMethod)member.Member;

                    var isExtensionMethod = resolvedMethod.IsExtensionMethod;

                    this.Write(Emitter.ROOT + "." + (isExtensionMethod ? Emitter.DELEGATE_BIND_SCOPE : Emitter.DELEGATE_BIND ) + "(");
                    memberReferenceExpression.Target.AcceptVisitor(this);
                    this.Write(", ");

                    if (isExtensionMethod)
                    {
                        this.Write(this.ShortenTypeName(resolvedMethod.DeclaringType.FullName));
                    }
                    else
                    {
                        memberReferenceExpression.Target.AcceptVisitor(this);
                    }

                    appendAdditionalCode = ")";
                }
                else
                {
                    memberReferenceExpression.Target.AcceptVisitor(this);
                }
                
                
                this.WriteDot();

                if (member == null)
                {
                    this.Write(Helpers.GetScriptName(memberReferenceExpression, false));
                }
                else if (!string.IsNullOrEmpty(inline))
                {
                    if (resolveResult is InvocationResolveResult)
                    {
                        this.PushWriter(inline);
                    }
                    else
                    {
                        this.Write(inline);
                    }
                }
                else if (member.Member.EntityType == EntityType.Property && member.TargetResult.Type.Kind != TypeKind.Anonymous && !this.Validator.IsObjectLiteral(member.Member.DeclaringTypeDefinition))
                {
                    if (!this.IsAssignment)
                    {
                        this.Write("get");
                        this.Write(memberReferenceExpression.MemberName);
                        this.WriteOpenParentheses();
                        this.WriteCloseParentheses();
                    }
                    else
                    {
                        this.PushWriter("set" + memberReferenceExpression.MemberName + "({0})");
                    }
                }
                else if (member.Member.EntityType == EntityType.Field)
                {                    
                    bool isConst = this.IsMemberConst(member.Member);

                    if (isConst && this.IsInlineConst(member.Member))
                    {
                        this.WriteScript(member.ConstantValue);
                    }
                    else
                    {
                        bool changeCase = (!this.IsNativeMember(member.Member.FullName) ? this.ChangeCase : true) && !isConst;
                        this.Write(this.GetEntityName(member.Member, !changeCase));
                    }                    
                }
                else if (resolveResult is InvocationResolveResult)
                {                    
                    InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                    this.Write(this.GetOverloadNameInvocationResolveResult(invocationResult));    
                }
                else
                {
                    this.Write(this.GetEntityName(member.Member));

                    if (appendAdditionalCode != null)
                    {
                        this.Write(appendAdditionalCode);
                    }
                }                    
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
            Tuple<bool, bool, string> inlineInfo = this.GetInlineCode(invocationExpression);
            var tupleArguments = this.GetArgumentsList(invocationExpression);
            var argsExpressions = tupleArguments.Item1;
            var paramsArg = tupleArguments.Item2;
            var argsCount = argsExpressions.Count();

            if (inlineInfo != null)
            {
                bool isStaticMethod = inlineInfo.Item1;
                bool isInlineMethod = inlineInfo.Item2;
                string inlineScript = inlineInfo.Item3;

                if (isInlineMethod)
                {
                    if (invocationExpression.Arguments.Count == 1)
                    {
                        var code = invocationExpression.Arguments.First();
                        var inlineExpression = code as PrimitiveExpression;
                        if (inlineExpression == null)
                        {
                            throw new Exception("Only primitive expression can be inlined: " + inlineExpression.GetText());
                        }

                        this.Write(inlineExpression.Value);
                        return;
                    }
                }
                else if (!String.IsNullOrEmpty(inlineScript) && invocationExpression.Target is IdentifierExpression)
                {
                    this.Write("");
                    StringBuilder savedBuilder = this.Output;

                    var args = new List<string>(argsCount);

                    foreach (var arg in argsExpressions)
                    {
                        this.Output = new StringBuilder();                        
                        arg.AcceptVisitor(this);
                        args.Add((arg == paramsArg ? "[" : "") + this.Output.ToString());
                    }

                    if (paramsArg != null)
                    {
                        args[args.Count - 1] = args[args.Count - 1] + "]";
                    }

                    this.Output = savedBuilder;

                    if (!isStaticMethod)
                    {
                        this.WriteThis();
                        this.WriteDot();
                    }

                    this.Output.Append(String.Format(inlineScript, args.ToArray()));

                    if (!isStaticMethod)
                    {
                        invocationExpression.Target.AcceptVisitor(this);
                    }

                    return;
                }
            }

            MemberReferenceExpression targetMember = invocationExpression.Target as MemberReferenceExpression;
            if (targetMember != null)
            {
                var member = this.Resolver.ResolveNode(targetMember.Target, this);

                if (member != null && member.Type.Kind == TypeKind.Delegate)
                {
                    throw this.CreateException(invocationExpression, "Delegate's methods are not supported. Please use direct delegate invoke.");
                }

                var targetResolve = this.Resolver.ResolveNode(targetMember, this);

                if (targetResolve != null)
                {
                    var csharpInvocation = targetResolve as CSharpInvocationResolveResult;
                    InvocationResolveResult invocationResult;
                    
                    if (csharpInvocation != null)
                    {
                        invocationResult = csharpInvocation.IsExtensionMethodInvocation ? csharpInvocation : null;

                        if (this.IsEmptyPartialInvoking(csharpInvocation))
                        {
                            this.SkipSemiColon = true;
                            return;
                        }
                    }
                    else
                    {
                        invocationResult = targetResolve as InvocationResolveResult;

                        if (this.IsEmptyPartialInvoking(invocationResult)) 
                        {
                            this.SkipSemiColon = true;
                            return;
                        }
                    }                    

                    if (invocationResult != null)
                    {
                        var resolvedMethod = invocationResult.Member as IMethod;

                        if (resolvedMethod != null && resolvedMethod.IsExtensionMethod)
                        {
                            string inline = this.GetInline(resolvedMethod);

                            if (!string.IsNullOrWhiteSpace(inline))
                            {
                                this.Write("");
                                StringBuilder savedBuilder = this.Output;
                                var args = new List<string>(argsCount + 1);

                                this.Output = new StringBuilder();
                                this.WriteThisExtension(invocationExpression.Target);
                                args.Add(this.Output.ToString());

                                foreach (var arg in argsExpressions)
                                {
                                    this.Output = new StringBuilder();
                                    arg.AcceptVisitor(this);
                                    args.Add((arg == paramsArg ? "[" : "") + this.Output.ToString());
                                }

                                if (paramsArg != null)
                                {
                                    args[args.Count - 1] = args[args.Count - 1] + "]";
                                }

                                this.Output = savedBuilder;

                                this.Output.Append(String.Format(inline, args.ToArray().Join(", ")));
                            }
                            else
                            {
                                string name = this.ShortenTypeName(Helpers.ReplaceSpecialChars(resolvedMethod.DeclaringType.FullName)) + "." + this.GetEntityName(resolvedMethod);
                                
                                this.Write(name);
                                this.WriteOpenParentheses();

                                this.WriteThisExtension(invocationExpression.Target);

                                if (argsCount > 0)
                                {
                                    this.WriteComma();
                                }

                                this.EmitExpressionList(argsExpressions, paramsArg);

                                this.WriteCloseParentheses();
                            }
                            
                            return;
                        }
                    }
                }
            }

            if (targetMember != null && targetMember.Target is BaseReferenceExpression)
            {
                var baseType = this.GetBaseMethodOwnerTypeDefinition(targetMember.MemberName, targetMember.TypeArguments.Count);
                var method = invocationExpression.GetParent<MethodDeclaration>();
                
                string currentMethod = "";
                
                if (method != null)
                {
                    currentMethod = method.Name;
                }

                if (this.Validator.IsIgnoreType(baseType))
                {
                    throw this.CreateException(targetMember.Target, "Cannot call base method, because parent class code is ignored");
                }

                string baseMethod = Helpers.GetScriptName(targetMember, false);
                bool needComma = false;

                if (currentMethod == baseMethod)
                {
                    this.WriteThis();
                    this.WriteDot();
                    this.Write("base");
                    this.WriteOpenParentheses();
                }
                else
                {
                    var resolveResult = this.Resolver.ResolveNode(targetMember, this);

                    if (resolveResult != null && resolveResult is InvocationResolveResult)
                    {
                        InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                        this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".prototype.", this.GetEntityName(invocationResult.Member));
                    }
                    else
                    {
                        this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".prototype.", this.ChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(baseMethod) : baseMethod);
                    }                    

                    this.WriteDot();
                    this.Write("call");
                    this.WriteOpenParentheses();
                    
                    this.WriteThis();
                    needComma = true;
                }

                foreach (var arg in argsExpressions)
                {
                    if (needComma)
                    {
                        this.WriteComma();
                    }

                    needComma = true;
                    arg.AcceptVisitor(this);
                }

                this.WriteCloseParentheses();
            }
            else
            {
                if (this.IsEmptyPartialInvoking(this.Resolver.ResolveNode(invocationExpression.Target, this) as InvocationResolveResult))
                {
                    this.SkipSemiColon = true;
                    return;
                }                  
                
                int count = this.Writers.Count;
                invocationExpression.Target.AcceptVisitor(this);

                if (this.Writers.Count > count)
                {
                    this.EmitExpressionList(argsExpressions, paramsArg);                    
                    this.PopWriter();
                }
                else
                {
                    this.WriteOpenParentheses();
                    this.EmitExpressionList(argsExpressions, paramsArg);
                    this.WriteCloseParentheses();
                }                
            }
        }        

        public override void VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            var delegateAssigment = false;
            var initCount = this.Writers.Count;

            if (assignmentExpression.Operator == AssignmentOperatorType.Add ||
                assignmentExpression.Operator == AssignmentOperatorType.Subtract)
            {
                var leftResolverResult = this.Resolver.ResolveNode(assignmentExpression.Left, this);
                var rightResolverResult = this.Resolver.ResolveNode(assignmentExpression.Right, this);
                var add = assignmentExpression.Operator == AssignmentOperatorType.Add;

                if (this.Validator.IsDelegateOrLambda(leftResolverResult) && this.Validator.IsDelegateOrLambda(rightResolverResult))
                {                    
                    this.IsAssignment = true;
                    assignmentExpression.Left.AcceptVisitor(this);
                    this.IsAssignment = false;
                    this.Write(" = ");
                    
                    delegateAssigment = true;
                    this.Write(Emitter.ROOT + "." + (add ? Emitter.DELEGATE_COMBINE : Emitter.DELEGATE_REMOVE));
                    this.WriteOpenParentheses();
                }
            }           
            
            this.IsAssignment = true;
            assignmentExpression.Left.AcceptVisitor(this);
            this.IsAssignment = false;            

            if (this.Writers.Count == 0 && !delegateAssigment)
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
                        throw this.CreateException(assignmentExpression, "Unsupported assignment operator: " + assignmentExpression.Operator.ToString());
                }

                int count = this.Writers.Count;

                if (count == 0)
                {
                    this.Write("= ");
                }
            }
            else
            {
                this.WriteComma();
            }  

            assignmentExpression.Right.AcceptVisitor(this);

            if (this.Writers.Count > initCount)
            {
                this.PopWriter();
            }

            if (delegateAssigment)
            {
                this.WriteCloseParentheses();
            }
        }

        public override void VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            this.EmitTypeReference(typeReferenceExpression.Type);
        }

        public override void VisitComposedType(ComposedType composedType)
        {
            this.EmitTypeReference(composedType);
        }

        public override void VisitPrimitiveType(PrimitiveType primitiveType)
        {
            this.EmitTypeReference(primitiveType);
        }

        public override void VisitSimpleType(SimpleType simpleType)
        {
            this.EmitTypeReference(simpleType);
        }

        public override void VisitNamedExpression(NamedExpression namedExpression)
        {
            this.EmitNameExpression(namedExpression.Name, namedExpression, namedExpression.Expression);
        }

        public override void VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            this.BeginBlock();
            var elements = arrayInitializerExpression.Elements;
            this.EmitExpressionList(elements, null);
            this.WriteNewLine();
            this.EndBlock();
        }

        public override void VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            if (arrayCreateExpression.Arguments.Count > 1)
            {
                throw this.CreateException(arrayCreateExpression, "Multi-dimensional arrays are not supported");
            }

            if (arrayCreateExpression.Initializer.IsNull && arrayCreateExpression.Arguments.Count > 0)
            {
                this.Write("new Array(");
                arrayCreateExpression.Arguments.First().AcceptVisitor(this);
                this.Write(")");

                return;
            }

            this.WriteOpenBracket();
            var elements = arrayCreateExpression.Initializer.Elements;
            this.EmitExpressionList(elements, null);
            this.WriteCloseBracket();
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

            var tupleArguments = this.GetArgumentsList(objectCreateExpression);
            var argsExpressions = tupleArguments.Item1;
            var paramsArg = tupleArguments.Item2;
            var argsCount = argsExpressions.Count();

            var invocationResolveResult = this.Resolver.ResolveNode(objectCreateExpression, this) as InvocationResolveResult;
            string inlineCode = null;
            if (invocationResolveResult != null)
            {
                inlineCode = this.GetInline(invocationResolveResult.Member);
            }

            var customCtor = this.Validator.GetCustomConstructor(type) ?? "";            
            var hasInitializer = !objectCreateExpression.Initializer.IsNull && objectCreateExpression.Initializer.Elements.Count > 0;

            if (inlineCode == null && Regex.Match(customCtor, @"\s*\{\s*\}\s*").Success)
            {
                this.WriteOpenBrace();
                this.WriteSpace();

                if (hasInitializer)
                {
                    this.WriteObjectInitializer(objectCreateExpression.Initializer.Elements, this.ChangeCase);

                    this.WriteSpace();
                    this.WriteCloseBrace();
                }
                else
                {
                    this.WriteCloseBrace();
                }
            }
            else
            {
                if (hasInitializer)
                {
                    this.Write(Emitter.ROOT);
                    this.WriteDot();
                    this.Write(Emitter.MERGE_OBJECT);
                    this.WriteOpenParentheses();
                }

                if (inlineCode != null)
                {
                    StringBuilder savedBuilder = this.Output;

                    var args = new List<string>(argsCount);

                    foreach (var arg in argsExpressions)
                    {
                        this.Output = new StringBuilder();
                        arg.AcceptVisitor(this);
                        args.Add((arg == paramsArg ? "[" : "") + this.Output.ToString());
                    }

                    if (paramsArg != null)
                    {
                        args[args.Count - 1] = args[args.Count - 1] + "]";
                    }

                    this.Output = savedBuilder;

                    this.Output.Append(String.Format(inlineCode, args.ToArray()));
                }
                else
                {

                    if (String.IsNullOrEmpty(customCtor))
                    {
                        this.WriteNew();
                        objectCreateExpression.Type.AcceptVisitor(this);
                    }
                    else
                    {
                        this.Write(customCtor);
                    }

                    this.WriteOpenParentheses();

                    if (!this.Validator.IsIgnoreType(type) && type.Methods.Count(m => m.IsConstructor) > 1)
                    {
                        this.WriteScript(this.GetOverloadNameInvocationResolveResult(this.Resolver.ResolveNode(objectCreateExpression, this) as InvocationResolveResult));

                        if (objectCreateExpression.Arguments.Count > 0)
                        {
                            this.WriteComma();
                        }
                    }
                    this.EmitExpressionList(argsExpressions, paramsArg);
                    this.WriteCloseParentheses();
                }

                if (hasInitializer)
                {
                    this.WriteComma();

                    this.BeginBlock();

                    var elements = objectCreateExpression.Initializer.Elements;
                    bool needComma = false;

                    foreach (Expression item in elements)
                    {                        
                        if (needComma)
                        {
                            this.WriteComma();
                            this.WriteNewLine();
                        }

                        needComma = true;

                        if (item is NamedExpression)
                        {
                            var namedExpression = (NamedExpression)item;
                            this.EmitNameExpression(namedExpression.Name, namedExpression, namedExpression.Expression);
                        }
                        else if (item is NamedArgumentExpression)
                        {
                            var namedArgumentExpression = (NamedArgumentExpression)item;
                            this.EmitNameExpression(namedArgumentExpression.Name, namedArgumentExpression, namedArgumentExpression.Expression);
                        }                        
                    }

                    this.WriteNewLine();
                    this.EndBlock();
                    this.WriteSpace();
                    this.WriteCloseParentheses();
                }
            }
        }

        protected virtual void EmitNameExpression(string name, Expression namedExpression, Expression expression)
        {
            var resolveResult = this.Resolver.ResolveNode(namedExpression, this);
            var lowerCaseName = this.ChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if (resolveResult != null && resolveResult is MemberResolveResult)
            {
                var member = ((MemberResolveResult)resolveResult).Member;
                lowerCaseName = this.GetEntityName(member);

                var isProperty = member.EntityType == EntityType.Property;

                if (!isProperty)
                {
                    this.Write(lowerCaseName);
                }
                else
                {
                    this.Write((isProperty ? "set" : "") + (isProperty ? name : lowerCaseName));
                }
            }
            else
            {
                this.Write(lowerCaseName);
            }

            this.WriteColon();
            expression.AcceptVisitor(this);
        }


        public override void VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            this.WriteIf();
            this.WriteOpenParentheses();

            ifElseStatement.Condition.AcceptVisitor(this);
            
            this.WriteCloseParentheses();
            this.EmitBlockOrIndentedLine(ifElseStatement.TrueStatement);

            if (ifElseStatement.FalseStatement != null && !ifElseStatement.FalseStatement.IsNull)
            {
                this.WriteElse();
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
            this.WriteFor();
            this.WriteOpenParentheses();

            foreach (var item in forStatement.Initializers)
            {
                if (item != forStatement.Initializers.First())
                {
                    WriteComma();
                }

                item.AcceptVisitor(this);
            }

            this.WriteSemiColon();
            this.WriteSpace();

            forStatement.Condition.AcceptVisitor(this);

            this.WriteSemiColon();
            this.WriteSpace();

            foreach (var item in forStatement.Iterators)
            {
                if (item != forStatement.Iterators.First())
                {
                    this.WriteComma();
                }

                item.AcceptVisitor(this);
            }

            this.WriteCloseParentheses();

            this.EnableSemicolon = true;

            this.EmitBlockOrIndentedLine(forStatement.EmbeddedStatement);

            this.PopLocals();
        }

        public override void VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            IAttribute inlineAttr = null;
            var resolveResult = this.Resolver.ResolveNode(indexerExpression, this);

            if (resolveResult is InvocationResolveResult)
            {
                var member = ((InvocationResolveResult)resolveResult).Member;
                if (member is SpecializedProperty)
                {
                    var specProp = (SpecializedProperty)member;
                    var method = this.IsAssignment ? specProp.Setter : specProp.Getter;
                    inlineAttr = this.GetAttribute(method.Attributes, Translator.CLR_ASSEMBLY + ".TemplateAttribute");
                }
            }

            indexerExpression.Target.AcceptVisitor(this);

            if (inlineAttr != null)
            {
                var inlineCode = inlineAttr.PositionalArguments[0];
                if (inlineCode.ConstantValue != null)
                {
                    string code = inlineCode.ConstantValue.ToString();

                    this.WriteDot();
                    this.PushWriter(code);
                    this.EmitExpressionList(indexerExpression.Arguments, null);
                    

                    if (!this.IsAssignment)
                    {
                        this.PopWriter();
                    }
                    else
                    {
                        this.WriteComma();
                    }
                }
            }
            else
            {
                if (indexerExpression.Arguments.Count != 1)
                {
                    throw this.CreateException(indexerExpression, "Only one index is supported");
                }                

                var index = indexerExpression.Arguments.First();

                var primitive = index as PrimitiveExpression;

                if (primitive != null && primitive.Value != null && Regex.Match(primitive.Value.ToString(), "^[_$a-z][_$a-z0-9]*$", RegexOptions.IgnoreCase).Success)
                {
                    this.WriteDot();
                    this.Write(primitive.Value);
                }
                else
                {
                    this.WriteOpenBracket();
                    index.AcceptVisitor(this);
                    this.WriteCloseBracket();
                }
            }            
        }

        public override void VisitCastExpression(CastExpression castExpression)
        {
            bool isInlineCast;
            string castCode = this.GetCastCode(castExpression.Expression, castExpression.Type, out isInlineCast);

            if (isInlineCast)
            {
                this.EmitInlineCast(castExpression.Expression, castExpression.Type, castCode);
                return;
            }
            
            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.CAST);
            this.WriteOpenParentheses();
            castExpression.Expression.AcceptVisitor(this);
            this.WriteComma();           

            if (castCode != null)
            {
                this.Write(castCode);
            }
            else
            {
                this.EmitCastType(castExpression.Type);
            }
            this.WriteCloseParentheses();
        }        

        public override void VisitAsExpression(AsExpression asExpression)
        {
            bool isInlineCast;
            string castCode = this.GetCastCode(asExpression.Expression, asExpression.Type, out isInlineCast);

            if (isInlineCast)
            {
                this.EmitInlineCast(asExpression.Expression, asExpression.Type, castCode);
                return;
            }

            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.AS);
            this.WriteOpenParentheses();
            asExpression.Expression.AcceptVisitor(this);
            this.WriteComma();

            if (castCode != null)
            {
                this.Write(castCode);
            }
            else
            {
                this.EmitCastType(asExpression.Type);
            }

            this.WriteCloseParentheses();
        }

        public override void VisitIsExpression(IsExpression isExpression)
        {
            bool isInlineCast;
            string castCode = this.GetCastCode(isExpression.Expression, isExpression.Type, out isInlineCast);

            if (isInlineCast)
            {
                this.EmitInlineCast(isExpression.Expression, isExpression.Type, castCode);
                return;
            }

            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.IS);
            this.WriteOpenParentheses();
            isExpression.Expression.AcceptVisitor(this);
            this.WriteComma();

            if (castCode != null)
            {
                this.Write(castCode);
            }
            else
            {
                this.EmitCastType(isExpression.Type);
            }

            this.WriteCloseParentheses();
        }

        public override void VisitReturnStatement(ReturnStatement returnStatement)
        {
            this.WriteReturn(false);

            if (!returnStatement.Expression.IsNull)
            {
                this.WriteSpace();
                returnStatement.Expression.AcceptVisitor(this);
            }
            
            this.WriteSemiColon();
            this.WriteNewLine();
        }

        public override void VisitThrowStatement(ThrowStatement throwStatement)
        {
            this.WriteThrow();
            throwStatement.Expression.AcceptVisitor(this);
            this.WriteSemiColon();
            this.WriteNewLine();
        }

        public override void VisitForeachStatement(ForeachStatement foreachStatement)
        {
            if (foreachStatement.EmbeddedStatement is EmptyStatement)
            {
                return;
            }

            var iteratorName = this.GetNextIteratorName();

            this.WriteVar();
            this.Write(iteratorName, " = ", Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.ENUMERATOR);

            this.WriteOpenParentheses();
            foreachStatement.InExpression.AcceptVisitor(this);
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

            this.WriteWhile();
            this.WriteOpenParentheses();
            this.Write(iteratorName);
            this.WriteDot();
            this.Write(Emitter.HAS_NEXT);
            this.WriteOpenCloseParentheses();
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();

            this.WriteVar();
            this.Write(foreachStatement.VariableName, " = ", iteratorName);
            
            this.WriteDot();
            this.Write(Emitter.NEXT);
            
            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();

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
            this.WriteNewLine();
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

            this.WriteTry();

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

                this.WriteCatch();
                this.WriteOpenParentheses();
                this.Write(varName);
                this.WriteCloseParentheses();

                clause.Body.AcceptVisitor(this);

                this.PopLocals();
            }

            if (!tryCatchStatement.FinallyBlock.IsNull)
            {
                this.WriteFinally();
                tryCatchStatement.FinallyBlock.AcceptVisitor(this);
            }
        }

        public override void VisitWhileStatement(WhileStatement whileStatement)
        {
            this.WriteWhile();
            this.WriteOpenParentheses();
            whileStatement.Condition.AcceptVisitor(this);
            this.WriteOpenCloseParentheses();
            this.EmitBlockOrIndentedLine(whileStatement.EmbeddedStatement);
        }

        public override void VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            this.WriteDo();
            this.EmitBlockOrIndentedLine(doWhileStatement.EmbeddedStatement);

            if (doWhileStatement.EmbeddedStatement is BlockStatement)
            {
                this.WriteSpace();
            }

            this.WriteWhile();
            this.WriteOpenCloseParentheses();

            doWhileStatement.Condition.AcceptVisitor(this);

            this.WriteOpenCloseParentheses();
            this.WriteSemiColon();

            this.WriteNewLine();
        }

        public override void VisitSwitchStatement(SwitchStatement switchStatement)
        {
            this.WriteSwitch();
            this.WriteOpenParentheses();

            switchStatement.Expression.AcceptVisitor(this);

            this.WriteCloseParentheses();
            this.WriteSpace();

            this.BeginBlock();
            switchStatement.SwitchSections.ToList().ForEach(s => s.AcceptVisitor(this));
            this.EndBlock();
            this.WriteNewLine();
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

            this.WriteColon();
            this.WriteNewLine();
        }

        public override void VisitBreakStatement(BreakStatement breakStatement)
        {
            this.Write("break");
            this.WriteSemiColon();
            this.WriteNewLine();
        }

        public override void VisitContinueStatement(ContinueStatement continueStatement)
        {
            this.Write("continue");
            this.WriteSemiColon();
            this.WriteNewLine();
        }

        public override void VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            this.WriteOpenParentheses();

            parenthesizedExpression.Expression.AcceptVisitor(this);
            this.WriteCloseParentheses();
        }

        public override void VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            typeOfExpression.Type.AcceptVisitor(this);
        }

        private static Regex injectComment = new Regex("@(.*)@", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static Regex removeStars = new Regex("(^\\s*)(\\* )", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public override void VisitComment(Comment comment)
        {
            Match injection = injectComment.Match(comment.Content);
            if (comment.CommentType == CommentType.MultiLine && injection.Success)
            {
                string code = removeStars.Replace(injection.Groups[1].Value, "$1");
                this.Write(code);
                this.WriteNewLine();
            }
            else if (comment.CommentType == CommentType.SingleLine || comment.CommentType == CommentType.MultiLine)
            {
                this.WriteComment(comment.Content);
            }            
        }

        public override void VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            this.WriteOpenBrace();
            this.WriteSpace();

            if (anonymousTypeCreateExpression.Initializers.Count > 0)
            {
                this.WriteObjectInitializer(anonymousTypeCreateExpression.Initializers, false);

                this.WriteSpace();
                this.WriteCloseBrace();
            }
            else
            {
                this.WriteCloseBrace();
            }
        }

        public override void VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            this.Write("null");
        }

        public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            var vars = eventDeclaration.Variables;
        }

        public override void VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            this.Write("null");
        }
    }
}
