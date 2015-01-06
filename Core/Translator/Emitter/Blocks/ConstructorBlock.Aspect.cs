using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace Bridge.NET
{
    public partial class ConstructorBlock
    {
        protected virtual IEnumerable<string> GetAspects()
        {
            var methods = this.StaticBlock ? this.TypeInfo.StaticMethods : this.TypeInfo.InstanceMethods;
            List<string> list = new List<string>();

            foreach (var methodGroup in methods)
            {
                foreach (var method in methodGroup.Value)
                {
                    Dictionary<string, List<AspectInfo>> aspectInfos = new Dictionary<string, List<AspectInfo>>();
                    
                    foreach (var attrSection in method.Attributes)
                    {
                        foreach (var attr in attrSection.Attributes)
                        {
                            var resolveResult = this.Emitter.Resolver.ResolveNode(attr, this.Emitter);

                            if (resolveResult != null && Inspector.IsAspectAttribute(resolveResult.Type))
                            {
                                if (!aspectInfos.ContainsKey(resolveResult.Type.FullName))
                                {
                                    aspectInfos.Add(resolveResult.Type.FullName, new List<AspectInfo>());
                                }

                                aspectInfos[resolveResult.Type.FullName].Add(Inspector.GetAspectInfo(attr, resolveResult.Type, System.AttributeTargets.Method, this.Emitter.GetEntityName(method)));
                            }
                        }
                    }

                    if (method.HasModifier(Modifiers.Override))
                    {
                        this.FindInheritedAspect(this.Emitter.GetBaseTypeDefinition(), method, aspectInfos);
                    }

                    this.EmitAspect(list, aspectInfos, method);
                }
            }
            
            return list;
        }

        protected virtual void EmitAspect(List<string> list, Dictionary<string, List<AspectInfo>> aspectInfos, MethodDeclaration method)
        {
            var aspects = this.Emitter.AssemblyInfo.Aspects;

            foreach (var aspect in aspectInfos)
            {
                var info = aspect.Value.First();
                list.Add(string.Format(info.Format, aspect.Key, info.TargetName, "this"));
            }
        }

        protected virtual void FindInheritedAspect(TypeDefinition baseType, MethodDeclaration method, Dictionary<string, List<AspectInfo>> aspectInfos)
        {
            var methodDef = baseType.Methods.FirstOrDefault(m => m.Name == method.Name);

            if (methodDef != null && methodDef.CustomAttributes.Count > 0)
            {
                foreach (var attr in methodDef.CustomAttributes)
                {
                    if (Inspector.IsAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        if (!aspectInfos.ContainsKey(attr.AttributeType.FullName))
                        {
                            aspectInfos.Add(attr.AttributeType.FullName, new List<AspectInfo>());
                        }

                        aspectInfos[attr.AttributeType.FullName].Add(Inspector.GetMethodAspectInfo(attr, this.Emitter, this.Emitter.GetEntityName(method)));
                    }
                }
            }

            if (methodDef != null && methodDef.IsVirtual)
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
