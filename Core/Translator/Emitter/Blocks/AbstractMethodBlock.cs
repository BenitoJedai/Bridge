using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public abstract class AbstractMethodBlock : AbstractEmitterBlock
    {
        protected virtual void EmitMethodParameters(IEnumerable<ParameterDeclaration> declarations, AstNode context)
        {
            this.WriteOpenParentheses();
            bool needComma = false;

            foreach (var p in declarations)
            {
                this.Emitter.Validator.CheckIdentifier(p.Name, context);

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(p.Name.Replace(Bridge.NET.Emitter.FIX_ARGUMENT_NAME, ""));
            }

            this.WriteCloseParentheses();
        }

        protected virtual void EmitTypeParameters(IEnumerable<TypeParameterDeclaration> declarations, AstNode context)
        {
            this.WriteOpenParentheses();
            bool needComma = false;

            foreach (var p in declarations)
            {
                this.Emitter.Validator.CheckIdentifier(p.Name, context);

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(p.Name.Replace(Bridge.NET.Emitter.FIX_ARGUMENT_NAME, ""));
            }

            this.WriteCloseParentheses();
        }                
    }
}
