using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class IdentifierBlock : AbstractEmitterBlock
    {
        public IdentifierBlock(Emitter emitter, IdentifierExpression identifierExpression)
        {
            this.Emitter = emitter;
            this.IdentifierExpression = identifierExpression;
        }

        public IdentifierExpression IdentifierExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitIdentifierExpression();
        }

        protected IMemberDefinition ResolveFieldOrMethod(string name, int genericCount)
        {
            bool allowPrivate = true;
            TypeDefinition type = this.Emitter.GetTypeDefinition();
            TypeDefinition thisType = type;

            while (true)
            {
                foreach (MethodDefinition method in type.Methods)
                {
                    if (method.Name != name || method.GenericParameters.Count != genericCount)
                    {
                        continue;
                    }

                    if (method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly)
                    {
                        return method;
                    }

                    if (method.IsPrivate && allowPrivate)
                    {
                        return method;
                    }

                    if (method.IsAssembly && type.Module.Mvid == thisType.Module.Mvid)
                    {
                        return method;
                    }
                }

                foreach (FieldDefinition field in type.Fields)
                {
                    if (field.Name != name)
                    {
                        continue;
                    }

                    if (field.IsPublic || field.IsFamily || field.IsFamilyOrAssembly)
                    {
                        return field;
                    }

                    if (field.IsPrivate && allowPrivate)
                    {
                        return field;
                    }

                    if (field.IsAssembly && type.Module.Mvid == thisType.Module.Mvid)
                    {
                        return field;
                    }
                }

                type = this.Emitter.GetBaseTypeDefinition(type);

                if (type == null)
                {
                    break;
                }

                allowPrivate = false;
            }

            return null;
        }

        protected void VisitIdentifierExpression()
        {
            IdentifierExpression identifierExpression = this.IdentifierExpression;
            var id = identifierExpression.Identifier;
            this.Emitter.Validator.CheckIdentifier(id, identifierExpression);

            var resolveResult = this.Emitter.Resolver.ResolveNode(identifierExpression, this.Emitter);
            var isResolved = resolveResult != null && !(resolveResult is ErrorResolveResult);
            var memberResult = resolveResult as MemberResolveResult;

            if (this.Emitter.Locals.ContainsKey(id))
            {
                this.Write(id);

                return;
            }

            IMemberDefinition member = this.ResolveFieldOrMethod(id, identifierExpression.TypeArguments.Count);

            if (member == null && memberResult != null)
            {
                var iMethod = memberResult.Member as IMethod;

                if (iMethod != null)
                {
                    member = this.ResolveFieldOrMethod(id, iMethod.TypeParameters.Count);
                }
            }

            if (member != null)
            {
                MethodDefinition method = member as MethodDefinition;
                FieldDefinition field = member as FieldDefinition;
                bool isStatic = method != null && method.IsStatic || field != null && field.IsStatic;
                string appendAdditionalCode = null;

                if (method != null)
                {
                    string inline = this.Emitter.GetInline(method);

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
                            this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(member.DeclaringType)));
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
                    this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(member.DeclaringType)));
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
                        this.Write(this.Emitter.GetOverloadNameInvocationResolveResult(invocationResult));
                    }
                    else
                    {
                        this.Write(this.Emitter.GetMethodName(method));
                    }

                    if (appendAdditionalCode != null)
                    {
                        this.Write(appendAdditionalCode);
                    }
                }
                else
                {
                    bool isConst = this.Emitter.IsMemberConst(memberResult.Member);

                    if (isConst && this.Emitter.IsInlineConst(memberResult.Member))
                    {
                        this.WriteScript(memberResult.ConstantValue);
                    }
                    else
                    {
                        bool changeCase = (!this.Emitter.IsNativeMember(memberResult.Member.FullName) ? this.Emitter.ChangeCase : true) && !isConst;
                        this.Write(this.Emitter.GetEntityName(memberResult.Member, !changeCase));
                    }
                }

                return;
            }

            string resolved = this.Emitter.ResolveNamespaceOrType(id, true);

            if (!String.IsNullOrEmpty(resolved))
            {
                if (this.Emitter.TypeDefinitions.ContainsKey(resolved))
                {
                    resolved = this.Emitter.ShortenTypeName(resolved);
                }

                this.Write(resolved);

                return;
            }

            if (memberResult != null && memberResult.Member.SymbolKind == SymbolKind.Property && memberResult.TargetResult.Type.Kind != TypeKind.Anonymous)
            {
                if (memberResult.Member.IsStatic)
                {
                    this.Write(this.Emitter.ShortenTypeName(Helpers.ReplaceSpecialChars(memberResult.Member.DeclaringType.FullName)));
                }
                else
                {
                    this.WriteThis();
                }

                this.WriteDot();

                if (!this.Emitter.IsAssignment)
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
                throw this.Emitter.CreateException(identifierExpression, "Cannot resolve identifier: " + id);
            }
        }        
    }
}
