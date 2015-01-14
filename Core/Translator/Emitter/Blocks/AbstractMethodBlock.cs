using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.NET
{
    public abstract class AbstractMethodBlock : AbstractEmitterBlock
    {        
        public static string GetOverloadName(Emitter emitter, MethodDefinition methodDef)
        {
            var name = emitter.GetMethodName(methodDef);
            var attr = emitter.GetAttribute(methodDef.CustomAttributes, Translator.CLR_ASSEMBLY + ".Name");

            if (methodDef.IsConstructor)
            {
                name = "$init";
            }

            if (attr == null && methodDef.Parameters.Count > 0)
            {
                StringBuilder sb = new StringBuilder(name);

                foreach (var p in methodDef.Parameters)
                {
                    sb.Append("$").Append(p.ParameterType.Name.Replace("[]", "$Array").Replace("`", "$"));
                }

                if (methodDef.HasGenericParameters)
                {
                    sb.Append("$").Append(methodDef.GenericParameters.Count);
                }

                name = sb.ToString();
            }

            return name;
        }

        protected virtual void EmitMethodParameters(IEnumerable<ParameterDeclaration> declarations, AstNode context)
        {
            this.WriteOpenParentheses();
            bool needComma = false;

            foreach (var p in declarations)
            {
                this.Emitter.Validator.CheckIdentifier(p.Name, context);

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(p.Name.Replace(Emitter.FIX_ARGUMENT_NAME, ""));
            }

            this.WriteCloseParentheses();
        }

        protected virtual void EmitTypeParameters(IEnumerable<TypeParameterDeclaration> declarations, AstNode context)
        {
            this.WriteOpenParentheses();
            bool needComma = false;

            foreach (var p in declarations)
            {
                this.Emitter.Validator.CheckIdentifier(p.Name, context);

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                this.Write(p.Name.Replace(Emitter.FIX_ARGUMENT_NAME, ""));
            }

            this.WriteCloseParentheses();
        }

        public static MethodDefinition FindMethodDefinitionInGroup(Emitter emitter, IEnumerable<ParameterDeclaration> parameters, IEnumerable<TypeParameterDeclaration> typeParameters, IEnumerable<MethodDefinition> group)
        {
            var args = new List<ParameterDeclaration>(parameters);
            var typeParametersCount = typeParameters != null ? typeParameters.Count() : 0;
            foreach (var method in group)
            {
                if (args.Count == method.Parameters.Count && method.GenericParameters.Count == typeParametersCount)
                {
                    bool match = true;
                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        var type = args[i].Type;
                        var resolveResult = emitter.Resolver.ResolveNode(type, emitter);

                        if (!(resolveResult is ErrorResolveResult) && resolveResult is TypeResolveResult)
                        {
                            if (((TypeResolveResult)resolveResult).Type.ReflectionName != method.Parameters[i].ParameterType.FullName.Replace("<", "[[").Replace(">", "]]").Replace(",", "],["))
                            {
                                match = false;
                                break;
                            }
                        }
                        else
                        {
                            var isArray = type.ToString().Contains("[]");

                            var typeName = isArray ? type.ToString().Replace("[]", "") : type.ToString();
                            var name = emitter.ResolveType(typeName, type);

                            var typeDef = emitter.TypeDefinitions[name];

                            if ((typeDef.FullName + (isArray ? "[]" : "")) != method.Parameters[i].ParameterType.FullName)
                            {
                                match = false;
                                break;
                            }
                        }
                    }

                    if (match)
                    {
                        return method;
                    }
                }
            }

            return null;
        }

        protected virtual string GetMethodsDetector(Dictionary<int, StringBuilder> detectorBuilders)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in detectorBuilders.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("else ");
                }

                sb.Append("if (arguments.length == ").Append(key).AppendLine(") {");
                sb.AppendLine(detectorBuilders[key].ToString());
                sb.AppendLine("}");
            }

            return sb.ToString();
        }

        protected virtual void EmitMethodDetector(Dictionary<int, StringBuilder> detectorBuilders, MethodDefinition method, string name)
        {
            var argsCount = method.Parameters.Count;
            if (!detectorBuilders.ContainsKey(argsCount))
            {
                detectorBuilders.Add(method.Parameters.Count, new StringBuilder());
            }

            StringBuilder sb = detectorBuilders[argsCount];

            if (method.HasParameters)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                    sb.Append("    else if (");
                }
                else
                {
                    sb.Append("    if (");
                }


                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    if (i != 0)
                    {
                        sb.Append(" && ");
                    }

                    sb.Append("Bridge.is(arguments[");
                    sb.Append(i);
                    sb.Append("], ");

                    var paramType = method.Parameters[i].ParameterType;
                    sb.Append(paramType.IsArray ? "Array" : this.Emitter.ShortenTypeName(this.Emitter.ResolveType(paramType.FullName)));
                    sb.Append(")");
                }

                sb.AppendLine(") {");
            }

            sb.Append(method.HasParameters ? "        " : "    ");
            if (method.ReturnType.MetadataType == MetadataType.Void)
            {
                sb.Append("this.");
            }
            else
            {
                sb.Append("return this.");
            }
            sb.Append(name).Append(".apply(this, arguments);");

            if (method.HasParameters)
            {
                sb.AppendLine();
                sb.Append("    }");
            }
        }
    }
}
