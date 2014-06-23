using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class FieldBlock : AbstractEmitterBlock
    {
        public FieldBlock(Emitter emitter, TypeInfo typeInfo, bool staticBlock)
        {
            this.Emitter = emitter;
            this.TypeInfo = typeInfo;
            this.StaticBlock = staticBlock;
        }

        public TypeInfo TypeInfo 
        { 
            get; 
            set; 
        }

        public bool StaticBlock
        {
            get;
            set;
        }

        public override void Emit()
        {
            if (this.StaticBlock)
            {
                this.EmitStaticFields();
            }
            else
            {
                this.EmitInstanceFields();
            }
        }

        protected virtual void EmitStaticFields()
        {
            var sortedNames = this.TypeInfo.StaticFields.Count > 0 ? new List<string>(this.TypeInfo.StaticFields.Keys) : new List<string>();
            if (this.TypeInfo.Consts.Count > 0)
            {
                sortedNames.AddRange(this.TypeInfo.Consts.Keys);
            }
            sortedNames.Sort();

            var changeCase = this.Emitter.ChangeCase;

            for (var i = 0; i < sortedNames.Count; i++)
            {
                var fieldName = sortedNames[i];
                var isField = this.TypeInfo.StaticFields.ContainsKey(fieldName);
                string name = null;

                if (this.TypeInfo.FieldsDeclarations.ContainsKey(fieldName))
                {
                    name = this.Emitter.GetEntityName(this.TypeInfo.FieldsDeclarations[fieldName], !isField);
                }
                else
                {
                    name = (changeCase && isField) ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(fieldName) : fieldName;
                    if (Bridge.NET.Emitter.IsReservedStaticName(name))
                    {
                        name = "$" + name;
                    }
                }

                this.Write("this.", name, " = ");

                if (isField)
                {
                    this.TypeInfo.StaticFields[fieldName].AcceptVisitor(this.Emitter);
                }
                else
                {
                    this.TypeInfo.Consts[fieldName].AcceptVisitor(this.Emitter);
                }

                this.WriteSemiColon();
                this.WriteNewLine();
            }
        }

        protected virtual void EmitInstanceFields()
        {            
            var names = new List<string>(this.TypeInfo.InstanceFields.Keys);
            names.Sort();

            foreach (var name in names)
            {
                string fieldName = name;

                if (this.TypeInfo.FieldsDeclarations.ContainsKey(name))
                {
                    fieldName = this.Emitter.GetEntityName(this.TypeInfo.FieldsDeclarations[name]);
                }
                else
                {
                    fieldName = this.Emitter.ChangeCase ? Ext.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;
                }

                this.Write("this.", fieldName, " = ");
                this.TypeInfo.InstanceFields[name].AcceptVisitor(this.Emitter);
                this.WriteSemiColon();
                this.WriteNewLine();
            }
        }

        
    }
}
