using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public class InvocationBlock : ConversionBlock
    {
        public InvocationBlock(IEmitter emitter, InvocationExpression invocationExpression)
        {
            this.Emitter = emitter;
            this.InvocationExpression = invocationExpression;
        }

        public InvocationExpression InvocationExpression 
        { 
            get; 
            set; 
        }

        protected override Expression GetExpression()
        {
            return this.InvocationExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.VisitInvocationExpression();
        }

        protected virtual bool IsEmptyPartialInvoking(InvocationResolveResult invocationResult)
        {
            if (invocationResult == null)
            {
                return false;
            }
            var resolvedMethod = invocationResult.Member as IMethod;
            return resolvedMethod != null && resolvedMethod.IsPartial && !resolvedMethod.HasBody;
        }

        protected void WriteThisExtension(Expression target)
        {
            if (target.HasChildren)
            {
                var first = target.Children.ElementAt(0);
                var expression = first as Expression;

                if (expression != null)
                {
                    expression.AcceptVisitor(this.Emitter);
                }
            }
        }         

        protected void VisitInvocationExpression()
        {
            InvocationExpression invocationExpression = this.InvocationExpression;
            var oldValue = this.Emitter.ReplaceAwaiterByVar;
            var oldAsyncExpressionHandling = this.Emitter.AsyncExpressionHandling;

            if (this.Emitter.IsAsync && !this.Emitter.AsyncExpressionHandling)
            {
                this.WriteAwaiters(invocationExpression);
                this.Emitter.ReplaceAwaiterByVar = true;
                this.Emitter.AsyncExpressionHandling = true;
            }            

            Tuple<bool, bool, string> inlineInfo = this.Emitter.GetInlineCode(invocationExpression);
            var argsInfo = new ArgumentsInfo(this.Emitter, invocationExpression);
            
            var argsExpressions = argsInfo.ArgumentsExpressions;
            var paramsArg = argsInfo.ParamsExpression;
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
                            throw new Exception("Only primitive expression can be inlined: " + inlineExpression.ToString());
                        }
                        
                        this.Write(inlineExpression.Value);

                        string value = inlineExpression.Value.ToString().Trim();
                        if (value[value.Length - 1] == ';')
                        {
                            this.Emitter.EnableSemicolon = false;
                            this.WriteNewLine();
                        }

                        this.Emitter.ReplaceAwaiterByVar = oldValue;
                        this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                        return;
                    }
                }
                else if (!String.IsNullOrEmpty(inlineScript) && invocationExpression.Target is IdentifierExpression)
                {
                    argsInfo.ThisArgument = "this";
                    bool noThis = !inlineScript.Contains("{this}");

                    if (!isStaticMethod && noThis)
                    {
                        this.WriteThis();
                        this.WriteDot();
                    }
                    new InlineArgumentsBlock(this.Emitter, argsInfo, inlineScript).Emit();
                    this.Emitter.ReplaceAwaiterByVar = oldValue;
                    this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                    return;
                }
            }            

            MemberReferenceExpression targetMember = invocationExpression.Target as MemberReferenceExpression;
            if (targetMember != null)
            {
                var member = this.Emitter.Resolver.ResolveNode(targetMember.Target, this.Emitter);

                if (member != null && member.Type.Kind == TypeKind.Delegate)
                {
                    throw (Exception)this.Emitter.CreateException(invocationExpression, "Delegate's methods are not supported. Please use direct delegate invoke.");
                }

                var targetResolve = this.Emitter.Resolver.ResolveNode(targetMember, this.Emitter);

                if (targetResolve != null)
                {
                    var csharpInvocation = targetResolve as CSharpInvocationResolveResult;
                    InvocationResolveResult invocationResult;

                    if (csharpInvocation != null)
                    {
                        invocationResult = csharpInvocation.IsExtensionMethodInvocation ? csharpInvocation : null;

                        if (this.IsEmptyPartialInvoking(csharpInvocation))
                        {
                            this.Emitter.SkipSemiColon = true;
                            this.Emitter.ReplaceAwaiterByVar = oldValue;
                            this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                            return;
                        }
                    }
                    else
                    {
                        invocationResult = targetResolve as InvocationResolveResult;

                        if (this.IsEmptyPartialInvoking(invocationResult))
                        {
                            this.Emitter.SkipSemiColon = true;
                            this.Emitter.ReplaceAwaiterByVar = oldValue;
                            this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                            return;
                        }
                    }

                    if (invocationResult != null)
                    {
                        var resolvedMethod = invocationResult.Member as IMethod;

                        if (resolvedMethod != null && resolvedMethod.IsExtensionMethod)
                        {
                            string inline = this.Emitter.GetInline(resolvedMethod);

                            if (!string.IsNullOrWhiteSpace(inline))
                            {
                                this.Write("");
                                StringBuilder savedBuilder = this.Emitter.Output;
                                this.Emitter.Output = new StringBuilder();
                                this.WriteThisExtension(invocationExpression.Target);
                                argsInfo.ThisArgument = this.Emitter.Output.ToString();
                                this.Emitter.Output = savedBuilder;
                                new InlineArgumentsBlock(this.Emitter, argsInfo, inline).Emit();
                            }
                            else
                            {
                                string name = this.Emitter.ShortenTypeName(Helpers.ReplaceSpecialChars(resolvedMethod.DeclaringType.FullName)) + "." + this.Emitter.GetEntityName(resolvedMethod);
                                var isIgnoreClass = resolvedMethod.DeclaringTypeDefinition != null && this.Emitter.Validator.IsIgnoreType(resolvedMethod.DeclaringTypeDefinition);

                                this.Write(name);

                                if (!isIgnoreClass && argsInfo.TypeArguments != null && argsInfo.TypeArguments.Length > 0)
                                {
                                    this.WriteOpenParentheses();
                                    new TypeExpressionListBlock(this.Emitter, argsInfo.TypeArguments).Emit();
                                    this.WriteCloseParentheses();
                                }

                                this.WriteOpenParentheses();

                                this.WriteThisExtension(invocationExpression.Target);

                                if (argsCount > 0)
                                {
                                    this.WriteComma();
                                }

                                new ExpressionListBlock(this.Emitter, argsExpressions, paramsArg).Emit();

                                this.WriteCloseParentheses();
                            }

                            this.Emitter.ReplaceAwaiterByVar = oldValue;
                            this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                            return;
                        }
                    }
                }
            }

            if (targetMember != null && targetMember.Target is BaseReferenceExpression)
            {                
                var baseType = this.Emitter.GetBaseMethodOwnerTypeDefinition(targetMember.MemberName, targetMember.TypeArguments.Count);
                var method = invocationExpression.GetParent<MethodDeclaration>();

                bool isIgnore = this.Emitter.Validator.IsIgnoreType(baseType);
                if (isIgnore)
                {
                    throw (Exception)this.Emitter.CreateException(targetMember.Target, "Cannot call base method, because parent class code is ignored");
                }

                string baseMethod = Helpers.GetScriptName(targetMember, false);
                bool needComma = false;

                var resolveResult = this.Emitter.Resolver.ResolveNode(targetMember, this.Emitter);

                if (resolveResult != null && resolveResult is InvocationResolveResult)
                {
                    InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                    this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".prototype.", this.Emitter.GetEntityName(invocationResult.Member));
                }
                else
                {
                    this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".prototype.", this.Emitter.ChangeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(baseMethod) : baseMethod);
                }

                if (!isIgnore && argsInfo.TypeArguments != null && argsInfo.TypeArguments.Length > 0)
                {
                    this.WriteOpenParentheses();
                    new TypeExpressionListBlock(this.Emitter, argsInfo.TypeArguments).Emit();
                    this.WriteCloseParentheses();
                }

                this.WriteDot();

                this.Write("call");
                this.WriteOpenParentheses();

                this.WriteThis();
                needComma = true;

                foreach (var arg in argsExpressions)
                {
                    if (needComma)
                    {
                        this.WriteComma();
                    }

                    needComma = true;
                    arg.AcceptVisitor(this.Emitter);
                }

                this.WriteCloseParentheses();
            }
            else
            {
                var targetResolveResult = this.Emitter.Resolver.ResolveNode(invocationExpression.Target, this.Emitter);
                var invocationResolveResult = targetResolveResult as InvocationResolveResult;

                if (this.IsEmptyPartialInvoking(invocationResolveResult))
                {
                    this.Emitter.SkipSemiColon = true;
                    this.Emitter.ReplaceAwaiterByVar = oldValue;
                    this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
                    return;
                }

                int count = this.Emitter.Writers.Count;
                invocationExpression.Target.AcceptVisitor(this.Emitter);

                if (this.Emitter.Writers.Count > count)
                {
                    var tuple = this.Emitter.Writers.Pop();
                    new InlineArgumentsBlock(this.Emitter, argsInfo, tuple.Item1).Emit();
                    var result = this.Emitter.Output.ToString();
                    this.Emitter.Output = tuple.Item2;
                    this.Emitter.IsNewLine = tuple.Item3;
                    this.Write(result);
                }
                else
                {
                    var isIgnore = false;

                    if(invocationResolveResult != null && invocationResolveResult.Member.DeclaringTypeDefinition != null && this.Emitter.Validator.IsIgnoreType(invocationResolveResult.Member.DeclaringTypeDefinition))
                    {
                        isIgnore = true;
                    }

                    if (!isIgnore && argsInfo.TypeArguments != null && argsInfo.TypeArguments.Length > 0)
                    {
                        this.WriteOpenParentheses();
                        new TypeExpressionListBlock(this.Emitter, argsInfo.TypeArguments).Emit();
                        this.WriteCloseParentheses();
                    }

                    this.WriteOpenParentheses();                    
                    new ExpressionListBlock(this.Emitter, argsExpressions, paramsArg).Emit();
                    this.WriteCloseParentheses();
                }
            }

            this.Emitter.ReplaceAwaiterByVar = oldValue;
            this.Emitter.AsyncExpressionHandling = oldAsyncExpressionHandling;
        }        
    }
}
