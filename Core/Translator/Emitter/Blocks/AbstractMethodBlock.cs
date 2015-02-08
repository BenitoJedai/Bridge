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
                this.Write(p.Name.Replace(Bridge.NET.Emitter.FIX_ARGUMENT_NAME, ""));
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
                this.Write(p.Name.Replace(Bridge.NET.Emitter.FIX_ARGUMENT_NAME, ""));
            }

            this.WriteCloseParentheses();
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
