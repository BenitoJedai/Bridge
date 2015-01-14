using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System.Text;
using ICSharpCode.NRefactory.TypeSystem;
using System.Linq;

namespace Bridge.NET
{
    public static class Helpers 
    {
        public static string GetOperatorMethodName(BinaryOperatorType op)
        {
            string name = "";

            switch (op)
            {
                case BinaryOperatorType.Add:
                    name = "Addition";
                    break;
                case BinaryOperatorType.BitwiseAnd:
                    name = "BitwiseAnd";
                    break;
                case BinaryOperatorType.BitwiseOr:
                    name = "BitwiseOr";
                    break;
                case BinaryOperatorType.ConditionalAnd:
                    name = "LogicalAnd";
                    break;
                case BinaryOperatorType.NullCoalescing:
                case BinaryOperatorType.ConditionalOr:
                    name = "LogicalOr";
                    break;
                case BinaryOperatorType.Divide:
                    name = "Division";
                    break;
                case BinaryOperatorType.Equality:
                    name = "Equality";
                    break;
                case BinaryOperatorType.ExclusiveOr:
                    name = "ExclusiveOr";
                    break;
                case BinaryOperatorType.GreaterThan:
                    name = "GreaterThan";
                    break;
                case BinaryOperatorType.GreaterThanOrEqual:
                    name = "GreaterThanOrEqual";
                    break;
                case BinaryOperatorType.InEquality:
                    name = "Inequality";
                    break;
                case BinaryOperatorType.LessThan:
                    name = "LessThan";
                    break;
                case BinaryOperatorType.LessThanOrEqual:
                    name = "LessThanOrEqual";
                    break;
                case BinaryOperatorType.Modulus:
                    name = "Modulus";
                    break;
                case BinaryOperatorType.Multiply:
                    name = "Multiply";
                    break;
                case BinaryOperatorType.ShiftLeft:
                    name = "LeftShift";
                    break;
                case BinaryOperatorType.ShiftRight:
                    name = "RightShift";
                    break;
                case BinaryOperatorType.Subtract:
                    name = "Subtraction";
                    break;
                default:
                    name = "";
                    break;
            }

            return "op_" + name;
        }
        
        
        public static void AcceptChildren(this AstNode node, IAstVisitor visitor)
        {
            foreach (AstNode child in node.Children)
            {
                child.AcceptVisitor(visitor);
            }
        }

        public static string GetScriptName(MethodDeclaration method, bool separator) 
        {            
            return Helpers.GetScriptName(method.Name, method.Parameters.Count, separator);
        }

        public static string GetScriptName(MemberReferenceExpression member, bool separator) 
        {
            return Helpers.GetScriptName(member.MemberName, member.TypeArguments.Count, separator);
        }

        public static string GetScriptName(MethodDefinition method, bool separator) 
        {
            return Helpers.GetScriptName(method.Name, method.GenericParameters.Count, separator);
        }

        public static string GetScriptName(TypeDeclaration type, bool separator) 
        {
            return Helpers.GetScriptName(type.Name, type.TypeParameters.Count, separator);
        }

        public static string GetScriptName(AstType type, bool separator) 
        {
            string result = null;
            SimpleType simpleType = type as SimpleType;

            if (simpleType != null) 
            {
                result = Helpers.GetScriptName(simpleType.Identifier, simpleType.TypeArguments.Count, separator);
            }
            else
            {
                PrimitiveType primType = type as PrimitiveType;

                if (primType != null)
                {
                    result = Helpers.GetScriptName(primType.KnownTypeCode.ToString(), 0, separator);
                }
                else
                {
                    result = Helpers.GetScriptName(type.ToString(), 0, separator);
                }
            }
            
            var composedType = type as ComposedType;

            if (composedType != null)
            {
                result = Helpers.GetScriptName(composedType.BaseType, separator) + "." + result;
            }
            
            return result;
        }

        public static string GetScriptFullName(IType type)
        {
            var name = Helpers.ReplaceSpecialChars(type.FullName);

            if (type.TypeParameterCount > 0)
            {
                name += "$" + type.TypeParameterCount;
            }

            return name;
        }

        public static string GetScriptFullName(TypeDefinition type) 
        {
            var name = Helpers.ReplaceSpecialChars(type.FullName);

            /*if (type.GenericParameters.Count > 0)
            {
                name += "$" + type.GenericParameters.Count;
            }*/

            return name;
        }

