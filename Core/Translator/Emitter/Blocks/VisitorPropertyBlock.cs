using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using Ext.Net.Utilities;
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

            if (memberResult != null && memberResult.Member.Attributes.Any(a => a.AttributeType.FullName == "Bridge.Foundation.FieldPropertyAttribute"))
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

                this.Write((setter ? "set" : "get") + propertyDeclaration.Name);
                this.WriteColon();
                this.WriteFunction();
                this.WriteOpenParentheses();
                this.Write(setter ? "value" : "");
                this.WriteCloseParentheses();
                this.WriteSpace();

                var script = this.Emitter.GetScript(accessor);

                if (script == null)
                {
                    if (!accessor.Body.IsNull)
                    {
                        accessor.Body.AcceptVisitor(this.Emitter);
                    }
                    else
                    {
                        bool isReserved = propertyDeclaration.HasModifier(Modifiers.Static) && Bridge.NET.Emitter.IsReservedStaticName(propertyDeclaration.Name);

                        this.BeginBlock();

                        if (setter)
                        {
                            this.Write("this." + (isReserved ? "$" : "") + propertyDeclaration.Name.ToLowerCamelCase() + " = value;");
                        }
                        else
                        {
                            this.WriteReturn(true);
                            this.Write("this." + (isReserved ? "$" : "") + propertyDeclaration.Name.ToLowerCamelCase());

                            var resolveResult = this.Emitter.Resolver.ResolveNode(propertyDeclaration.ReturnType, this.Emitter);
                            Helpers.CheckValueTypeClone(resolveResult, null, this);
                            this.WriteSemiColon();
                        }

                        this.WriteNewLine();
                        this.EndBlock();
                    }
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
