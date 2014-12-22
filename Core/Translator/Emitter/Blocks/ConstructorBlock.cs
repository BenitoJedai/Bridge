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
    public class ConstructorBlock : AbstractMethodBlock
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

        protected virtual IEnumerable<string> GetEvents()
        {
            var methods = this.StaticBlock ? this.TypeInfo.StaticMethods : this.TypeInfo.InstanceMethods;
            List<string> list = new List<string>();

            foreach (var methodGroup in methods)
            {
                foreach (var method in methodGroup.Value)
                {
                    foreach (var attrSection in method.Attributes)
                    {
                        foreach (var attr in attrSection.Attributes)
                        {
                            var resolveresult = this.Emitter.Resolver.ResolveNode(attr, this.Emitter) as InvocationResolveResult;

                            if (resolveresult != null)
                            {
                                var baseTypes = resolveresult.Type.GetAllBaseTypes().ToArray();

                                if (baseTypes.Any(t => t.FullName == "Bridge.CLR.EventAttribute"))
                                {
                                    if (methodGroup.Value.Count > 1)
                                    {
                                        throw new Exception("Overloaded method cannot be event handler");
                                    }

                                    string eventName = methodGroup.Key;
                                    var eventField = resolveresult.Type.GetFields(f => f.Name == "Event");
                                    if (eventField.Count() > 0)
                                    {
                                        eventName = eventField.First().ConstantValue.ToString();
                                    }

                                    string format = null;
                                    var formatField = resolveresult.Type.GetFields(f => f.Name == "Format", GetMemberOptions.IgnoreInheritedMembers);
                                    if (formatField.Count() > 0)
                                    {
                                        format = formatField.First().ConstantValue.ToString();
                                    }
                                    else
                                    {
                                        for (int i = baseTypes.Length - 1; i >= 0; i--)
                                        {
                                            formatField = baseTypes[i].GetFields(f => f.Name == "Format");
                                            if (formatField.Count() > 0)
                                            {
                                                format = formatField.First().ConstantValue.ToString();
                                                break;
                                            }    
                                        }
                                    }

                                    bool isCommon = false;
                                    var commonField = resolveresult.Type.GetFields(f => f.Name == "IsCommonEvent");
                                    if (commonField.Count() > 0)
                                    {
                                        isCommon = Convert.ToBoolean(commonField.First().ConstantValue);
                                    }

                                    if (isCommon)
                                    {
                                        var eventArg = attr.Arguments.First();
                                        var primitiveArg = eventArg  as ICSharpCode.NRefactory.CSharp.PrimitiveExpression;
                                        
                                        if (primitiveArg != null)
                                        {
                                            eventName = primitiveArg.Value.ToString();
                                        }
                                        else
                                        {
                                            var memberArg = eventArg as MemberReferenceExpression;
                                            
                                            if (memberArg != null)
                                            {
                                                var memberResolveResult = this.Emitter.Resolver.ResolveNode(memberArg, this.Emitter) as MemberResolveResult;

                                                if (memberResolveResult != null)
                                                {
                                                    eventName = this.Emitter.GetEntityName(memberResolveResult.Member);
                                                }                                                
                                            }
                                        }
                                    }

                                    int selectorIndex = isCommon ? 1 : 0;
                                    string selector = null;
                                    if (attr.Arguments.Count > selectorIndex)
                                    {
                                        selector = ((ICSharpCode.NRefactory.CSharp.PrimitiveExpression)(attr.Arguments.ElementAt(selectorIndex))).Value.ToString();
                                    }
                                    else
                                    {
                                        var resolvedmethod = resolveresult.Member as IMethod;
                                        if (resolvedmethod.Parameters.Count > selectorIndex)
                                        {
                                            selector = resolvedmethod.Parameters[selectorIndex].ConstantValue.ToString();
                                        }
                                    }

                                    if (attr.Arguments.Count > (selectorIndex + 1))
                                    {
                                        var memberResolveResult = this.Emitter.Resolver.ResolveNode(attr.Arguments.ElementAt(selectorIndex + 1), this.Emitter) as MemberResolveResult;
                                        if (memberResolveResult != null && memberResolveResult.Member.Attributes.Count > 0)
                                        {
                                            var template = this.Emitter.Validator.GetAttribute(memberResolveResult.Member.Attributes, "Bridge.CLR.TemplateAttribute");

                                            if (template != null)
                                            {
                                                selector = string.Format(template.PositionalArguments.First().ConstantValue.ToString(), selector);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var resolvedmethod = resolveresult.Member as IMethod;
                                        if (resolvedmethod.Parameters.Count > (selectorIndex + 1))
                                        {
                                            var templateType = resolvedmethod.Parameters[selectorIndex + 1].Type;
                                            var templateValue = Convert.ToInt32(resolvedmethod.Parameters[selectorIndex + 1].ConstantValue);
                                            var fields = templateType.GetFields(f => 
                                            {
                                                var field = f as DefaultResolvedField;
                                                if (field != null && field.ConstantValue != null && Convert.ToInt32(field.ConstantValue.ToString()) == templateValue)
                                                {
                                                    return true;
                                                }

                                                var field1 = f as DefaultUnresolvedField;
                                                if (field1 != null && field1.ConstantValue != null && Convert.ToInt32(field1.ConstantValue.ToString()) == templateValue)
                                                {
                                                    return true;
                                                }

                                                return false;
                                            }, GetMemberOptions.IgnoreInheritedMembers);

                                            if (fields.Count() > 0)
                                            {
                                                var template = this.Emitter.Validator.GetAttribute(fields.First().Attributes, "Bridge.CLR.TemplateAttribute");

                                                if (template != null)
                                                {
                                                    selector = string.Format(template.PositionalArguments.First().ConstantValue.ToString(), selector);
                                                }
                                            }
                                        }
                                    }

                                    list.Add(string.Format(format, eventName, selector, this.Emitter.GetEntityName(method)));
                                }
                            }
                        }
                    }
                }
            }
            
            return list;
        }

        protected virtual void EmitCtorForStaticClass()
        {
            var handlers = this.GetEvents();

            if (this.TypeInfo.StaticCtor != null || this.TypeInfo.StaticFields.Count > 0 || this.TypeInfo.Consts.Count > 0 || this.TypeInfo.StaticEvents.Count > 0 || handlers.Count() > 0)
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

                foreach (var fn in handlers)
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
            var handlers = this.GetEvents();

            if (this.TypeInfo.InstanceFields.Count == 0 && this.TypeInfo.Events.Count == 0 && handlers.Count() == 0)
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

            foreach (var fn in handlers)
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
                MethodDefinition methodDef = this.FindMethodDefinitionInGroup(ctor.Parameters, null, methodsDef);
                if (methodDef != null)
                {
                    string name = this.GetOverloadName(methodDef);
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
