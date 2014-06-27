using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class VisitorMethodBlock : AbstractMethodBlock
    {
        public VisitorMethodBlock(Emitter emitter, MethodDeclaration methodDeclaration)
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
            this.AddLocals(methodDeclaration.Parameters);

            if (this.Emitter.MethodsGroup != null)
            {
                MethodDefinition methodDef = this.FindMethodDefinitionInGroup(methodDeclaration.Parameters, methodDeclaration.TypeParameters, this.Emitter.MethodsGroup);
                string name = this.GetOverloadName(methodDef);
                this.EmitMethodDetector(this.Emitter.MethodsGroupBuilder, methodDef, name);

                this.Write(name);
            }
            else
            {
                this.Write(this.Emitter.GetEntityName(methodDeclaration));
            }

            this.WriteColon();
            this.WriteFunction();

            this.EmitMethodParameters(methodDeclaration.Parameters, methodDeclaration);

            this.WriteSpace();

            var script = this.Emitter.GetScript(methodDeclaration);

            if (script == null)
            {
                methodDeclaration.Body.AcceptVisitor(this.Emitter);
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
