using System.Text;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;

namespace ScriptKit.NET
{
    public static class Helpers 
    {
        public static void AcceptChildren(this AstNode node, IAstVisitor visitor)
        {
            foreach (AstNode child in node.Children)
            {
                child.AcceptVisitor(visitor);
            }
        }

        public static string GetScriptName(MethodDeclaration method) 
        {
            return Helpers.GetScriptName(method.Name, method.Parameters.Count);
        }

        public static string GetScriptName(MemberReferenceExpression member) 
        {
            return Helpers.GetScriptName(member.MemberName, member.TypeArguments.Count);
        }

        public static string GetScriptName(MethodDefinition method) 
        {
            return Helpers.GetScriptName(method.Name, method.GenericParameters.Count);
        }

        public static string GetScriptName(TypeDeclaration type) 
        {
            return Helpers.GetScriptName(type.Name, type.TypeParameters.Count);
        }

        public static string GetScriptName(AstType type) 
        {
            string result = null;
            SimpleType simpleType = type as SimpleType;

            if (simpleType != null) 
            {
                result = Helpers.GetScriptName(simpleType.Identifier, simpleType.TypeArguments.Count);
            }
            else
            {
                PrimitiveType primType = type as PrimitiveType;

                if (primType != null)
                {
                    result = Helpers.GetScriptName(primType.KnownTypeCode.ToString(), 0);
                }
                else
                {
                    result = Helpers.GetScriptName(type.ToString(), 0);
                }
            }
            
            var composedType = type as ComposedType;

            if (composedType != null)
            {
                result = Helpers.GetScriptName(composedType.BaseType) + "." + result;
            }
            
            return result;
        }

        public static string GetScriptFullName(TypeDefinition type) 
        {
            return Helpers.ReplaceSpecialChars(type.FullName);
        }

        public static string GetScriptFullName(TypeReference type) 
        {
            StringBuilder builder = new StringBuilder(type.Namespace);

            if (builder.Length > 0)
            {
                builder.Append('.');
            }
            
            builder.Append(Helpers.ReplaceSpecialChars(type.Name));
            
            return builder.ToString();
        }

        public static string GetTypeMapKey(TypeDefinition type) 
        {
            return Helpers.GetScriptFullName(type);
        }

        public static string GetTypeMapKey(TypeInfo info) 
        {
            return info.FullName;
        }

        public static string GetTypeMapKey(TypeReference type) 
        {
            return Helpers.GetScriptFullName(type);
        }

        private static string GetScriptName(string name, int paramCount) 
        {
            return Helpers.GetPostfixedName(name, paramCount, "$");
        }

        private static string ReplaceSpecialChars(string name) 
        {
            return name.Replace('`', '$').Replace('/', '.');
        }

        private static string GetPostfixedName(string name, int paramCount, string separator) 
        {
            if (paramCount < 1)
            {
                return name;
            }

            return name;
            //return name + separator + paramCount;
        }

    }
}
