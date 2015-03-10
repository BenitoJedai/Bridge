using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using Object.Net.Utilities;
using Bridge.Contract;

namespace Bridge.NET
{
    public class VisitorCustomEventBlock : AbstractMethodBlock
    {
        public VisitorCustomEventBlock(IEmitter emitter, CustomEventDeclaration customEventDeclaration)
        {
            this.Emitter = emitter;
            this.CustomEventDeclaration = customEventDeclaration;
        }

        public CustomEventDeclaration CustomEventDeclaration
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.EmitPropertyMethod(this.CustomEventDeclaration, this.CustomEventDeclaration.AddAccessor, false);
            this.EmitPropertyMethod(this.CustomEventDeclaration, this.CustomEventDeclaration.RemoveAccessor, true);
        }

        protected virtual void EmitPropertyMethod(CustomEventDeclaration customEventDeclaration, Accessor accessor, bool remover)
        {
            if (!accessor.IsNull && this.Emitter.GetInline(accessor) == null)
            {
                this.EnsureComma();

                this.ResetLocals();

                this.AddLocals(new ParameterDeclaration[] { new ParameterDeclaration { Name = "value" } });

                this.Write((remover ? "remove" : "add") + customEventDeclaration.Name);
                this.WriteColon();
                this.WriteFunction();
                this.WriteOpenParentheses();
                this.Write("value");
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
