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
        protected virtual IEnumerable<string> GetPropertiesAspects()
        {
            var properties = this.StaticBlock ? this.TypeInfo.StaticProperties : this.TypeInfo.InstanceProperties;

            List<string> list = new List<string>();
            var typeDef = this.Emitter.GetTypeDefinition();

            foreach (var property in properties)
            {
                List<AspectInfo> aspectInfos = new List<AspectInfo>();
                var propertyDef = Helpers.GetPropertyDefinition(this.Emitter, property.Value, typeDef);

                if (propertyDef != null)
                {
                    if (propertyDef.CustomAttributes.Count > 0)
                    {
                        foreach (var attr in propertyDef.CustomAttributes)
                        {
                            if (AspectHelpers.IsPropertyAspectAttribute(attr.AttributeType, this.Emitter))
                            {
                                //var name = this.Emitter.GetEntityName(property.Value);
                                var name = property.Value.Name;
                                var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Property, name);
                                aspectInfos.Add(info);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var attrSection in property.Value.Attributes)
                    {
                        foreach (var attr in attrSection.Attributes)
                        {
                            var resolveResult = this.Emitter.Resolver.ResolveNode(attr, this.Emitter);

                            if (resolveResult != null && AspectHelpers.IsPropertyAspectAttribute(resolveResult.Type))
                            {
                                aspectInfos.Add(AspectHelpers.GetAspectInfo(this.Emitter, attr, resolveResult.Type, System.AttributeTargets.Property, property.Value.Name));
                            }
                        }
                    }
                }

                if (property.Value.HasModifier(Modifiers.Override))
                {
                    this.FindPropertyInheritedAspect(this.Emitter.GetBaseTypeDefinition(), property.Value, aspectInfos);
                }

                this.EmitPropertyAspect(list, aspectInfos, property.Value);
            }
            

            return list;
        }

        protected virtual void EmitPropertyAspect(List<string> list, List<AspectInfo> aspectInfos, EntityDeclaration property)
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
                var code = this.GetAspectCode(false, info, property, excludeTypes);
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
                        if (aspectTarget == AttributeTargets.Class && info.TargetName != typeName || !info.IsPropertyAspect(this.Emitter))
                        {
                            continue;
                        }

                        var code = this.GetAspectCode(false, info, property, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }

                if (aspectTarget == AttributeTargets.Class)
                {
                    var inheritedAspects = new List<AspectInfo>();
                    this.FindClassInheritedAspects(this.Emitter.GetTypeDefinition(), property, inheritedAspects);

                    foreach (var info in inheritedAspects)
                    {
                        if (!info.IsPropertyAspect(this.Emitter))
                        {
                            continue;
                        }
                        var code = this.GetAspectCode(false, info, property, excludeTypes);
                        if (code != null && code.Length > 0)
                        {
                            list.Add(code);
                        }
                    }
                }
            }
        }

        protected virtual void FindClassInheritedAspects(TypeDefinition baseType, EntityDeclaration property, List<AspectInfo> aspectInfos)
        {
            if (baseType.CustomAttributes.Count > 0)
            {
                var thisType = this.Emitter.GetTypeDefinition();
                foreach (var attr in baseType.CustomAttributes)
                {
                    if (AspectHelpers.IsPropertyAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var methodDef = Helpers.GetPropertyDefinition(this.Emitter, property, baseType);
                        //var name = this.Emitter.GetEntityName(property);
                        var name = property.Name;
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Property, name);

                        if (thisType.FullName == baseType.FullName || info.Inheritance == TranslatorMulticastInheritance.All || (info.Inheritance == TranslatorMulticastInheritance.Strict && property.HasModifier(Modifiers.Override)))
                        {
                            aspectInfos.Add(info);
                        }
                    }
                }
            }

            baseType = this.Emitter.GetBaseTypeDefinition(baseType);
            if (baseType != null)
            {
                this.FindClassInheritedAspects(baseType, property, aspectInfos);
            }
        }

        protected virtual void FindPropertyInheritedAspect(TypeDefinition baseType, EntityDeclaration property, List<AspectInfo> aspectInfos)
        {
            var propertyDef = Helpers.GetPropertyDefinition(this.Emitter, property, baseType);

            if (propertyDef != null && propertyDef.CustomAttributes.Count > 0)
            {
                foreach (var attr in propertyDef.CustomAttributes)
                {
                    if (AspectHelpers.IsPropertyAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        //var name = this.Emitter.GetEntityName(property);
                        var name = property.Name;
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Property, name);

                        if (info.Inheritance != TranslatorMulticastInheritance.None)
                        {
                            aspectInfos.Add(info);
                        }
                    }
                }
            }

            if (propertyDef != null && propertyDef.GetMethod.IsVirtual && propertyDef.GetMethod.IsNewSlot)
            {
                return;
            }

            baseType = this.Emitter.GetBaseTypeDefinition(baseType);
            if (baseType != null)
            {
                this.FindPropertyInheritedAspect(baseType, property, aspectInfos);
            }
        }
    }
}
