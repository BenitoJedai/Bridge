using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public class TypeBlock : AbstractEmitterBlock
    {
        public TypeBlock(IEmitter emitter, AstType type)
        {
            this.Emitter = emitter;
            this.Type = type;
        }

        public AstType Type 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.EmitTypeReference();
        }

        protected virtual void EmitTypeReference()
        {
            AstType astType = this.Type;
            this.Write(Helpers.TranslateTypeReference(astType, this.Emitter));
        }        

        public static string GetTypeName(IType type, IEmitter emitter)
        {
            if (type.Kind == TypeKind.Array)
            {
                return "Array";
            }

            if (type.Kind == TypeKind.Dynamic)
            {
                return "Object";
            }

            return emitter.ShortenTypeName(Helpers.GetScriptFullName(type));
        }
    }
}
