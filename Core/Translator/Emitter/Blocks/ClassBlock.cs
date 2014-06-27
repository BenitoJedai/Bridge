using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using System.Text;
using Mono.Cecil;
using Ext.Net.Utilities;

namespace Bridge.NET
{
    public class ClassBlock : AbstractEmitterBlock
    {
        public ClassBlock(Emitter emitter, TypeInfo typeInfo)
        {
            this.Emitter = emitter;
            this.TypeInfo = typeInfo;
        }

        public TypeInfo TypeInfo
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.EmitClassHeader();
            this.EmitStaticBlock();
            this.EmitInstantiableBlock();
            this.EmitClassEnd();    
        }        

        protected virtual void EmitClassHeader()
        {
            TypeDefinition baseType = this.Emitter.GetBaseTypeDefinition();

            this.Write(Bridge.NET.Emitter.ROOT + ".Class.extend");
            this.WriteOpenParentheses();

            var typeDef = this.Emitter.TypeDefinitions[this.TypeInfo.GenericFullName];
            string name = this.Emitter.Validator.GetCustomTypeName(typeDef);

            if (name.IsEmpty())
            {
                name = this.TypeInfo.FullName;
            }

            this.Write("'" + name, "', ");
            this.BeginBlock();

            string extend = this.Emitter.GetTypeHierarchy();

            if (extend.IsNotEmpty() && !this.TypeInfo.IsEnum)
            {
                this.Write("$extend");
                this.WriteColon();
                this.Write(extend);
                this.Emitter.Comma = true;
            }

            if (this.TypeInfo.Module != null)
            {
                this.EnsureComma();
                this.Write("$scope");
                this.WriteColon();
                this.Write("exports");
                this.Emitter.Comma = true;
            }
        }

        protected virtual void EmitStaticBlock()
        {
            if (this.TypeInfo.HasStatic)
            {
                this.EnsureComma();
                this.Write("$statics");
                this.WriteColon();
                this.BeginBlock();

                new ConstructorBlock(this.Emitter, this.TypeInfo, true).Emit();                
                new MethodBlock(this.Emitter, this.TypeInfo, true).Emit();

                this.WriteNewLine();
                this.EndBlock();
                this.Emitter.Comma = true;
            }
        }

        protected virtual void EmitInstantiableBlock()
        {
            if (this.TypeInfo.HasInstantiable)
            {
                this.EnsureComma();
                new ConstructorBlock(this.Emitter, this.TypeInfo, false).Emit();
                new MethodBlock(this.Emitter, this.TypeInfo, false).Emit();
            }
            else
            {
                this.Emitter.Comma = false;
            }
        }

        protected virtual void EmitClassEnd()
        {
            this.WriteNewLine();
            this.EndBlock();
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();
            this.WriteNewLine();
        }
    }
}
