using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class VisitorMethodBlock : AbstractMethodBlock
    {
        public VisitorMethodBlock(IEmitter emitter, MethodDeclaration methodDeclaration)
        {
            this.Emitter = emitter;
            this.MethodDeclaration = methodDeclaration;
        }

        public MethodDeclaration MethodDeclaration
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.VisitMethodDeclaration(this.MethodDeclaration);
        }

        protected void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            this.EnsureComma();
            this.ResetLocals();
            var prevMap = this.BuildLocalsMap(methodDeclaration.Body);
            this.AddLocals(methodDeclaration.Parameters);

            var typeDef = this.Emitter.GetTypeDefinition();
            var overloads = OverloadsCollection.Create(this.Emitter, methodDeclaration);

            if (overloads.HasOverloads)
            {
                string name = overloads.GetOverloadName();
                this.Write(name);
            }
            else
            {
                this.Write(this.Emitter.GetEntityName(methodDeclaration));
            }

            this.WriteColon();

            if (methodDeclaration.TypeParameters.Count > 0)
            {
                this.WriteFunction();
                this.EmitTypeParameters(methodDeclaration.TypeParameters, methodDeclaration);
                this.WriteSpace();
                this.BeginBlock();
                this.WriteReturn(true);
            }


            this.WriteFunction();

            this.EmitMethodParameters(methodDeclaration.Parameters, methodDeclaration);

            this.WriteSpace();

            var script = this.Emitter.GetScript(methodDeclaration);

            if (script == null)
            {
                if (methodDeclaration.HasModifier(Modifiers.Async))
                {
                    new AsyncBlock(this.Emitter, methodDeclaration).Emit();
                }
                else
                {
                    methodDeclaration.Body.AcceptVisitor(this.Emitter);
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

            if (methodDeclaration.TypeParameters.Count > 0)
            {
                this.WriteNewLine();
                this.EndBlock();
            }

            this.ClearLocalsMap(prevMap);
            this.Emitter.Comma = true;
        }                  
    }
}
