using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public abstract class AbstractAspectBlock
    {
        public AbstractAspectBlock(Emitter emitter)
        {
            this.Emitter = emitter;
        }

        public Emitter Emitter 
        { 
            get; 
            set; 
        }

        public abstract IEnumerable<string> GetAspects();
    }

    public abstract class ConstructorAspectBlock: AbstractAspectBlock
    {
        public ConstructorAspectBlock(ConstructorBlock constructorBlock) : base(constructorBlock.Emitter)
        {
            this.ConstructorBlock = constructorBlock;
        }

        public ConstructorBlock ConstructorBlock 
        { 
            get; 
            set; 
        }

        protected virtual string GetAspectCode(AspectInfo aspect, EntityDeclaration entity, List<string> excludeTypes)
        {
            if (excludeTypes.Contains(aspect.AspectType))
            {
                return null;
            }

            if (this.Match(aspect, entity))
            {
                if (this.Exclude(aspect, excludeTypes))
                {
                    return null;
                }

                return this.FormatAspect(aspect, entity, this.GetArgList(aspect), this.GetPropList(aspect));
            }

            return null;
        }

        protected abstract string FormatAspect(AspectInfo aspect, EntityDeclaration entity, string argList, string propList);

        protected virtual bool Match(AspectInfo aspect, EntityDeclaration entity)
        {
            var targetMembersAttributes = aspect.TargetMembersAttributes;
            var targetMembers = aspect.TargetMembers;
            var targetTypesAttributes = aspect.TargetTypesAttributes;
            var targetTypes = aspect.TargetTypes;
            var className = this.Emitter.TypeInfo.FullName;

            return AspectHelpers.MatchTargetAttributes(targetMembersAttributes, entity) &&
                AspectHelpers.MatchTargetAttributes(targetTypesAttributes, this.Emitter.TypeInfo.TypeDeclaration) &&
                AspectHelpers.MatchTarget(targetMembers, entity.Name) &&
                AspectHelpers.MatchTarget(targetTypes, className);
        }

        protected virtual bool Exclude(AspectInfo aspect, List<string> excludeTypes)
        {
            var exclude = aspect.Exclude;

            if (exclude)
            {
                excludeTypes.Add(aspect.AspectType);
                return true;
            }

            var multiple = aspect.Multiple;
            var replace = aspect.Replace;

            if (!multiple || replace)
            {
                excludeTypes.Add(aspect.AspectType);
            }

            return false;
        }

        protected virtual string GetPropList(AspectInfo aspect)
        {
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
            return propList;
        }

        protected virtual string GetArgList(AspectInfo aspect)
        {
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
            return argList;
        }        
    }
}
