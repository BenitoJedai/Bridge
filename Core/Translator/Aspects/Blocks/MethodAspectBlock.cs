using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class MethodAspectBlock : ConstructorAspectBlock
    {
        public MethodAspectBlock(ConstructorBlock block) : base(block)
        {
        }

        protected bool IsGroup
        {
            get;
            set;
        }

        public override IEnumerable<string> GetAspects()
        {
            var methods = this.ConstructorBlock.StaticBlock ? this.ConstructorBlock.TypeInfo.StaticMethods : this.ConstructorBlock.TypeInfo.InstanceMethods;
            List<string> list = new List<string>();
            var typeDef = this.Emitter.GetTypeDefinition();

            foreach (var methodGroup in methods)
            {
                this.IsGroup = methodGroup.Value.Count > 1;

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
                                    var name = this.IsGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
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
                        this.FindInheritedAspect(this.Emitter.GetBaseTypeDefinition(), method, aspectInfos);
                    }

                    this.EmitMethodAspect(list, aspectInfos, method);
                }
            }

            return list;
        }        

        protected override string FormatAspect(AspectInfo aspect, EntityDeclaration entity, string argList, string propList)
        {
            var name = this.IsGroup ? null : this.Emitter.GetEntityName(entity);
            if (this.IsGroup)
            {
                var thisType = this.Emitter.GetTypeDefinition();
                var methodDef = Helpers.GetMethodDefinition(this.Emitter, (MethodDeclaration)entity, thisType);
                name = AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef);
            }

            var typeName = aspect.ClientType ?? aspect.AspectType;
            if (propList.Length > 0 && aspect.MergeFormat != null)
            {
                return string.Format(aspect.MergeFormat, typeName, name, "this", argList, propList);
            }

            return string.Format(aspect.Format, typeName, name, "this", argList);
        }

        protected virtual void EmitMethodAspect(List<string> list, List<AspectInfo> aspectInfos, MethodDeclaration method)
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
                var code = this.GetAspectCode(info, method, excludeTypes);
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

                        var code = this.GetAspectCode(info, method, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }

                if (aspectTarget == AttributeTargets.Class)
                {
                    var inheritedAspects = new List<AspectInfo>();
                    this.FindClassInheritedAspects(this.Emitter.GetTypeDefinition(), method, inheritedAspects);

                    foreach (var info in inheritedAspects)
                    {
                        if (!info.IsMethodAspect(this.Emitter))
                        {
                            continue;
                        }
                        var code = this.GetAspectCode(info, method, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }
            }
        }

        protected virtual void FindClassInheritedAspects(TypeDefinition baseType, MethodDeclaration method, List<AspectInfo> aspectInfos)
        {
            if (baseType.CustomAttributes.Count > 0)
            {
                var thisType = this.Emitter.GetTypeDefinition();

                foreach (var attr in baseType.CustomAttributes)
                {
                    if (AspectHelpers.IsMethodAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, baseType);
                        var name = this.IsGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
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
                this.FindClassInheritedAspects(baseType, method, aspectInfos);
            }
        }

        protected virtual void FindInheritedAspect(TypeDefinition baseType, MethodDeclaration method, List<AspectInfo> aspectInfos)
        {
            var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, baseType);

            if (methodDef != null && methodDef.CustomAttributes.Count > 0)
            {
                foreach (var attr in methodDef.CustomAttributes)
                {
                    if (AspectHelpers.IsMethodAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var name = this.IsGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
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
                this.FindInheritedAspect(baseType, method, aspectInfos);
            }
        }
    }
}
