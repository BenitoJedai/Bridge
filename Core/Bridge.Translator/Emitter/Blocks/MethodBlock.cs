using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using System.Text;
using Mono.Cecil;
using Ext.Net.Utilities;
using ICSharpCode.NRefactory.Semantics;

namespace Bridge.NET
{
    public class MethodBlock : AbstractMethodBlock
    {
        public MethodBlock(Emitter emitter, TypeInfo typeInfo, bool staticBlock)
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
                this.EmitMethods(this.TypeInfo.StaticMethods, this.TypeInfo.StaticProperties, this.TypeInfo.StaticEvents);
            }
            else
            {
                this.EmitMethods(this.TypeInfo.InstanceMethods, this.TypeInfo.InstanceProperties, this.TypeInfo.Events);
            }
        }

        protected virtual void EmitMethods(Dictionary<string, List<MethodDeclaration>> methods, Dictionary<string, EntityDeclaration> properties, List<EventDeclaration> events)
        {
            foreach (var e in events)
            {
                foreach (var evtVar in e.Variables)
                {
                    this.EnsureComma();
                    this.EmitEventAccessor(e, evtVar, true);

                    this.WriteComma();
                    this.WriteNewLine();

                    this.EmitEventAccessor(e, evtVar, false);
                    this.Emitter.Comma = true;
                }  
            }
            
            var names = new List<string>(properties.Keys);

            foreach (var name in names)
            {
                var prop = properties[name];

                if (prop is PropertyDeclaration)
                {
                    this.Emitter.VisitPropertyDeclaration((PropertyDeclaration)prop);
                }
                else if (prop is CustomEventDeclaration)
                {
                    this.Emitter.VisitCustomEventDeclaration((CustomEventDeclaration)prop);
                }
            }

            names = new List<string>(methods.Keys);

            foreach (var name in names)
            {
                this.EmitMethodsGroup(methods[name]);
            }
        }

        protected void EmitEventAccessor(EventDeclaration e, VariableInitializer evtVar, bool add)
        {
            string name = evtVar.Name;

            this.Write(add ? "add_" : "remove_", name, " : ");
            this.WriteFunction();
            this.WriteOpenParentheses();
            this.Write("value");
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();
            this.WriteThis();
            this.WriteDot();
            this.Write(this.Emitter.GetEntityName(e));
            this.Write(" = ");
            this.Write(Emitter.ROOT, ".", add ? Emitter.DELEGATE_COMBINE : Emitter.DELEGATE_REMOVE);
            this.WriteOpenParentheses();
            this.WriteThis();
            this.WriteDot();
            this.Write(this.Emitter.GetEntityName(e));
            this.WriteComma();
            this.WriteSpace();
            this.Write("value");
            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();
            this.EndBlock();
        }

        protected virtual void EmitMethodsGroup(List<MethodDeclaration> group)
        {
            if (group.Count == 1)
            {
                if (!group[0].Body.IsNull)
                {
                    this.Emitter.VisitMethodDeclaration(group[0]);
                }
            }
            else
            {
                var typeDef = this.Emitter.GetTypeDefinition();
                var name = group[0].Name;
                var methodsDef = typeDef.Methods.Where(m => m.Name == name);
                MethodDeclaration noArgsMethod = null;
                this.Emitter.MethodsGroup = methodsDef;
                this.Emitter.MethodsGroupBuilder = new Dictionary<int, StringBuilder>();

                foreach (var method in group)
                {
                    if (method.Parameters.Count == 0)
                    {
                        noArgsMethod = method;
                    }
                    else
                    {
                        if (!method.Body.IsNull)
                        {
                            this.Emitter.VisitMethodDeclaration(method);
                        }
                    }
                }

                this.Emitter.MethodsGroup = null;

                if (noArgsMethod == null)
                {
                    noArgsMethod = new MethodDeclaration();
                    noArgsMethod.Name = name;
                    noArgsMethod.Body = new BlockStatement();
                }

                this.Emitter.InjectMethodDetectors = true;
                this.Emitter.VisitMethodDeclaration(noArgsMethod);

                this.Emitter.MethodsGroupBuilder = null;
            }
        }
    }
}