        public static string GetScriptFullName(TypeReference type) 
        {
            StringBuilder builder = new StringBuilder(type.Namespace);

            if (builder.Length > 0)
            {
                builder.Append('.');
            }

            var name = type.Name;
            builder.Append(Helpers.ReplaceSpecialChars(name));

            if (type.GenericParameters.Count > 0)
            {
                builder.Append("$" + type.GenericParameters.Count);
            }
            
            return builder.ToString();
        }

        public static string GetTypeMapKey(TypeDefinition type) 
        {
            return Helpers.GetScriptFullName(type);
        }

        public static string GetTypeMapKey(TypeInfo info) 
        {
            return !string.IsNullOrEmpty(info.GenericName) ? info.GenericFullName : info.FullName;
        }

        public static string GetTypeMapKey(TypeReference type) 
        {
            return Helpers.GetScriptFullName(type);
        }

        public static string GetScriptName(string name, int paramCount, bool separator) 
        {
            return Helpers.GetPostfixedName(name, paramCount, separator ? "$" : null);
        }

        public static string ReplaceSpecialChars(string name) 
        {
            return name.Replace('`', '$').Replace('/', '.');
        }

        private static string GetPostfixedName(string name, int paramCount, string separator) 
        {
            if (paramCount < 1)
            {
                return name;
            }

            if (string.IsNullOrEmpty(separator))
            {
                return name;
            }
            
            return name + separator + paramCount;
        }

        public static bool IsSubclassOf(TypeDefinition thisTypeDefinition, TypeDefinition typeDefinition, Emitter emitter)
        {
            if (thisTypeDefinition.BaseType != null)
            {
                TypeDefinition baseTypeDefinition = null;

                try
                {
                    baseTypeDefinition = Helpers.ToTypeDefinition(thisTypeDefinition.BaseType, emitter);
                }
                catch { }

                if (baseTypeDefinition != null)
                {
                    return (baseTypeDefinition == typeDefinition || Helpers.IsSubclassOf(baseTypeDefinition, typeDefinition, emitter));
                }
            }
            return false;
        }

        public static bool IsImplementationOf(TypeDefinition thisTypeDefinition, TypeDefinition interfaceTypeDefinition, Emitter emitter)
        {
            foreach (TypeReference interfaceReference in thisTypeDefinition.Interfaces)
            {
                if (interfaceReference == interfaceTypeDefinition)
                {
                    return true;
                }

                TypeDefinition interfaceDefinition = null;
                
                try 
                {
                    interfaceDefinition = Helpers.ToTypeDefinition(interfaceReference, emitter);
                }
                catch { }

                if (interfaceDefinition != null && Helpers.IsImplementationOf(interfaceDefinition, interfaceTypeDefinition, emitter))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAssignableFrom(TypeDefinition thisTypeDefinition, TypeDefinition typeDefinition, Emitter emitter)
        {
            return (thisTypeDefinition == typeDefinition 
                    || (typeDefinition.IsClass && !typeDefinition.IsValueType && Helpers.IsSubclassOf(typeDefinition, thisTypeDefinition, emitter))
                    || (typeDefinition.IsInterface && Helpers.IsImplementationOf(typeDefinition, thisTypeDefinition, emitter)));
        }

        public static TypeDefinition ToTypeDefinition(TypeReference reference, Emitter emitter)
        {
            try
            {
                if (emitter.TypeDefinitions.ContainsKey(reference.FullName))
                {
                    return emitter.TypeDefinitions[reference.FullName];
                }

                return reference.Resolve();
            }
            catch
            {
            }

            return null;
        }

        public static MethodDefinition GetMethodDefinition(Emitter emitter, MethodDeclaration methodDeclaration, TypeDefinition type)
        {
            var parameters = methodDeclaration.Parameters.ToList();
            var methods = type.Methods.Where(m => m.Name == methodDeclaration.Name && m.Parameters.Count == parameters.Count && m.GenericParameters.Count == methodDeclaration.TypeParameters.Count).ToList();

            if (methods.Count <= 1)
            {
                return methods.Count == 1 ? methods[0] : null;
            }

            return AbstractMethodBlock.FindMethodDefinitionInGroup(emitter, parameters, null, methods);
        }
    }
}