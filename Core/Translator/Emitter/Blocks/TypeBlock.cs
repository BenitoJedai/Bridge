using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class TypeBlock : AbstractEmitterBlock
    {
        public TypeBlock(Emitter emitter, AstType type)
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
            this.Write(TypeBlock.TranslateTypeReference(astType, this.Emitter));
        }

        public static string TranslateTypeReference(AstType astType, Emitter emitter)
        {
            var composedType = astType as ComposedType;

            if (composedType != null && composedType.ArraySpecifiers != null && composedType.ArraySpecifiers.Count > 0)
            {
                return "Array";
            }

            var simpleType = astType as SimpleType;

            if (simpleType != null && simpleType.Identifier == "dynamic")
            {
                return "Object";
            }

            string type = emitter.ResolveType(Helpers.GetScriptName(astType, true), astType);

            if (String.IsNullOrEmpty(type))
            {
                throw emitter.CreateException(astType, "Cannot resolve type " + astType.ToString());
            }

            return Ext.Net.Utilities.StringUtils.LeftOfRightmostOf(emitter.ShortenTypeName(type), "$");
        }
    }
}
