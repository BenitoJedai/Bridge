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
                this.EmitMethods(this.TypeInfo.StaticMethods, this.TypeInfo.StaticProperties);
            }
            else
            {
                this.EmitMethods(this.TypeInfo.InstanceMethods, this.TypeInfo.InstanceProperties);
            }
        }

        protected virtual void EmitMethods(Dictionary<string, List<MethodDeclaration>> methods, Dictionary<string, PropertyDeclaration> properties)
        {
            var names = new List<string>(properties.Keys);

            foreach (var name in names)
            {
                this.Emitter.VisitPropertyDeclaration(properties[name]);
            }

            names = new List<string>(methods.Keys);

            foreach (var name in names)
            {
                this.EmitMethodsGroup(methods[name]);
            }
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
