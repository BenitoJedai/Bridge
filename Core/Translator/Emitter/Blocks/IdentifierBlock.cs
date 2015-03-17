using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public class IdentifierBlock : ConversionBlock
    {
        public IdentifierBlock(IEmitter emitter, IdentifierExpression identifierExpression)
        {
            this.Emitter = emitter;
            this.IdentifierExpression = identifierExpression;
        }

        public IdentifierExpression IdentifierExpression 
        { 
            get; 
            set; 
        }

        protected override Expression GetExpression()
        {
            return this.IdentifierExpression;
        }

        protected override void EmitConversionExpression()
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

            ResolveResult resolveResult = null;
            if (identifierExpression.Parent is InvocationExpression && (((InvocationExpression)(identifierExpression.Parent)).Target == identifierExpression))
            {
                resolveResult = this.Emitter.Resolver.ResolveNode(identifierExpression.Parent, this.Emitter);
            }
            else
            {
                resolveResult = this.Emitter.Resolver.ResolveNode(identifierExpression, this.Emitter);
            }
            var id = identifierExpression.Identifier;
            //this.Emitter.Validator.CheckIdentifier(id, identifierExpression);

            var isResolved = resolveResult != null && !(resolveResult is ErrorResolveResult);
            var memberResult = resolveResult as MemberResolveResult;

            if (this.Emitter.Locals != null && this.Emitter.Locals.ContainsKey(id))
            {
                if (this.Emitter.LocalsMap != null && this.Emitter.LocalsMap.ContainsKey(id) && !(identifierExpression.Parent is DirectionExpression))
                {
                    this.Write(this.Emitter.LocalsMap[id]);
                }
                else
                {
                    this.Write(id);
                }

                Helpers.CheckValueTypeClone(resolveResult, identifierExpression, this);

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

                        if (!isStatic)
                        {
                            var isExtensionMethod = resolvedMethod.IsExtensionMethod;
                            this.Write(Bridge.NET.Emitter.ROOT + "." + (isExtensionMethod ? Bridge.NET.Emitter.DELEGATE_BIND_SCOPE : Bridge.NET.Emitter.DELEGATE_BIND) + "(");
                            this.WriteThis();
                            this.Write(", ");
                            appendAdditionalCode = ")";
                        }
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
                        this.Write(OverloadsCollection.Create(this.Emitter, invocationResult.Member).GetOverloadName());
                    }
                    else
                    {
                        this.Write(this.Emitter.GetDefinitionName(method));
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
                    else if (memberResult != null && memberResult.Member is DefaultResolvedEvent && this.Emitter.IsAssignment && (this.Emitter.AssignmentType == AssignmentOperatorType.Add || this.Emitter.AssignmentType == AssignmentOperatorType.Subtract))
                    {
                        this.Write(this.Emitter.AssignmentType == AssignmentOperatorType.Add ? "add" : "remove");
                        this.Write(OverloadsCollection.Create(this.Emitter, memberResult.Member).GetOverloadName());
                        this.WriteOpenParentheses();
                    }
                    else
                    {
                        this.Write(OverloadsCollection.Create(this.Emitter, memberResult.Member).GetOverloadName());
                    }
                }

                Helpers.CheckValueTypeClone(resolveResult, identifierExpression, this);

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

            string inlineCode = memberResult != null ? this.Emitter.GetInline(memberResult.Member) : null;
            bool hasInline = !string.IsNullOrEmpty(inlineCode);
            bool hasThis = hasInline && inlineCode.Contains("{this}");
            
            if (hasThis)
            {
                this.Write("");
                var oldBuilder = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();

                if (memberResult.Member.IsStatic)
                {
                    this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(memberResult.Member.DeclaringType)));
                }
                else
                {
                    this.WriteThis();
                }

                inlineCode = inlineCode.Replace("{this}", this.Emitter.Output.ToString());
                this.Emitter.Output = oldBuilder;

                if (resolveResult is InvocationResolveResult)
                {
                    this.PushWriter(inlineCode);
                }
                else
                {
                    this.Write(inlineCode);
                }

                return;
            }

            if (hasInline && memberResult.Member.IsStatic)
            {
                if (resolveResult is InvocationResolveResult)
                {
                    this.PushWriter(inlineCode);
                }
                else
                {
                    this.Write(inlineCode);
                }
            }            
            else if (memberResult != null && memberResult.Member.SymbolKind == SymbolKind.Property && memberResult.TargetResult.Type.Kind != TypeKind.Anonymous)
            {
                this.WriteTarget(memberResult);

                if (!string.IsNullOrWhiteSpace(inlineCode))
                {
                    this.Write(inlineCode);
                }
                else if (Helpers.IsFieldProperty(memberResult.Member, this.Emitter))
                {
                    this.Write(Helpers.GetPropertyRef(memberResult.Member, this.Emitter));
                }
                else if (!this.Emitter.IsAssignment)
                {
                    this.Write(Helpers.GetPropertyRef(memberResult.Member, this.Emitter));
                    this.WriteOpenParentheses();
                    this.WriteCloseParentheses();
                }
                else
                {
                    this.PushWriter(Helpers.GetPropertyRef(memberResult.Member, this.Emitter, true) + "({0})");
                }
            }
            else if (memberResult != null && memberResult.Member is DefaultResolvedEvent && this.Emitter.IsAssignment && (this.Emitter.AssignmentType == AssignmentOperatorType.Add || this.Emitter.AssignmentType == AssignmentOperatorType.Subtract))
            {
                this.WriteTarget(memberResult);

                if (!string.IsNullOrWhiteSpace(inlineCode))
                {
                    this.Write(inlineCode);
                }
                else
                {
                    this.Write(this.Emitter.AssignmentType == AssignmentOperatorType.Add ? "add" : "remove");
                    this.Write(OverloadsCollection.Create(this.Emitter, memberResult.Member).GetOverloadName());
                }

                this.WriteOpenParentheses();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(inlineCode))
                {
                    this.Write(inlineCode);
                }
                else if (isResolved)
                {
                    if (resolveResult is TypeResolveResult)
                    {
                        var typeResolveResult = (TypeResolveResult)resolveResult;

                        this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(typeResolveResult.Type)));

                        if (typeResolveResult.Type.TypeParameterCount > 0)
                        {
                            this.WriteOpenParentheses();
                            new TypeExpressionListBlock(this.Emitter, this.IdentifierExpression.TypeArguments).Emit();
                            this.WriteCloseParentheses();
                        }
                        else
                        {
                            this.WriteDot();
                        }
                    }
                    else if (resolveResult is LocalResolveResult)
                    {
                        var localResolveResult = (LocalResolveResult)resolveResult;
                        this.Write(localResolveResult.Variable.Name);
                    }
                    else if (memberResult != null)
                    {
                        this.WriteTarget(memberResult);
                        this.Write(this.Emitter.GetEntityName(memberResult.Member));
                    }
                    else
                    {
                        this.Write(resolveResult.ToString());
                    }

                    Helpers.CheckValueTypeClone(resolveResult, identifierExpression, this);
                }
                else
                {
                    throw (Exception)this.Emitter.CreateException(identifierExpression, "Cannot resolve identifier: " + id);
                }
            }
        }

        protected void WriteTarget(MemberResolveResult memberResult)
        {
            if (memberResult.Member.IsStatic)
            {
                this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(memberResult.Member.DeclaringType)));
            }
            else
            {
                this.WriteThis();
            }

            this.WriteDot();
        }
    }
}
