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
using ICSharpCode.NRefactory.Completion;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.CSharp.Completion;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace Bridge.NET
{
    public partial class Emitter : Visitor
    {
        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            this.EnsureComma();
            this.ResetLocals();
            this.AddLocals(methodDeclaration.Parameters);

            this.Write(this.GetEntityName(methodDeclaration));

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
            this.PushLocals();
            this.BeginBlock();

            blockStatement.Children.ToList().ForEach(child => child.AcceptVisitor(this));
            this.EndBlock();

            if (!this.KeepLineAfterBlock(blockStatement))
            {
                this.WriteNewLine();
            }

            this.PopLocals();
        }

        public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            this.WriteVar();
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

        public override void VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            binaryOperatorExpression.Left.AcceptVisitor(this);
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

            this.WriteSpace();
            binaryOperatorExpression.Right.AcceptVisitor(this);
        }

        public override void VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            var id = identifierExpression.Identifier;
            this.CheckIdentifier(id, identifierExpression);

            var resolveResult = MemberResolver.ResolveExpression(identifierExpression);
            var isResolved = resolveResult != null && !resolveResult.IsError;
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
                
                if (method != null)
                {
                    string inline = this.GetInline(method);

                    if (!string.IsNullOrWhiteSpace(inline))
                    {
                        
                        return;
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
                    this.Write(this.GetMethodName(method));

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
            var resolveResult = Bridge.NET.MemberResolver.Resolve(memberReferenceExpression);

            if (resolveResult == null || resolveResult.IsError)
            {
                //throw new Exception("MemberReferenceExpression resolving is failed: " + memberReferenceExpression.ToString());
                memberReferenceExpression.Target.AcceptVisitor(this);
                this.WriteDot();
                string name = Helpers.GetScriptName(memberReferenceExpression, false);
                //this.Write(this.ChangeCase ? name.ToLowerCamelCase() : name);
                this.Write(name.ToLowerCamelCase());
                return;
            }

            if (resolveResult is DynamicInvocationResolveResult)
            {
                resolveResult = ((DynamicInvocationResolveResult)resolveResult).Target;
            }

            if (resolveResult is MethodGroupResolveResult)
            {
                MethodGroupResolveResult group = (MethodGroupResolveResult)resolveResult;
                IMethod method = group.Methods.First();
                memberReferenceExpression.Target.AcceptVisitor(this);
                this.WriteDot();
                this.Write(this.GetEntityName(method));
                return;
            }
            
            MemberResolveResult member = resolveResult as MemberResolveResult;           
            
            string inline = member != null ? this.GetInline(member.Member) : null;
            if (!string.IsNullOrEmpty(inline) && member.Member.IsStatic)
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
                    this.PushWriter(inline);
                }
                else if (member.Member.EntityType == EntityType.Property && member.TargetResult.Type.Kind != TypeKind.Anonymous)
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
                    this.Write(this.GetEntityName(invocationResult.Member));
                }
                else
                {
                    this.Write(Helpers.GetScriptName(memberReferenceExpression, false));
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
            Tuple<bool, string> inlineCode = this.GetInlineCode(invocationExpression);

            if (inlineCode != null && !String.IsNullOrEmpty(inlineCode.Item2) && invocationExpression.Target is IdentifierExpression/* && inlineCode.Item1*/)
            {
                bool isStatic = inlineCode.Item1;
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

                if (!isStatic) 
                {
                    this.WriteThis();
                    this.WriteDot();                    
                }

                this.Output.Append(String.Format(inlineCode.Item2, args.ToArray()));

                if (!isStatic)
                {
                    invocationExpression.Target.AcceptVisitor(this);
                }
                
                return;                
            }

            MemberReferenceExpression targetMember = invocationExpression.Target as MemberReferenceExpression;
            if (targetMember != null)
            {
                var member = MemberResolver.ResolveParent(targetMember);

                if (member != null && member.Type.Kind == TypeKind.Delegate)
                {
                    throw this.CreateException(invocationExpression, "Delegate's methods are not supported. Please use direct delegate invoke.");
                }

                var targetResolve = MemberResolver.Resolve(targetMember);

                if (targetResolve != null && !targetResolve.IsError)
                {
                    var csharpInvocation = targetResolve as CSharpInvocationResolveResult;
                    InvocationResolveResult invocationResult;
                    
                    if (csharpInvocation != null)
                    {
                        invocationResult = csharpInvocation.IsExtensionMethodInvocation ? csharpInvocation : null;
                    }
                    else
                    {
                        invocationResult = targetResolve as InvocationResolveResult;
                    }                    

                    if (invocationResult != null)
                    {
                        var resolvedMethod = invocationResult.Member as DefaultResolvedMethod;

                        if (resolvedMethod != null && resolvedMethod.IsExtensionMethod)
                        {
                            string inline = this.GetInline(resolvedMethod);
                            if (!string.IsNullOrWhiteSpace(inline))
                            {
                                this.Write("");
                                StringBuilder savedBuilder = this.Output;
                                var args = new List<string>(invocationExpression.Arguments.Count + 1);

                                this.Output = new StringBuilder();
                                this.WriteThisExtension(invocationExpression);
                                args.Add(this.Output.ToString());

                                foreach (var arg in invocationExpression.Arguments)
                                {
                                    this.Output = new StringBuilder();
                                    arg.AcceptVisitor(this);
                                    args.Add(this.Output.ToString());
                                }

                                this.Output = savedBuilder;

                                this.Output.Append(String.Format(inline, args.ToArray()));
                            }
                            else
                            {
                                string name = resolvedMethod.DeclaringType.FullName + "." + this.GetEntityName(resolvedMethod);

                                this.Write(name);
                                this.WriteOpenParentheses();

                                this.WriteThisExtension(invocationExpression);

                                this.EmitExpressionList(invocationExpression.Arguments);

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
                    var resolveResult = MemberResolver.Resolve(targetMember);
                    if (resolveResult != null && !resolveResult.IsError && resolveResult is InvocationResolveResult)
                    {
                        InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                        this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".", this.GetEntityName(invocationResult.Member));
                    }
                    else
                    {
                        this.Write(this.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".", this.ChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(baseMethod) : baseMethod);
                    }                    

                    this.WriteDot();
                    this.Write("call");
                    this.WriteOpenParentheses();
                    
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

                this.WriteCloseParentheses();
            }
            else
            {
                invocationExpression.Target.AcceptVisitor(this);
                if (this.Writers.Count > 0)
                {
                    this.EmitExpressionList(invocationExpression.Arguments);                    
                    this.PopWriter();
                }
                else
                {
                    this.WriteOpenParentheses();
                    this.EmitExpressionList(invocationExpression.Arguments);
                    this.WriteCloseParentheses();
                }                
            }
        }

        protected virtual void WriteThisExtension(InvocationExpression invocationExpression)
        {
            if (invocationExpression.Target.HasChildren)
            {
                var first = invocationExpression.Target.Children.ElementAt(0);
                var expression = first as Expression;
                if (expression != null)
                {
                    expression.AcceptVisitor(this);
                }

                if (invocationExpression.Arguments.Count > 0)
                {
                    this.WriteComma();
                    this.WriteSpace();
                }
            }
        }

        public override void VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            this.IsAssignment = true;
            assignmentExpression.Left.AcceptVisitor(this);
            this.IsAssignment = false;

            if (this.Writers.Count == 0)
            {
                this.WriteSpace();
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

            if (this.Writers.Count == 0)
            {
                this.Write("= ");
            }

            assignmentExpression.Right.AcceptVisitor(this);

            if (this.Writers.Count > 0)
            {
                this.PopWriter();
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

        public override void VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            if (arrayCreateExpression.Arguments.Count > 1)
            {
                throw this.CreateException(arrayCreateExpression, "Multi-dimensional arrays are not supported");
            }

            this.WriteOpenBracket();
            this.WriteSpace();
            var elements = arrayCreateExpression.Initializer.Elements;
            this.EmitExpressionList(elements);

            if (elements.Count > 0)
            {
                this.WriteSpace();
            }

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

            var customCtor = this.Validator.GetCustomConstructor(type) ?? "";
            var hasInitializer = !objectCreateExpression.Initializer.IsNull && objectCreateExpression.Initializer.Elements.Count > 0;

            if (Regex.Match(customCtor, @"\s*\{\s*\}\s*").Success)
            {
                this.WriteOpenBrace();
                this.WriteSpace();

                if (hasInitializer)
                {
                    this.WriteObjectInitializer(objectCreateExpression.Initializer.Elements);

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
                    this.Write(Emitter.APPLY_OBJECT);
                    this.WriteOpenParentheses();
                }

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
                this.EmitExpressionList(objectCreateExpression.Arguments);
                this.WriteCloseParentheses();

                if (hasInitializer)
                {
                    this.WriteComma();

                    this.WriteOpenBrace(true);

                    var elements = objectCreateExpression.Initializer.Elements;
                    bool needComma = false;

                    foreach (Expression item in elements)
                    {                        
                        if (needComma)
                        {
                            this.WriteComma();
                        }

                        needComma = true;

                        if (item is NamedExpression)
                        {
                            var namedExpression = (NamedExpression)item;
                            var resolveResult = MemberResolver.ResolveExpression(item);
                            var lowerCaseName = Ext.Net.Utilities.StringUtils.ToLowerCamelCase(namedExpression.Name);

                            if (resolveResult != null && !resolveResult.IsError && resolveResult is MemberResolveResult)
                            {
                                var isMember = ((MemberResolveResult)resolveResult).Member.EntityType == EntityType.Property;
                                this.Write((isMember ? "set" : "") + (isMember ? namedExpression.Name : lowerCaseName));
                            }
                            else
                            {
                                this.Write(lowerCaseName);
                            }
                            this.WriteColon();
                            namedExpression.Expression.AcceptVisitor(this);
                        }
                        else if (item is NamedArgumentExpression)
                        {
                            var namedArgumentExpression = (NamedArgumentExpression)item;
                            var resolveResult = MemberResolver.ResolveExpression(item);
                            var lowerCaseName = Ext.Net.Utilities.StringUtils.ToLowerCamelCase(namedArgumentExpression.Name);

                            if (resolveResult != null && !resolveResult.IsError && resolveResult is MemberResolveResult)
                            {
                                var isMember = ((MemberResolveResult)resolveResult).Member.EntityType == EntityType.Property;
                                this.Write((isMember ? "set" : "") + (isMember ? namedArgumentExpression.Name : lowerCaseName));
                            }
                            else
                            {
                                this.Write(lowerCaseName);
                            }
                            this.WriteColon();
                            namedArgumentExpression.Expression.AcceptVisitor(this);
                        }                       
                        
                    }

                    this.WriteCloseBrace();
                    this.WriteSpace();
                    this.WriteCloseParentheses();
                }
            }
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
            if (indexerExpression.Arguments.Count != 1)
            {
                throw this.CreateException(indexerExpression, "Only one index is supported");
            }

            indexerExpression.Target.AcceptVisitor(this);

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

        public override void VisitCastExpression(CastExpression castExpression)
        {
            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.CAST);
            this.WriteOpenParentheses();
            castExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            castExpression.Type.AcceptVisitor(this);
            this.WriteCloseParentheses();
        }

        public override void VisitAsExpression(AsExpression asExpression)
        {
            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.AS);
            this.WriteOpenParentheses();
            asExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            asExpression.Type.AcceptVisitor(this);
            this.WriteCloseParentheses();
        }

        public override void VisitIsExpression(IsExpression isExpression)
        {
            this.Write(Emitter.ROOT);
            this.WriteDot();
            this.Write(Emitter.IS);
            this.WriteOpenParentheses();
            isExpression.Expression.AcceptVisitor(this);
            this.WriteComma();
            isExpression.Type.AcceptVisitor(this);
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

        public override void VisitComment(Comment comment)
        {
            if (comment.CommentType != CommentType.SingleLine
                && comment.CommentType != CommentType.MultiLine)
            {
                return;
            }

            this.WriteComment(comment.Content);            
        }

        public override void VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            this.WriteOpenBrace();
            this.WriteSpace();

            if (anonymousTypeCreateExpression.Initializers.Count > 0)
            {
                this.WriteObjectInitializer(anonymousTypeCreateExpression.Initializers);

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
    }
}
