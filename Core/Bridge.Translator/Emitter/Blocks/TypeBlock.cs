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
            var composedType = astType as ComposedType;

            if (composedType != null && composedType.ArraySpecifiers != null && composedType.ArraySpecifiers.Count > 0)
            {
                this.Write("Array");
            }
            else
            {
                string type = this.Emitter.ResolveType(Helpers.GetScriptName(astType, true));

                if (String.IsNullOrEmpty(type))
                {
                    throw this.Emitter.CreateException(astType, "Cannot resolve type " + astType.ToString());
                }

                this.Write(Ext.Net.Utilities.StringUtils.LeftOfRightmostOf(this.Emitter.ShortenTypeName(type), "$"));
            }
        }    
    }
}
