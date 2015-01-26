using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class ParameterAspectBlock : ConstructorAspectBlock
    {
        public ParameterAspectBlock(ConstructorBlock block)
            : base(block)
        {
        }

        protected bool IsGroup
        {
            get;
            set;
        }

        public override IEnumerable<string> GetAspects()
        {
            var block = this.ConstructorBlock;
            var methods = block.StaticBlock ? block.TypeInfo.StaticMethods : block.TypeInfo.InstanceMethods;
            var properties = block.StaticBlock ? block.TypeInfo.StaticProperties : block.TypeInfo.InstanceProperties;

            List<string> list = new List<string>();
            var typeDef = this.Emitter.GetTypeDefinition();

            foreach (var methodGroup in methods)
            {
                this.IsGroup = methodGroup.Value.Count > 1;

                foreach (var method in methodGroup.Value)
                {
                    if (method.Parameters.Count == 0)
                    {
                        continue;
                    }
                    
                    List<AspectInfo> aspectInfos = new List<AspectInfo>();
                    var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, typeDef);                    

                    if (methodDef != null)
                    {
                        var methodName = this.IsGroup ? AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef) : this.Emitter.GetEntityName(method);
                        var isOverride = method.HasModifier(Modifiers.Override);
                        foreach (var prm in methodDef.Parameters)
                        {
                            if (prm.CustomAttributes.Count > 0)
                            {
                                foreach (var attr in prm.CustomAttributes)
                                {
                                    if (AspectHelpers.IsParameterAspectAttribute(attr.AttributeType, this.Emitter)) 
                                    {                                    
                                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Parameter, prm.Name);
                                        info.AdditionalInfo["index"] = prm.Index.ToString();
                                        info.AdditionalInfo["name"] = prm.Name;
                                        info.AdditionalInfo["type"] = TypeBlock.TranslateTypeReference(method.Parameters.ElementAt(prm.Index).Type, this.Emitter);
                                        info.AdditionalInfo["methodName"] = this.GetMethodName(method);
                                        aspectInfos.Add(info);
                                    }
                                }
                            }

                            if (isOverride)
                            {
                                this.FindInheritedAspect(this.Emitter.GetBaseTypeDefinition(), method, aspectInfos, prm.Index);
                            }
                        }                        
                    }                    

                    this.EmitAspect(list, aspectInfos, method);
                }
            }

            this.IsGroup = false;
            foreach (var property in properties)
            {
                var propDec = (PropertyDeclaration)property.Value;

                if (propDec.Setter == null || propDec.Setter.IsNull)
                {
                    continue;
                }

                List<AspectInfo> aspectInfos = new List<AspectInfo>();
                var propertyDef = Helpers.GetPropertyDefinition(this.Emitter, property.Value, typeDef);

                if (propertyDef != null)
                {
                    if (propertyDef.CustomAttributes.Count > 0)
                    {
                        foreach (var attr in propertyDef.CustomAttributes)
                        {
                            if (AspectHelpers.IsParameterAspectAttribute(attr.AttributeType, this.Emitter))
                            {
                                var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Parameter, "value");
                                info.AdditionalInfo["index"] = "0";
                                info.AdditionalInfo["name"] = "value";
                                info.AdditionalInfo["type"] = TypeBlock.TranslateTypeReference(propDec.ReturnType, this.Emitter);
                                info.AdditionalInfo["methodName"] = this.GetMethodName(propDec);
                                aspectInfos.Add(info);
                            }
                        }
                    }
                }

                if (property.Value.HasModifier(Modifiers.Override))
                {
                    this.FindPropertyInheritedAspect(this.Emitter.GetBaseTypeDefinition(), property.Value, aspectInfos);
                }

                this.EmitAspect(list, aspectInfos, property.Value);
            }

            return list;
        }

        protected virtual string GetMethodName(EntityDeclaration entity)
        {
            if (entity is PropertyDeclaration)
            {
                return "set" + entity.Name;
            }
            
            var name = this.IsGroup ? null : this.Emitter.GetEntityName(entity);
            if (this.IsGroup)
            {
                var thisType = this.Emitter.GetTypeDefinition();
                var methodDef = Helpers.GetMethodDefinition(this.Emitter, (MethodDeclaration)entity, thisType);
                name = AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef);
            }

            return name;
        }

        protected override string FormatAspect(AspectInfo aspect, EntityDeclaration entity, string argList, string propList)
        {
            var methodName = aspect.AdditionalInfo["methodName"];
            var argName = aspect.AdditionalInfo["name"];
            var argIndex = aspect.AdditionalInfo["index"];
            var argType = aspect.AdditionalInfo["type"];

            var typeName = aspect.ClientType ?? aspect.AspectType;
            if (propList.Length > 0 && aspect.MergeFormat != null)
            {
                return string.Format(aspect.MergeFormat, typeName, methodName, "this", argList, propList, argName, argIndex, argType);
            }

            return string.Format(aspect.Format, typeName, methodName, "this", argList, argName, argIndex, argType);
        }

        protected virtual void EmitAspect(List<string> list, List<AspectInfo> aspectInfos, EntityDeclaration method)
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
        }

        protected virtual void FindInheritedAspect(TypeDefinition baseType, MethodDeclaration method, List<AspectInfo> aspectInfos, int index)
        {
            var methodDef = Helpers.GetMethodDefinition(this.Emitter, method, baseType);

            if (methodDef != null && methodDef.Parameters.Count > index)
            {
                var prm = methodDef.Parameters.ElementAt(index);

                foreach (var attr in prm.CustomAttributes)
                {
                    if (AspectHelpers.IsParameterAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Parameter, prm.Name);
                        info.AdditionalInfo["index"] = prm.Index.ToString();
                        info.AdditionalInfo["name"] = prm.Name;
                        info.AdditionalInfo["type"] = TypeBlock.TranslateTypeReference(method.Parameters.ElementAt(prm.Index).Type, this.Emitter);
                        info.AdditionalInfo["methodName"] = this.GetMethodName(method);

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
                this.FindInheritedAspect(baseType, method, aspectInfos, index);
            }
        }

        protected virtual void FindPropertyInheritedAspect(TypeDefinition baseType, EntityDeclaration property, List<AspectInfo> aspectInfos)
        {
            var propDec = (PropertyDeclaration)property;
            var propertyDef = Helpers.GetPropertyDefinition(this.Emitter, property, baseType);

            if (propertyDef != null && propertyDef.CustomAttributes.Count > 0)
            {
                foreach (var attr in propertyDef.CustomAttributes)
                {
                    if (AspectHelpers.IsParameterAspectAttribute(attr.AttributeType, this.Emitter))
                    {
                        var info = AspectHelpers.GetAspectInfo(attr, this.Emitter, AttributeTargets.Parameter, "value");
                        info.AdditionalInfo["index"] = "0";
                        info.AdditionalInfo["name"] = "value";
                        info.AdditionalInfo["type"] = TypeBlock.TranslateTypeReference(propDec.ReturnType, this.Emitter);
                        info.AdditionalInfo["methodName"] = this.GetMethodName(propDec);
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
