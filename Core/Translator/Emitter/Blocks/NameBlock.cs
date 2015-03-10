using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class NameBlock : AbstractEmitterBlock
    {
        public NameBlock(IEmitter emitter, NamedExpression namedExpression) : this(emitter, namedExpression.Name, namedExpression, namedExpression.Expression)
        {            
        }

        public NameBlock(IEmitter emitter, string name, Expression namedExpression, Expression expression)
        {
            this.Emitter = emitter;
            this.NamedExpression = namedExpression;
            this.Expression = expression;
            this.Name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public Expression Expression
        {
            get;
            set;
        }

        public Expression NamedExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.EmitNameExpression(this.Name, this.NamedExpression, this.Expression);
        }

        protected virtual void EmitNameExpression(string name, Expression namedExpression, Expression expression)
        {
            var resolveResult = this.Emitter.Resolver.ResolveNode(namedExpression, this.Emitter);
            var lowerCaseName = this.Emitter.ChangeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;

            if (resolveResult != null && resolveResult is MemberResolveResult)
            {
                var member = ((MemberResolveResult)resolveResult).Member;
                lowerCaseName = this.Emitter.GetEntityName(member);

                var isProperty = member.SymbolKind == SymbolKind.Property;

                if (!isProperty)
                {
                    this.Write(lowerCaseName);
                }
                else
                {
                    this.Write(isProperty ? Helpers.GetPropertyRef(member, this.Emitter, !(expression is ArrayInitializerExpression)) : lowerCaseName);                                        
                }
            }
            else
            {
                this.Write(lowerCaseName);
            }

            this.WriteColon();
            expression.AcceptVisitor(this.Emitter);
        }
    }
}
