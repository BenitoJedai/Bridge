using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using System;
using System.Text;

namespace Bridge.NET
{
    public partial class ConstructorBlock
    {
        protected virtual IEnumerable<string> GetMethodsAspects()
        {
            var methods = this.StaticBlock ? this.TypeInfo.StaticMethods : this.TypeInfo.InstanceMethods;
            List<string> list = new List<string>();
            var typeDef = this.Emitter.GetTypeDefinition();

            foreach (var methodGroup in methods)
            {
                var isGroup = methodGroup.Value.Count > 1;

                foreach (var method in methodGroup.Value)
                {
                    List<AspectInfo> aspectInfos = new List<AspectInfo>();
                    var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, typeDef);

                    if (methodDef != null)
                    {
                        if (methodDef.CustomAttributes.Count > 0)
                        {
                            foreach (var attr in methodDef.CustomAttributes)
                            {
                                if (AspectHelpers.IsMethodAspectAttribute(attr.AttributeType, this.Emitter))
                                {
                                    var name = isGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
                                    var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Method, name);
                                    aspectInfos.Add(info);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var attrSection in method.Attributes)
                        {
                            foreach (var attr in attrSection.Attributes)
                            {
                                var resolveResult = this.Emitter.Resolver.ResolveNode(attr, this.Emitter);

                                if (resolveResult != null && AspectHelpers.IsMethodAspectAttribute(resolveResult.Type))
                                {
                                    aspectInfos.Add(AspectHelpers.GetAspectInfo(this.Emitter, attr, resolveResult.Type, System.AttributeTargets.Method, this.Emitter.GetEntityName(method)));
                                }
                            }
                        }
                    }

                    if (method.HasModifier(Modifiers.Override))
                    {
                        this.FindInheritedAspect(isGroup, this.Emitter.GetBaseTypeDefinition(), method, aspectInfos);
                    }

                    this.EmitMethodAspect(isGroup, list, aspectInfos, method);
                }
            }

            return list;
        }

        protected virtual void EmitMethodAspect(bool isGroup, List<string> list, List<AspectInfo> aspectInfos, MethodDeclaration method)
        {
            var globalAspects = this.Emitter.AssemblyInfo.Aspects;
            var excludeTypes = new List<string>();
            var typeName = this.Emitter.GetTypeDefinition().FullName;

            aspectInfos.Sort((a, b) =>
            {
                return a.Priority.CompareTo(b.Priority);
            });

            foreach (var info in aspectInfos)
            {
                var code = this.GetAspectCode(isGroup, info, method, excludeTypes);
                if (code != null && code.Length > 0)
                {
                    list.Add(code);
                }                   
            }

            var targetKeys = new AttributeTargets[] { AttributeTargets.Class, AttributeTargets.Assembly };

            foreach (var aspectTarget in targetKeys)
            {
                if (globalAspects.ContainsKey(aspectTarget))
                {
                    var aspects = globalAspects[aspectTarget];

                    aspects.Sort((a, b) =>
                    {
                        return a.Priority.CompareTo(b.Priority);
                    });

                    foreach (var info in aspects)
                    {
                        if (aspectTarget == AttributeTargets.Class && info.TargetName != typeName || !info.IsMethodAspect(this.Emitter))
                        {
                            continue;
                        }

                        var code = this.GetAspectCode(isGroup, info, method, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }
                
                if (aspectTarget == AttributeTargets.Class)
                {
                    var inheritedAspects = new List<AspectInfo>();
                    this.FindClassInheritedAspects(isGroup, this.Emitter.GetTypeDefinition(), method, inheritedAspects);

                    foreach (var info in inheritedAspects)
                    {
                        if (!info.IsMethodAspect(this.Emitter))
                        {
                            continue;
                        }
                        var code = this.GetAspectCode(isGroup, info, method, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }
            }
        }

        protected virtual void FindClassInheritedAspects(bool isGroup, TypeDefinition baseType, MethodDeclaration method, List<AspectInfo> aspectInfos)
        {
            if (baseType.CustomAttributes.Count > 0)
            {
                var thisType = this.Emitter.GetTypeDefinition();

                foreach (var attr in baseType.CustomAttributes)
                {
                    if (AspectHelpers.IsMethodAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, baseType);
                        var name = isGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Method, name);

                        if (thisType.FullName == baseType.FullName || info.Inheritance == TranslatorMulticastInheritance.All || (info.Inheritance == TranslatorMulticastInheritance.Strict && method.HasModifier(Modifiers.Override)))
                        {
                            aspectInfos.Add(info);
                        }
                    }
                }
            }

            baseType = this.Emitter.GetBaseTypeDefinition(baseType);
            if (baseType != null)
            {
                this.FindClassInheritedAspects(isGroup, baseType, method, aspectInfos);
            }
        }

        protected virtual void FindInheritedAspect(bool isGroup, TypeDefinition baseType, MethodDeclaration method, List<AspectInfo> aspectInfos)
        {
            var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, baseType);

            if (methodDef != null && methodDef.CustomAttributes.Count > 0)
            {
                foreach (var attr in methodDef.CustomAttributes)
                {
                    if (AspectHelpers.IsMethodAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var name = isGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Method, name);

                        if (info.Inheritance != TranslatorMulticastInheritance.None)
                        {
                            aspectInfos.Add(info);
                        }
                    }
                }
            }

            if (methodDef != null && methodDef.IsVirtual && methodDef.IsNewSlot)
            {
                return;
            }

            baseType = this.Emitter.GetBaseTypeDefinition(baseType);
            if (baseType != null)
            {
                this.FindInheritedAspect(isGroup, baseType, method, aspectInfos);
            }
        }
    }
}
