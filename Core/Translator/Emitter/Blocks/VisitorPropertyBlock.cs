using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using Object.Net.Utilities;
using Bridge.Contract;
using ICSharpCode.NRefactory.Semantics;
using System.Linq;

namespace Bridge.NET
{
    public class VisitorPropertyBlock : AbstractMethodBlock
    {
        public VisitorPropertyBlock(IEmitter emitter, PropertyDeclaration propertyDeclaration)
        {
            this.Emitter = emitter;
            this.PropertyDeclaration = propertyDeclaration;
        }

        public PropertyDeclaration PropertyDeclaration
        {
            get;
            set;
        }

        public override void Emit()
        {
            if (this.PropertyDeclaration.Getter.Body.IsNull && this.PropertyDeclaration.Setter.Body.IsNull)
            {
                return;
            }

            this.EmitPropertyMethod(this.PropertyDeclaration, this.PropertyDeclaration.Getter, false);
            this.EmitPropertyMethod(this.PropertyDeclaration, this.PropertyDeclaration.Setter, true);
        }

        protected virtual void EmitPropertyMethod(PropertyDeclaration propertyDeclaration, Accessor accessor, bool setter)
        {
            var memberResult = this.Emitter.Resolver.ResolveNode(propertyDeclaration, this.Emitter) as MemberResolveResult;

            if (memberResult != null && 
                (memberResult.Member.Attributes.Any(a => a.AttributeType.FullName == "Bridge.FieldPropertyAttribute") ||
                (propertyDeclaration.Getter.IsNull && propertyDeclaration.Setter.IsNull)))
            {
                return;
            }

            if (!accessor.IsNull && this.Emitter.GetInline(accessor) == null)
            {
                this.EnsureComma();

                this.ResetLocals();

                if (setter)
                {
                    this.AddLocals(new ParameterDeclaration[] { new ParameterDeclaration { Name = "value" } });
                }

                var overloads = OverloadsCollection.Create(this.Emitter, propertyDeclaration, setter);
                string name = propertyDeclaration.Name;
                if (overloads.HasOverloads)
                {
                    name = overloads.GetOverloadName();
                }
                this.Write((setter ? "set" : "get") + name);
                this.WriteColon();
                this.WriteFunction();
                this.WriteOpenParentheses();
                this.Write(setter ? "value" : "");
                this.WriteCloseParentheses();
                this.WriteSpace();

                var script = this.Emitter.GetScript(accessor);

                if (script == null)
                {
                        accessor.Body.AcceptVisitor(this.Emitter);




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

                this.Emitter.Comma = true;
            }
        }
    }
}
