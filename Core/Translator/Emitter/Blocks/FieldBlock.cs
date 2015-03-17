using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class FieldBlock : AbstractEmitterBlock
    {
        public FieldBlock(IEmitter emitter, ITypeInfo typeInfo, bool staticBlock)
        {
            this.Emitter = emitter;
            this.TypeInfo = typeInfo;
            this.StaticBlock = staticBlock;
        }

        public ITypeInfo TypeInfo 
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

            var autoPropNames = new List<string>();
            var fieldNames = new List<string>();
            foreach (var name in sortedNames)
            {
                if (this.TypeInfo.AutoProperties.Contains(name))
                {
                    autoPropNames.Add(name);
                }
                else
                {
                    fieldNames.Add(name);
                }
            }

            if (fieldNames.Count > 0)
            {
                this.WriteStaticObject("fields", fieldNames);
                this.Emitter.Comma = true;
            }

            if (this.TypeInfo.StaticEvents.Count > 0)
            {
                this.WriteEvents("events", this.TypeInfo.StaticEvents);
                this.Emitter.Comma = true;
            }

            if (autoPropNames.Count > 0)
            {
                this.WriteStaticObject("properties", autoPropNames);
                this.Emitter.Comma = true;
            }
        }

        protected virtual void WriteStaticObject(string objectName, List<string> sortedNames, bool closeBlock = true)
        {
            if (objectName != null)
            {
                this.EnsureComma();
                this.Write(objectName);

                this.WriteColon();
                this.BeginBlock();
            }

            var changeCase = this.Emitter.ChangeCase;

            for (var i = 0; i < sortedNames.Count; i++)
            {
                this.EnsureComma();

                var fieldName = sortedNames[i];
                var isField = this.TypeInfo.StaticFields.ContainsKey(fieldName);
                string name = null;

                if (this.TypeInfo.FieldsDeclarations.ContainsKey(fieldName))
                {
                    name = this.Emitter.GetEntityName(this.TypeInfo.FieldsDeclarations[fieldName], !isField);
                }
                else
                {
                    name = (changeCase && isField) ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(fieldName) : fieldName;
                    if (Bridge.NET.Emitter.IsReservedStaticName(name) || Helpers.IsReservedWord(name))
                    {
                        name = "$" + name;
                    }
                }

                this.Write(name);
                this.WriteColon();

                if (isField)
                {
                    var typeExpr = this.TypeInfo.StaticFields[fieldName];
                    var primitiveExpr = typeExpr as PrimitiveExpression;
                    if (primitiveExpr != null && primitiveExpr.Value is AstType)
                    {
                        this.Write("new " + Helpers.TranslateTypeReference((AstType)primitiveExpr.Value, this.Emitter) + "()");
                    }
                    else
                    {
                        typeExpr.AcceptVisitor(this.Emitter);
                    }
                }
                else
                {
                    this.TypeInfo.Consts[fieldName].AcceptVisitor(this.Emitter);
                }

                this.Emitter.Comma = true;
            }

            if (closeBlock)
            {
                this.WriteNewLine();
                this.EndBlock();
            }
        }

        protected virtual void EmitInstanceFields()
        {            
            var names = new List<string>(this.TypeInfo.InstanceFields.Keys);
            names.Sort();

            var autoPropNames = new List<string>();
            var fieldNames = new List<string>();
            foreach (var name in names)
            {
                if (this.TypeInfo.AutoProperties.Contains(name))
                {
                    autoPropNames.Add(name);
                }
                else
                {
                    fieldNames.Add(name);
                }
            }

            if (fieldNames.Count > 0)
            {
                this.WriteObject("fields", fieldNames);
                this.Emitter.Comma = true;
            }

            if (this.TypeInfo.Events.Count > 0)
            {
                this.WriteEvents("events", this.TypeInfo.Events);
                this.Emitter.Comma = true;
            }

            if (autoPropNames.Count > 0)
            {
                this.WriteObject("properties", autoPropNames);
                this.Emitter.Comma = true;
            }
        }

        protected virtual void WriteEvents(string objectName, IEnumerable<EventDeclaration> events)
        {
            if (objectName != null)
            {
                this.EnsureComma();
                this.Write(objectName);

                this.WriteColon();
                this.BeginBlock();
            }

            foreach (var evt in events)
            {
                foreach (var evtVar in evt.Variables)
                {
                    this.EnsureComma();
                    string name = OverloadsCollection.Create(this.Emitter, evt).GetOverloadName();

                    this.Write(name);
                    this.WriteColon();
                    evtVar.Initializer.AcceptVisitor(this.Emitter);
                    this.Emitter.Comma = true;                    
                }
            }

            if (objectName != null)
            {
                this.WriteNewLine();
                this.EndBlock();
            }
        }

        protected virtual void WriteObject(string objectName, List<string> names, bool closeBlock = true)
        {
            if (objectName != null)
            {
                this.EnsureComma();
                this.Write(objectName);

                this.WriteColon();
                this.BeginBlock();
            }

            foreach (var name in names)
            {
                this.EnsureComma();
                string fieldName = name;

                if (this.TypeInfo.FieldsDeclarations.ContainsKey(name))
                {
                    fieldName = OverloadsCollection.Create(this.Emitter, this.TypeInfo.FieldsDeclarations[name]).GetOverloadName();
                }
                else if (this.TypeInfo.InstanceProperties.ContainsKey(name))
                {
                    fieldName = OverloadsCollection.Create(this.Emitter, (PropertyDeclaration)this.TypeInfo.InstanceProperties[name], false).GetOverloadName();
                }
                else
                {
                    fieldName = this.Emitter.ChangeCase ? Object.Net.Utilities.StringUtils.ToLowerCamelCase(name) : name;
                    if (Helpers.IsReservedWord(fieldName))
                    {
                        fieldName = "$" + fieldName;
                    }
                }

                this.Write(fieldName);
                this.WriteColon();

                var typeExpr = this.TypeInfo.InstanceFields[name];
                var primitiveExpr = typeExpr as PrimitiveExpression;
                if (primitiveExpr != null && primitiveExpr.Value is AstType)
                {
                    this.Write("new " + Helpers.TranslateTypeReference((AstType)primitiveExpr.Value, this.Emitter) + "()");
                }
                else
                {
                    typeExpr.AcceptVisitor(this.Emitter);
                }

                this.Emitter.Comma = true;
            }

            if (closeBlock)
            {
                this.WriteNewLine();
                this.EndBlock();
            }
        }        
    }
}
