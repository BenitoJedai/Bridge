using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using Object.Net.Utilities;
using System.Text;

namespace Bridge.NET
{
    public class MemberReferenceBlock : ConversionBlock
    {
        public MemberReferenceBlock(IEmitter emitter, MemberReferenceExpression memberReferenceExpression)
        {
            this.Emitter = emitter;
            this.MemberReferenceExpression = memberReferenceExpression;
        }

        public MemberReferenceExpression MemberReferenceExpression 
        { 
            get; 
            set; 
        }

        protected override Expression GetExpression()
        {
            return this.MemberReferenceExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.VisitMemberReferenceExpression();
        }

        protected void VisitMemberReferenceExpression()
        {
            MemberReferenceExpression memberReferenceExpression = this.MemberReferenceExpression;

            ResolveResult resolveResult = null;
            if (memberReferenceExpression.Parent is InvocationExpression && (((InvocationExpression)(memberReferenceExpression.Parent)).Target == memberReferenceExpression))
            {
                resolveResult = this.Emitter.Resolver.ResolveNode(memberReferenceExpression.Parent, this.Emitter);
            }
            else
            {
                resolveResult = this.Emitter.Resolver.ResolveNode(memberReferenceExpression, this.Emitter);
            }
            bool oldIsAssignment = this.Emitter.IsAssignment;

            if (resolveResult == null && !(resolveResult is ErrorResolveResult))
            {
                this.Emitter.IsAssignment = false;
                memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                this.Emitter.IsAssignment = oldIsAssignment;
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

            if (hasThis)
            {
                this.Write("");
                var oldBuilder = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();
                this.Emitter.IsAssignment = false;
                memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                this.Emitter.IsAssignment = oldIsAssignment;
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

            if (member != null && member.Member.SymbolKind == SymbolKind.Field && this.Emitter.IsMemberConst(member.Member) && this.Emitter.IsInlineConst(member.Member))
            {
                this.WriteScript(member.ConstantValue);
            }
            else if (hasInline && member.Member.IsStatic)
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
                            var attr = this.Emitter.GetAttribute(member.Member.Attributes, Translator.Bridge_ASSEMBLY + ".NameAttribute");

                            if (attr != null)
                            {
                                enumStringName = this.Emitter.GetEntityName(member.Member);
                            }
                            else
                            {
                                switch (enumMode)
                                {
                                    case 3:
                                        enumStringName = Object.Net.Utilities.StringUtils.ToLowerCamelCase(member.Member.Name);
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
                    bool isStatic = resolvedMethod != null && resolvedMethod.IsStatic;                    

                    var isExtensionMethod = resolvedMethod.IsExtensionMethod;

                    this.Emitter.IsAssignment = false;
                    if (!isStatic)
                    {
                        this.Write(Bridge.NET.Emitter.ROOT + "." + (isExtensionMethod ? Bridge.NET.Emitter.DELEGATE_BIND_SCOPE : Bridge.NET.Emitter.DELEGATE_BIND) + "(");
                        memberReferenceExpression.Target.AcceptVisitor(this.Emitter);                        
                        this.Write(", ");
                    }
               
                    this.Emitter.IsAssignment = oldIsAssignment;
                    

                    if (isExtensionMethod)
                    {
                        this.Write(this.Emitter.ShortenTypeName(resolvedMethod.DeclaringType.FullName));
                    }
                    else
                    {
                        this.Emitter.IsAssignment = false;
                        memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                        this.Emitter.IsAssignment = oldIsAssignment;
                    }

                    this.WriteDot();

                    this.Write(OverloadsCollection.Create(this.Emitter, member.Member).GetOverloadName());
                    if (!isStatic)
                    {
                        this.Write(")");
                    }

                    return;
                }
                else
                {
                    this.Emitter.IsAssignment = false;
                    memberReferenceExpression.Target.AcceptVisitor(this.Emitter);
                    this.Emitter.IsAssignment = oldIsAssignment;
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
                    if (Helpers.IsFieldProperty(member.Member, this.Emitter))
                    {
                        this.Write(Helpers.GetPropertyRef(member.Member, this.Emitter));
                    }
                    else if (!this.Emitter.IsAssignment)
                    {
                        this.Write(Helpers.GetPropertyRef(member.Member, this.Emitter));
                        this.WriteOpenParentheses();
                        this.WriteCloseParentheses();
                    }
                    else
                    {
                        this.PushWriter(Helpers.GetPropertyRef(member.Member, this.Emitter, true) + "({0})");
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
                        this.Write(OverloadsCollection.Create(this.Emitter, member.Member).GetOverloadName());                        
                    }
                }
                else if (resolveResult is InvocationResolveResult)
                {
                    InvocationResolveResult invocationResult = (InvocationResolveResult)resolveResult;
                    this.Write(OverloadsCollection.Create(this.Emitter, invocationResult.Member).GetOverloadName());
                }
                else if (member.Member is DefaultResolvedEvent && this.Emitter.IsAssignment && (this.Emitter.AssignmentType == AssignmentOperatorType.Add || this.Emitter.AssignmentType == AssignmentOperatorType.Subtract))
                {
                    this.Write(this.Emitter.AssignmentType == AssignmentOperatorType.Add ? "add" : "remove");
                    this.Write(OverloadsCollection.Create(this.Emitter, member.Member).GetOverloadName());
                    this.WriteOpenParentheses();                    
                }
                else
                {
                    this.Write(this.Emitter.GetEntityName(member.Member));                    
                }

                Helpers.CheckValueTypeClone(resolveResult, memberReferenceExpression, this);
            }
        }        
    }
}
