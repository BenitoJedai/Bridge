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
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace Bridge.NET
{
    public partial class ConstructorBlock : AbstractMethodBlock
    {
        public ConstructorBlock(Emitter emitter, TypeInfo typeInfo, bool staticBlock)
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
                this.EmitCtorForStaticClass();
            }
            else
            {
                this.EmitCtorForInstantiableClass();
            }
        }

        protected virtual IEnumerable<string> GetInjectors()
        {
            var handlers = this.GetEvents();
            var aspects = this.GetAspects();

            return aspects.Concat(handlers);
        }

        protected virtual void EmitCtorForStaticClass()
        {
            var injectors = this.GetInjectors();

            if (this.TypeInfo.StaticCtor != null || this.TypeInfo.StaticFields.Count > 0 || this.TypeInfo.Consts.Count > 0 || this.TypeInfo.StaticEvents.Count > 0 || injectors.Count() > 0)
            {
                this.Write("$init");
                this.WriteColon();
                this.WriteFunction();
                this.WriteOpenCloseParentheses(true);

                this.BeginBlock();                

                new FieldBlock(this.Emitter, this.TypeInfo, true).Emit();

                if (this.TypeInfo.StaticEvents.Count > 0)
                {
                    new EventBlock(this.Emitter, this.TypeInfo.StaticEvents).Emit();
                }

                if (this.TypeInfo.StaticCtor != null)
                {
                    var ctor = this.TypeInfo.StaticCtor;

                    if (ctor.Body.HasChildren)
                    {
                        ctor.Body.AcceptChildren(this.Emitter);
                    }
                }

                foreach (var fn in injectors)
                {
                    this.Write(fn);
                    this.WriteNewLine();
                }

                this.EndBlock();
                this.Emitter.Comma = true;
            }
        }

        protected virtual void EmitInitMembers()
        {
            var injectors = this.GetInjectors();

            if (this.TypeInfo.InstanceFields.Count == 0 && this.TypeInfo.Events.Count == 0 && injectors.Count() == 0)
            {
                return;
            }

            this.Write("$initMembers");

            this.WriteColon();
            this.WriteFunction();
            this.WriteOpenParentheses();
            this.WriteCloseParentheses();
            this.WriteSpace();
            this.BeginBlock();

            var requireNewLine = false;
            var changeCase = this.Emitter.ChangeCase;
            var baseType = this.Emitter.GetBaseTypeDefinition();

            if (!this.Emitter.Validator.IsIgnoreType(baseType))
            {
                this.Write("this.base();");
                this.WriteNewLine();
            }

            if (this.TypeInfo.InstanceFields.Count > 0)
            {
                new FieldBlock(this.Emitter, this.TypeInfo, false).Emit();
                requireNewLine = true;
            }

            if (this.TypeInfo.Events.Count > 0)
            {
                if (requireNewLine)
                {
                    this.WriteNewLine();
                }
                new EventBlock(this.Emitter, this.TypeInfo.Events).Emit();
                requireNewLine = true;
            }

            foreach (var fn in injectors)
            {
                this.Write(fn);
                this.WriteNewLine();
            }

            this.EndBlock();
            this.Emitter.Comma = true;
        }

        protected virtual void EmitCtorForInstantiableClass()
        {            
            this.EmitInitMembers();

            this.EnsureComma();

            var baseType = this.Emitter.GetBaseTypeDefinition();
            var typeDef = this.Emitter.GetTypeDefinition();

            if (typeDef.IsValueType)
            {
                this.TypeInfo.Ctors.Add(new ConstructorDeclaration
                {
                    Modifiers = Modifiers.Public,
                    Body = new BlockStatement()
                });
            }

            if (this.TypeInfo.Ctors.Count > 1)
            {
                this.Write("$multipleCtors");
                this.WriteColon();
                this.WriteScript(true);
                this.Emitter.Comma = true;
            }

            if (this.TypeInfo.Ctors.Count > 1)
            {
                this.EmitCtorDetector(this.TypeInfo.Ctors);
            }

            foreach (var ctor in this.TypeInfo.Ctors)
            {
                this.EnsureComma();
                this.ResetLocals();
                var prevMap = this.BuildLocalsMap(ctor.Body);
                this.AddLocals(ctor.Parameters);

                var ctorName = "$init";

                if (this.TypeInfo.Ctors.Count > 1)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var p in ctor.Parameters)
                    {
                        var resolveResult = this.Emitter.Resolver.ResolveNode(p.Type, this.Emitter);

                        if (resolveResult is TypeResolveResult)
                        {
                            var type = ((TypeResolveResult)resolveResult).Type;
                            sb.Append("$").Append(type.Name);

                            if (type.TypeParameterCount > 0)
                            {
                                sb.Append("$").Append(type.TypeParameterCount);
                            }
                        }
                        else
                        {
                            sb.Append("$").Append(p.Type.ToString());
                        }
                    }

                    ctorName += sb.ToString();
                }

                this.Write(ctorName);

                this.WriteColon();
                this.WriteFunction();

                this.EmitMethodParameters(ctor.Parameters, ctor);

                this.WriteSpace();
                this.BeginBlock();

                var requireNewLine = false;
                var changeCase = this.Emitter.ChangeCase;

                if (baseType != null && !this.Emitter.Validator.IsIgnoreType(baseType) ||
                    (ctor.Initializer != null && ctor.Initializer.ConstructorInitializerType == ConstructorInitializerType.This))
                {
                    if (requireNewLine)
                    {
                        this.WriteNewLine();
                    }
                    this.EmitBaseConstructor(ctor, ctorName);
                    requireNewLine = true;
                }

                var script = this.Emitter.GetScript(ctor);

                if (script == null)
                {
                    if (ctor.Body.HasChildren)
                    {
                        if (requireNewLine)
                        {
                            this.WriteNewLine();
                        }

                        ctor.Body.AcceptChildren(this.Emitter);
                    }
                }
                else
                {
                    if (requireNewLine)
                    {
                        this.WriteNewLine();
                    }

                    foreach (var line in script)
                    {
                        this.Write(line);
                        this.WriteNewLine();
                    }
                }

                this.EndBlock();
                this.Emitter.Comma = true;
                this.ClearLocalsMap(prevMap);
            }
        }

        protected virtual void EmitCtorDetector(List<ConstructorDeclaration> ctors)
        {
            var methodsDef = this.Emitter.GetTypeDefinition().Methods.Where(m => m.IsConstructor);
            Dictionary<int, StringBuilder> detectorBuilders = new Dictionary<int, StringBuilder>();
            this.EnsureComma();

            this.Write("$ctorDetector: function () ");
            this.BeginBlock();

            foreach (var ctor in ctors)
            {
                MethodDefinition methodDef = AbstractMethodBlock.FindMethodDefinitionInGroup(this.Emitter, ctor.Parameters, null, methodsDef);
                if (methodDef != null)
                {
                    string name = AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef);
                    this.EmitMethodDetector(detectorBuilders, methodDef, name);
                }
                else
                {
                    StringBuilder sb = null;
                    if (!detectorBuilders.ContainsKey(0))
                    {
                        sb = new StringBuilder();
                        detectorBuilders.Add(0, sb);
                    }
                    else
                    {
                        sb = detectorBuilders[0];
                    }

                    sb.AppendLine("if (arguments.length == 0) {");
                    sb.AppendLine("    this.$init();");
                    sb.AppendLine("}");
                }
            }

            string detectors = Ext.Net.Utilities.StringUtils.ReplaceLastInstanceOf(this.GetMethodsDetector(detectorBuilders), Environment.NewLine, "");

            this.Write(this.WriteIndentToString(detectors));
            this.WriteNewLine();
            this.EndBlock();
            this.Emitter.Comma = true;
        }

        protected virtual void EmitBaseConstructor(ConstructorDeclaration ctor, string ctorName)
        {
            var initializer = ctor.Initializer != null && !ctor.Initializer.IsNull ? ctor.Initializer : new ConstructorInitializer()
            {
                ConstructorInitializerType = ConstructorInitializerType.Base
            };

            bool appendScope = false;

            if (initializer.ConstructorInitializerType == ConstructorInitializerType.Base)
            {
                var baseType = this.Emitter.GetBaseTypeDefinition();
                var baseName = (ctor.Initializer != null && !ctor.Initializer.IsNull) ? this.Emitter.GetOverloadNameInvocationResolveResult(this.Emitter.Resolver.ResolveNode(ctor.Initializer, this.Emitter) as InvocationResolveResult) : "$init";

                if (baseName == ctorName)
                {
                    this.Write("this.base");
                }
                else
                {
                    this.Write(this.Emitter.ShortenTypeName(Helpers.GetScriptFullName(baseType)), ".prototype.");
                    this.Write(baseName);
                    this.Write(".call");
                    appendScope = true;
                }
            }
            else
            {
                this.WriteThis();
                this.WriteDot();
                this.Write(this.Emitter.GetOverloadNameInvocationResolveResult(this.Emitter.Resolver.ResolveNode(ctor.Initializer, this.Emitter) as InvocationResolveResult));
            }

            this.WriteOpenParentheses();

            if (appendScope)
            {
                this.WriteThis();

                if (initializer.Arguments.Count > 0)
                {
                    this.WriteComma();
                }
            }

            var args = new List<Expression>(initializer.Arguments);
            for (int i = 0; i < args.Count; i++)
            {
                args[i].AcceptVisitor(this.Emitter);
                if (i != (args.Count - 1))
                {
                    this.WriteComma();
                }
            }

            this.WriteCloseParentheses();
            this.WriteSemiColon();
            this.WriteNewLine();
        }        
    }
}
