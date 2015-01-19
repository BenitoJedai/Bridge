using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System.Text;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bridge.NET
{
    public static class AspectHelpers 
    {
        public static void ReadAspectTypeProperties(AssemblyInfo assemblyInfo, IType type)
        {
            if (!assemblyInfo.AspectTypeProperties.ContainsKey(type.FullName))
            {
                var properties = new Dictionary<string, object>();
                assemblyInfo.AspectTypeProperties.Add(type.FullName, properties);

                foreach (var attr in type.GetDefinition().Attributes)
                {
                    if (attr.AttributeType.FullName == "Bridge.CLR.MulticastOptionsAttribute")
                    {
                        foreach (var arg in attr.NamedArguments)
                        {
                            properties.Add(arg.Key.Name, arg.Value.ConstantValue);
                        }
                    }
                }
            }
        }

        public static void ReadAspectTypeProperties(Emitter emitter, TypeReference type)
        {
            var assemblyInfo = emitter.AssemblyInfo;

            if (!assemblyInfo.AspectTypeProperties.ContainsKey(type.FullName))
            {
                var properties = new Dictionary<string, object>();
                assemblyInfo.AspectTypeProperties.Add(type.FullName, properties);

                var attributes = Helpers.ToTypeDefinition(type, emitter).CustomAttributes;
                foreach (var attr in attributes)
                {
                    if (attr.AttributeType.FullName == "Bridge.CLR.MulticastOptionsAttribute")
                    {
                        foreach (var arg in attr.Properties)
                        {
                            properties.Add(arg.Name, arg.Argument.Value);
                        }
                    }
                }
            }
        }

        public static AspectInfo GetAspectInfo(Emitter emitter, ICSharpCode.NRefactory.CSharp.Attribute attr, IType type, AttributeTargets target, string targetName)
        {
            AssemblyInfo assemblyInfo = emitter.AssemblyInfo;
            AspectInfo aspect = new AspectInfo(assemblyInfo);

            if (attr.HasArgumentList)
            {
                foreach (var arg in attr.Arguments)
                {
                    var primitiveExpr = arg as PrimitiveExpression;

                    if (primitiveExpr != null)
                    {
                        aspect.ConstructorArguments.Add(primitiveExpr.Value);
                    }
                }
            }

            aspect.Target = target;
            aspect.AspectType = type.FullName;
            aspect.Type = type;
            aspect.Format = AspectHelpers.GetAspectFormat(type, "Format");
            aspect.MergeFormat = AspectHelpers.GetAspectFormat(type, "MergeFormat");
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

            AspectHelpers.ReadAspectTypeProperties(assemblyInfo, type);

            return aspect;
        }

        public static AspectInfo GetAspectInfo(CustomAttribute attr, Emitter emitter, AttributeTargets target, string targetName)
        {
            var type = attr.AttributeType;
            AspectInfo aspect = new AspectInfo(emitter.AssemblyInfo);

            if (attr.HasConstructorArguments)
            {
                foreach (var arg in attr.ConstructorArguments)
                {
                    aspect.ConstructorArguments.Add(arg.Value);
                }
            }

            aspect.Target = target;
            aspect.AspectType = type.FullName;
            aspect.TypeReference = type;
            aspect.Format = AspectHelpers.GetAspectFormat(type, emitter, "Format");
            aspect.MergeFormat = AspectHelpers.GetAspectFormat(type, emitter, "MergeFormat");
            aspect.TargetName = targetName;

            foreach (var arg in attr.Properties)
            {
                aspect.Properties.Add(arg.Name, arg.Argument.Value);
            }

            AspectHelpers.ReadAspectTypeProperties(emitter, type);

            return aspect;
        }

        public static bool IsMulticastAspectAttribute(IType type)
        {
            return AspectHelpers.IsTypeAttribute(type, "Bridge.CLR.MulticastAspectAttribute");
        }

        public static bool IsAspectAttribute(IType type)
        {
            return AspectHelpers.IsTypeAttribute(type, "Bridge.CLR.AspectAttribute");
        }

        public static bool IsMethodAspectAttribute(IType type)
        {
            return AspectHelpers.IsTypeAttribute(type, "Bridge.CLR.MethodAspectAttribute");
        }

        public static bool IsPropertyAspectAttribute(IType type)
        {
            return AspectHelpers.IsTypeAttribute(type, "Bridge.CLR.PropertyAspectAttribute");
        }

        public static bool IsAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions["Bridge.CLR.AspectAttribute"], emitter);
        }

        public static bool IsMulticastAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions["Bridge.CLR.MulticastAspectAttribute"], emitter);
        }

        public static bool IsMethodAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions["Bridge.CLR.MethodAspectAttribute"], emitter);
        }

        public static bool IsTypeAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions["Bridge.CLR.TypeAspectAttribute"], emitter);
        }

        public static bool IsPropertyAspectAttribute(TypeReference type, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions["Bridge.CLR.PropertyAspectAttribute"], emitter);
        }

        public static bool IsTypeAttribute(TypeReference type, string name, Emitter emitter)
        {
            return Helpers.IsSubclassOf(Helpers.ToTypeDefinition(type, emitter), emitter.TypeDefinitions[name], emitter);
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
                    if (AspectHelpers.IsTypeAttribute(baseType, name))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetAspectFormat(IType type, string name)
        {
            string format = null;
            var formatField = type.GetFields(f => f.Name == name, GetMemberOptions.IgnoreInheritedMembers);
            if (formatField.Count() > 0)
            {
                format = formatField.First().ConstantValue.ToString();
            }
            else
            {
                var baseTypes = type.GetAllBaseTypes().ToArray();
                for (int i = baseTypes.Length - 1; i >= 0; i--)
                {
                    formatField = baseTypes[i].GetFields(f => f.Name == name);
                    if (formatField.Count() > 0)
                    {
                        format = formatField.First().ConstantValue.ToString();
                        break;
                    }
                }
            }

            return format;
        }

        public static string GetAspectFormat(TypeReference type, Emitter emitter, string name)
        {
            string format = null;
            var typeDef = Helpers.ToTypeDefinition(type, emitter);

            if (typeDef == null)
            {
                return null;
            }

            var formatField = typeDef.Fields.FirstOrDefault(f => f.Name == name);
            if (formatField != null)
            {
                format = formatField.Constant.ToString();
            }
            else
            {
                var baseType = typeDef.BaseType;

                if (baseType != null)
                {
                    return AspectHelpers.GetAspectFormat(Helpers.ToTypeDefinition(baseType, emitter), emitter, name);
                }
            }

            return format;
        }

        public static bool MatchTargetAttributes(TranslatorMulticastAttributes targetMembersAttributes, EntityDeclaration entity)
        {
            bool match = true;
            if (targetMembersAttributes != TranslatorMulticastAttributes.Default)
            {
                match = false;

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Instance) == TranslatorMulticastAttributes.Instance && !entity.HasModifier(Modifiers.Static))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Internal) == TranslatorMulticastAttributes.Internal && entity.HasModifier(Modifiers.Internal))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.NonVirtual) == TranslatorMulticastAttributes.NonVirtual && (!entity.HasModifier(Modifiers.Virtual) && !entity.HasModifier(Modifiers.Override)))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Private) == TranslatorMulticastAttributes.Private && (entity.HasModifier(Modifiers.Private) || (!entity.HasModifier(Modifiers.Public) && !entity.HasModifier(Modifiers.Protected) && !entity.HasModifier(Modifiers.Internal))))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Protected) == TranslatorMulticastAttributes.Protected && entity.HasModifier(Modifiers.Protected))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Public) == TranslatorMulticastAttributes.Public && entity.HasModifier(Modifiers.Public))
                {
                    return true;
                }
                
                if ((targetMembersAttributes & TranslatorMulticastAttributes.Static) == TranslatorMulticastAttributes.Static && entity.HasModifier(Modifiers.Static))
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.Virtual) == TranslatorMulticastAttributes.Virtual && (entity.HasModifier(Modifiers.Virtual) || entity.HasModifier(Modifiers.Override)))
                {
                    return true;
                }                
            }

            return match;
        }

        public static bool MatchTargetAttributes(TranslatorMulticastAttributes targetMembersAttributes, ParameterDeclaration entity)
        {
            bool match = true;
            if (targetMembersAttributes != TranslatorMulticastAttributes.Default)
            {
                match = false;

                if ((targetMembersAttributes & TranslatorMulticastAttributes.OutParameter) == TranslatorMulticastAttributes.Instance && entity.ParameterModifier == ParameterModifier.Out)
                {
                    return true;
                }

                if ((targetMembersAttributes & TranslatorMulticastAttributes.RefParameter) == TranslatorMulticastAttributes.RefParameter && entity.ParameterModifier == ParameterModifier.Ref)
                {
                    return true;
                }
            }

            return match;
        }

        public static bool MatchTarget(string targetMask, string target)
        {
            if (targetMask == null || targetMask.Length == 0)
            {
                return true;
            }

            string pattern;
            if (targetMask.StartsWith("regex:"))
            {
                pattern = targetMask.Substring(6);
            }
            else
            {
                pattern = "^" + Regex.Escape(targetMask).Replace("\\*", ".*").Replace("\\?", ".") + "$";
            }

            return Regex.IsMatch(target, pattern);
        }
    }
}