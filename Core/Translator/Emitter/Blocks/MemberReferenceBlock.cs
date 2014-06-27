using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using System.Collections.Generic;
using System.Text;
using Ext.Net.Utilities;

namespace Bridge.NET
{
    public class MemberReferenceBlock : AbstractEmitterBlock
    {
        public MemberReferenceBlock(Emitter emitter, MemberReferenceExpression memberReferenceExpression)
        {
            this.Emitter = emitter;
            this.MemberReferenceExpression = memberReferenceExpression;
        }

        public MemberReferenceExpression MemberReferenceExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitMemberReferenceExpression();
        }

        protected void VisitMemberReferenceExpression()
        {
            MemberReferenceExpression memberReferenceExpression = this.MemberReferenceExpression;

            var resolveResult = this.Emitter.Resolver.ResolveNode(memberReferenceExpression, this.Emitter);

            if (resolveResult == null && !(resolveResult is ErrorResolveResult))
            {
                memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
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
                resolveResult = this.Emitter.Resolver.ResolveNode(memberReferenceExpression.Parent, this.Emitter);
            }

            MemberResolveResult member = resolveResult as MemberResolveResult;

            string inline = member != null ? this.Emitter.GetInline(member.Member) : null;
            bool hasInline = !string.IsNullOrEmpty(inline);
            bool hasThis = hasInline && inline.Contains("{this}");
            string appendAdditionalCode = null;

            if (hasThis)
            {
                this.Write("");
                var oldBuilder = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();
                memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                inline = inline.Replace("{this}", this.Emitter.Output.ToString());
                this.Emitter.Output = oldBuilder;

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
                        var enumMode = this.Emitter.Validator.EnumEmitMode(typeDef);

                        if ((this.Emitter.Validator.IsIgnoreType(typeDef) && enumMode == -1) || enumMode == 2)
                        {
                            this.WriteScript(member.ConstantValue);
                            return;
                        }

                        if (enumMode >= 3)
                        {
                            string enumStringName = member.Member.Name;
                            var attr = this.Emitter.GetAttribute(member.Member.Attributes, Translator.CLR_ASSEMBLY + ".NameAttribute");

                            if (attr != null)
                            {
                                enumStringName = this.Emitter.GetEntityName(member.Member);
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

                    this.Write(this.Emitter.ShortenTypeName(typeResolveResult.Type.FullName));
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

                    this.Write(Emitter.ROOT + "." + (isExtensionMethod ? Emitter.DELEGATE_BIND_SCOPE : Emitter.DELEGATE_BIND) + "(");
                    memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                    this.Write(", ");

                    if (isExtensionMethod)
                    {
                        this.Write(this.Emitter.ShortenTypeName(resolvedMethod.DeclaringType.FullName));
                    }
                    else
                    {
                        memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                    }

                    appendAdditionalCode = ")";
                }
                else
                {
                    memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
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
                else if (member.Member.SymbolKind == SymbolKind.Property && member.TargetResult.Type.Kind != TypeKind.Anonymous && !this.Emitter.Validator.IsObjectLiteral(member.Member.DeclaringTypeDefinition))
                {
                    if (!this.Emitter.IsAssignment)
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
                else if (member.Member.SymbolKind == SymbolKind.Field)
                {
                    bool isConst = this.Emitter.IsMemberConst(member.Member);

                    if (isConst && this.Emitter.IsInlineConst(member.Member))
                    {
                        this.WriteScript(member.ConstantValue);
                    }
                    else
                    {
                        bool changeCase = (!this.Emitter.IsNativeMember(member.Member.FullName) ? this.Emitter.ChangeCase : true) && !isConst;
                        this.Write(this.Emitter.GetEntityName(member.Member, !changeCase));
                    }
                }
                else if (resolveResult is InvocationResolveResult)
                {
                    InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                    this.Write(this.Emitter.GetOverloadNameInvocationResolveResult(invocationResult));
                }
                else if (member.Member is DefaultResolvedEvent && this.Emitter.IsAssignment && (this.Emitter.AssignmentType == AssignmentOperatorType.Add || this.Emitter.AssignmentType == AssignmentOperatorType.Subtract))
                {
                    this.Write(this.Emitter.AssignmentType == AssignmentOperatorType.Add ? "add" : "remove");
                    this.Write(member.Member.Name);
                    this.WriteOpenParentheses();                    
                }
                else
                {
                    this.Write(this.Emitter.GetEntityName(member.Member));

                    if (appendAdditionalCode != null)
                    {
                        this.Write(appendAdditionalCode);
                    }
                }
            }
        }        
    }
}
