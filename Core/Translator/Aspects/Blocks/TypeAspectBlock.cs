using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class TypeAspectBlock : ConstructorAspectBlock
    {
        public TypeAspectBlock(ConstructorBlock block) : base(block)
        {
        }

        public override IEnumerable<string> GetAspects()
        {
            List<string> list = new List<string>();
            this.EmitTypeAspect(list, this.Emitter.GetTypeDefinition());

            return list;
        }

        protected override string FormatAspect(AspectInfo aspect, EntityDeclaration entity, string argList, string propList)
        {
            var name = this.Emitter.GetEntityName(entity);
            var typeName = aspect.ClientType ?? aspect.AspectType;

            if (propList.Length > 0 && aspect.MergeFormat != null)
            {
                return string.Format(aspect.MergeFormat, typeName, name, "this", argList, propList);
            }

            return string.Format(aspect.Format, typeName, name, "this", argList);
        }

        protected virtual void EmitTypeAspect(List<string> list, TypeDefinition type)
        {
            var block = this.ConstructorBlock;
            var globalAspects = this.Emitter.AssemblyInfo.Aspects;
            var excludeTypes = new List<string>();
            var typeName = type.FullName;

            var inheritedAspects = new List<AspectInfo>();
            this.FindClassInheritedAspects(this.Emitter.GetTypeDefinition(), type, inheritedAspects);

            foreach (var info in inheritedAspects)
            {
                var code = this.GetAspectCode(info, block.TypeInfo.TypeDeclaration, excludeTypes);
                if (code != null && code.Length > 0)
                {
                    list.Add(code);
                }
            }

            var targetKeys = new AttributeTargets[] { AttributeTargets.Assembly };

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
                        if (!info.IsTypeAspect(this.Emitter))
                        {
                            continue;
                        }

                        var code = this.GetAspectCode(info, block.TypeInfo.TypeDeclaration, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }                
            }
        }

        protected virtual void FindClassInheritedAspects(TypeDefinition baseType, TypeDefinition thisType, List<AspectInfo> aspectInfos)
        {
            if (baseType.CustomAttributes.Count > 0)
            {
                foreach (var attr in baseType.CustomAttributes)
                {
                    if (AspectHelpers.IsTypeAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Class, thisType.FullName);

                        if (baseType.FullName == thisType.FullName || info.Inheritance == TranslatorMulticastInheritance.All || info.Inheritance == TranslatorMulticastInheritance.Strict)
                        {
                            aspectInfos.Add(info);
                        }
                    }
                }
            }

            baseType = this.Emitter.GetBaseTypeDefinition(baseType);
            if (baseType != null)
            {
                this.FindClassInheritedAspects(baseType, thisType, aspectInfos);
            }
        }
    }
}
