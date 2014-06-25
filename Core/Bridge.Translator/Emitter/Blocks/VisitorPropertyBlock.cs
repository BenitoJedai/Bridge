using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using Ext.Net.Utilities;

namespace Bridge.NET
{
    public class VisitorPropertyBlock : AbstractMethodBlock
    {
        public VisitorPropertyBlock(Emitter emitter, PropertyDeclaration propertyDeclaration)
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
            this.EmitPropertyMethod(this.PropertyDeclaration, this.PropertyDeclaration.Getter, false);
            this.EmitPropertyMethod(this.PropertyDeclaration, this.PropertyDeclaration.Setter, true);
        }

        protected virtual void EmitPropertyMethod(PropertyDeclaration propertyDeclaration, Accessor accessor, bool setter)
        {
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
                        bool isReserved = propertyDeclaration.HasModifier(Modifiers.Static) && Emitter.IsReservedStaticName(propertyDeclaration.Name);

                        this.BeginBlock();

                        if (setter)
                        {
                            this.Write("this.Emitter." + (isReserved ? "$" : "") + propertyDeclaration.Name.ToLowerCamelCase() + " = value;");
                        }
                        else
                        {
                            this.WriteReturn(true);
                            this.Write("this.Emitter." + (isReserved ? "$" : "") + propertyDeclaration.Name.ToLowerCamelCase());
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
