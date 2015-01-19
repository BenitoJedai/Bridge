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
        private IEnumerable<string> aspects;
        public virtual IEnumerable<string> GetAspects()
        {
            if (this.aspects != null)
            {
                return this.aspects;
            }

            var methods = this.GetMethodsAspects();
            var properties = this.GetPropertiesAspects();
            var types = this.GetTypeAspects();

            this.aspects = types.Concat(methods).Concat(properties);
            return this.aspects;
        }

        protected virtual string GetAspectCode(bool isGroup, AspectInfo aspect, EntityDeclaration entity, List<string> excludeTypes)
        {
            if (excludeTypes.Contains(aspect.AspectType))
            {
                return null;
            }

            var targetMembersAttributes = aspect.TargetMembersAttributes;
            var targetMembers = aspect.TargetMembers;
            var targetTypesAttributes = aspect.TargetTypesAttributes;
            var targetTypes = aspect.TargetTypes;

            string className = this.Emitter.TypeInfo.FullName;

            if (AspectHelpers.MatchTargetAttributes(targetMembersAttributes, entity) &&
                AspectHelpers.MatchTargetAttributes(targetTypesAttributes, this.Emitter.TypeInfo.TypeDeclaration) &&
                AspectHelpers.MatchTarget(targetMembers, entity.Name) &&
                AspectHelpers.MatchTarget(targetTypes, className))
            {
                var exclude = aspect.Exclude;

                if (exclude)
                {
                    excludeTypes.Add(aspect.AspectType);
                    return null;
                }

                var multiple = aspect.Multiple;
                var replace = aspect.Replace;
                var inheritance = aspect.Inheritance;

                if (!multiple || replace)
                {
                    excludeTypes.Add(aspect.AspectType);                    
                }

                string argList = "";

                if (aspect.ConstructorArguments.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    bool comma = false;
                    foreach (var arg in aspect.ConstructorArguments)
                    {
                        if (comma)
                        {
                            sb.Append(", ");
                        }

                        sb.Append(this.Emitter.ToJavaScript(arg));
                        comma = true;
                    }

                    argList = sb.ToString();
                }

                string propList = "";

                if (aspect.CustomProperties.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    bool comma = false;
                    foreach (var arg in aspect.CustomProperties)
                    {
                        if (comma)
                        {
                            sb.Append(", ");
                        }

                        sb.AppendFormat("set{0}:{1}", arg.Key, this.Emitter.ToJavaScript(arg.Value));
                        comma = true;
                    }

                    propList = sb.ToString();
                }

                var name = isGroup ? null : entity is PropertyDeclaration ? entity.Name : this.Emitter.GetEntityName(entity);
                if (isGroup)
                {
                    var thisType = this.Emitter.GetTypeDefinition();
                    var methodDef = Helpers.GetMethodDefinition(this.Emitter, (MethodDeclaration)entity, thisType);
                    name = AbstractMethodBlock.GetOverloadName(this.Emitter, methodDef);
                }

                if (propList.Length > 0 && aspect.MergeFormat != null)
                {
                    return string.Format(aspect.MergeFormat, aspect.AspectType, name, "this", argList, propList);
                }
                
                return string.Format(aspect.Format, aspect.AspectType, name, "this", argList);
            }

            return null;
        }
    }
}
