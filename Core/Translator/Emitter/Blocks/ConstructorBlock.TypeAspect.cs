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
        protected virtual IEnumerable<string> GetTypeAspects()
        {
            List<string> list = new List<string>();
            this.EmitTypeAspect(list, this.Emitter.GetTypeDefinition());            

            return list;
        }

        protected virtual void EmitTypeAspect(List<string> list, TypeDefinition type)
        {
            var globalAspects = this.Emitter.AssemblyInfo.Aspects;
            var excludeTypes = new List<string>();
            var typeName = type.FullName;
            
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
                        if (aspectTarget == AttributeTargets.Class && info.TargetName != typeName || !info.IsTypeAspect(this.Emitter))
                        {
                            continue;
                        }

                        var code = this.GetAspectCode(false, info, this.TypeInfo.TypeDeclaration, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }                

                if (aspectTarget == AttributeTargets.Class)
                {
                    var inheritedAspects = new List<AspectInfo>();
                    this.FindClassInheritedAspects(this.Emitter.GetTypeDefinition(), type, inheritedAspects);

                    foreach (var info in inheritedAspects)
                    {
                        var code = this.GetAspectCode(false, info, this.TypeInfo.TypeDeclaration, excludeTypes);
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
