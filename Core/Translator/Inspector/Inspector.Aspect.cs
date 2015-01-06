using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Ext.Net.Utilities;
using System.Linq;
using ICSharpCode.NRefactory.Semantics;
using Mono.Cecil;

namespace Bridge.NET
{
    public partial class Inspector
    {
        protected virtual bool ReadAspect(ICSharpCode.NRefactory.CSharp.Attribute attr, string name, ResolveResult resolveResult, AttributeTargets target, string targetName)
        {
            if (Inspector.IsMulticastAspectAttribute(resolveResult.Type))
            {
                var globalAspects = this.AssemblyInfo.Aspects;

                Dictionary<AttributeTargets, List<AspectInfo>> targetAspects;
                if (globalAspects.ContainsKey(resolveResult.Type.FullName))
                {
                    targetAspects = globalAspects[resolveResult.Type.FullName];
                }
                else
                {
                    targetAspects = new Dictionary<AttributeTargets, List<AspectInfo>>();
                    globalAspects.Add(resolveResult.Type.FullName, targetAspects);
                }

                List<AspectInfo> aspects;
                if (targetAspects.ContainsKey(target))
                {
                    aspects = targetAspects[target];
                }
                else
                {
                    aspects = new List<AspectInfo>();
                    targetAspects.Add(target, aspects);
                }

                AspectInfo aspect = Inspector.GetAspectInfo(attr, resolveResult.Type, target, targetName);

                aspects.Add(aspect);
            }

            return false;
        }

        public static AspectInfo GetAspectInfo(ICSharpCode.NRefactory.CSharp.Attribute attr, IType type, AttributeTargets target, string targetName)
        {
            AspectInfo aspect = new AspectInfo();

            aspect.Target = target;
            aspect.AspectType = type.FullName;
            aspect.Type = type;
            aspect.Format = Inspector.GetAspectFormat(type);
            aspect.TargetName = targetName;

            foreach (var arg in attr.Arguments)
            {
                NamedExpression namedExression = arg as NamedExpression;
                NamedArgumentExpression namedArgumentExpression = arg as NamedArgumentExpression;

                if (namedExression != null)
                {
                    var primitive = namedExression.Expression as PrimitiveExpression;

                    if (primitive != null)
                    {
                        aspect.Properties.Add(namedExression.Name, primitive.Value);
                    }
                }
            }
            return aspect;
        }

        public static AspectInfo GetMethodAspectInfo(CustomAttribute attr, Emitter emitter, string targetName)
        {
            var type = attr.AttributeType;
            AspectInfo aspect = new AspectInfo();
            
            aspect.Target = AttributeTargets.Method;
            aspect.AspectType = type.FullName;
            aspect.TypeReference = type;
            aspect.Format = Inspector.GetAspectFormat(type, emitter);
            aspect.TargetName = targetName;

            foreach (var arg in attr.Properties)
            {
                aspect.Properties.Add(arg.Name, arg.Argument.Value);
            }
            return aspect;
        }

        public static bool IsMulticastAspectAttribute(IType type)
        {
            return Inspector.IsTypeAttribute(type, "Bridge.CLR.MulticastAspectAttribute");
        }

        public static bool IsAspectAttribute(IType type)
        {
            return Inspector.IsTypeAttribute(type, "Bridge.CLR.AspectAttribute");
        }

        public static bool IsAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(type.Resolve(), emitter.TypeDefinitions["Bridge.CLR.AspectAttribute"], emitter);
        }

        public static bool IsTypeAttribute(IType type, string name)
        {
            if (type.FullName == name)
            {
                return true;
            }

            if (type.DirectBaseTypes != null)
            {
                foreach (var baseType in type.DirectBaseTypes)
                {
                    if (Inspector.IsTypeAttribute(baseType, name))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetAspectFormat(IType type)
        {
            string format = null;
            var formatField = type.GetFields(f => f.Name == "Format", GetMemberOptions.IgnoreInheritedMembers);
            if (formatField.Count() > 0)
            {
                format = formatField.First().ConstantValue.ToString();
            }
            else
            {
                var baseTypes = type.GetAllBaseTypes().ToArray();
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

            return format;
        }

        public static string GetAspectFormat(TypeReference type, Emitter emitter)
        {
            string format = null;
            var typeDef = Helpers.ToTypeDefinition(type, emitter);

            if (typeDef == null)
            {
                return null;
            }

            var formatField = typeDef.Fields.FirstOrDefault(f => f.Name == "Format");
            if (formatField != null)
            {
                format = formatField.Constant.ToString();
            }
            else
            {
                var baseType = typeDef.BaseType;

                if (baseType != null)
                {
                    return Inspector.GetAspectFormat(Helpers.ToTypeDefinition(baseType, emitter), emitter);
                }
            }

            return format;
        }
    }
}
