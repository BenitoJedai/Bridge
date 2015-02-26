using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System.Text;
using ICSharpCode.NRefactory.TypeSystem;
using System.Linq;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Semantics;
using System;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace Bridge.Plugin
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

        public static string GetScriptName(OperatorDeclaration op, bool separator)
        {
            return Helpers.GetScriptName(op.Name, op.Parameters.Count, separator);
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

        public static string GetTypeMapKey(ITypeInfo info) 
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

        public static bool IsSubclassOf(TypeDefinition thisTypeDefinition, TypeDefinition typeDefinition, IEmitter emitter)
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

        public static bool IsImplementationOf(TypeDefinition thisTypeDefinition, TypeDefinition interfaceTypeDefinition, IEmitter emitter)
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

        public static bool IsAssignableFrom(TypeDefinition thisTypeDefinition, TypeDefinition typeDefinition, IEmitter emitter)
        {
            return (thisTypeDefinition == typeDefinition 
                    || (typeDefinition.IsClass && !typeDefinition.IsValueType && Helpers.IsSubclassOf(typeDefinition, thisTypeDefinition, emitter))
                    || (typeDefinition.IsInterface && Helpers.IsImplementationOf(typeDefinition, thisTypeDefinition, emitter)));
        }

        public static TypeDefinition ToTypeDefinition(TypeReference reference, IEmitter emitter)
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

        public static PropertyDefinition GetPropertyDefinition(IEmitter emitter, EntityDeclaration propertyDeclaration, TypeDefinition type)
        {
            return type.Properties.FirstOrDefault(p => p.Name == propertyDeclaration.Name);
        }

        public static MethodDefinition GetMethodDefinition(IEmitter emitter, MethodDeclaration methodDeclaration, TypeDefinition type)
        {
            var parameters = methodDeclaration.Parameters.ToList();
            var methods = type.Methods.Where(m => m.Name == methodDeclaration.Name && m.Parameters.Count == parameters.Count && m.GenericParameters.Count == methodDeclaration.TypeParameters.Count).ToList();

            if (methods.Count <= 1)
            {
                return methods.Count == 1 ? methods[0] : null;
            }

            return Helpers.FindMethodDefinitionInGroup(emitter, parameters, null, methods, methodDeclaration.ReturnType);
        }

        public static MethodDefinition GetMethodDefinition(IEmitter emitter, OperatorDeclaration operatorDeclaration, TypeDefinition type)
        {
            var parameters = operatorDeclaration.Parameters.ToList();
            var ops = type.Methods.Where(m => m.Name == operatorDeclaration.Name && m.Parameters.Count == parameters.Count).ToList();

            if (ops.Count <= 1)
            {
                return ops.Count == 1 ? ops[0] : null;
            }

            return Helpers.FindMethodDefinitionInGroup(emitter, parameters, null, ops, operatorDeclaration.ReturnType);
        }

        public static MethodDefinition FindMethodDefinitionInGroup(IEmitter emitter, IEnumerable<ParameterDeclaration> parameters, IEnumerable<TypeParameterDeclaration> typeParameters, IEnumerable<MethodDefinition> group, AstType returnType)
        {
            var args = new List<ParameterDeclaration>(parameters);
            var typeParametersCount = typeParameters != null ? typeParameters.Count() : 0;
            foreach (var method in group)
            {
                if (args.Count == method.Parameters.Count && method.GenericParameters.Count == typeParametersCount)
                {
                    bool match = true;

                    if (returnType != null)
                    {
                        var resolveResult = emitter.Resolver.ResolveNode(returnType, emitter);
                        if (!Helpers.TypeIsMatch(emitter, resolveResult, returnType, method.ReturnType))
                        {
                            match = false;
                            continue;
                        }
                    }

                    for (int i = 0; i < method.Parameters.Count; i++)
                    {
                        var type = args[i].Type;
                        var resolveResult = emitter.Resolver.ResolveNode(type, emitter);
                        var typeRef = method.Parameters[i].ParameterType;

                        if (Helpers.TypeIsMatch(emitter, resolveResult, type, typeRef))
                        {
                            match = true;
                            break;
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

        public static bool TypeIsMatch(IEmitter emitter, ResolveResult resolveResult, AstType type, TypeReference typeRef)
        {
            var match = true;
            if (!(resolveResult is ErrorResolveResult) && resolveResult is TypeResolveResult)
            {
                if (((TypeResolveResult)resolveResult).Type.ReflectionName != typeRef.FullName.Replace("<", "[[").Replace(">", "]]").Replace(",", "],["))
                {
                    match = false;
                }
            }
            else
            {
                var isArray = typeRef.ToString().Contains("[]");

                var typeName = isArray ? typeRef.ToString().Replace("[]", "") : typeRef.ToString();
                var name = emitter.ResolveType(typeName, type);

                var typeDef = emitter.TypeDefinitions[name];

                if ((typeDef.FullName + (isArray ? "[]" : "")) != typeRef.FullName)
                {
                    match = false;
                }
            }

            return match;
        }

        public static string GetOverloadName(IEmitter emitter, MethodDefinition methodDef)
        {
            var name = emitter.GetMethodName(methodDef);
            var attr = emitter.GetAttribute(methodDef.CustomAttributes, "Bridge.CLR.Name");

            if (methodDef.IsConstructor)
            {
                name = "$ctor";
            }

            if (attr == null && (methodDef.Parameters.Count > 0 || methodDef.HasGenericParameters))
            {
                StringBuilder sb = new StringBuilder(name);

                if (methodDef.Parameters.Count > 0)
                {
                    var methodsDef = methodDef.DeclaringType.Methods.Where(m => m.Name == methodDef.Name && m.HasParameters).ToList();
                    sb.Append("$");
                    sb.Append(Helpers.GetMethodIndex(methodsDef, methodDef) + 1);
                }
                else if (methodDef.HasGenericParameters)
                {
                    sb.Append("$0");
                }
                
                if (methodDef.HasGenericParameters)
                {
                    sb.Append("$").Append(methodDef.GenericParameters.Count);
                }

                name = sb.ToString();
            }

            return name;
        }

        public static int GetMethodIndex(IEnumerable<MethodDefinition> methods, MethodDefinition method)
        {
            return methods.ToList().IndexOf(method);
            /*IEnumerable<string> names = methods.Select(m => m.ToString());
            string name = method.ToString();
            var list = names.ToList();
            list.Sort();

            return list.IndexOf(name);*/
        }

        public static string TranslateTypeReference(AstType astType, IEmitter emitter)
        {
            var composedType = astType as ComposedType;

            if (composedType != null && composedType.ArraySpecifiers != null && composedType.ArraySpecifiers.Count > 0)
            {
                return "Array";
            }

            var simpleType = astType as SimpleType;

            if (simpleType != null && simpleType.Identifier == "dynamic")
            {
                return "Object";
            }

            string type = emitter.ResolveType(Helpers.GetScriptName(astType, true), astType);

            if (String.IsNullOrEmpty(type))
            {
                throw (Exception)emitter.CreateException(astType, "Cannot resolve type " + astType.ToString());
            }

            var name = type;
            if (emitter.TypeDefinitions.ContainsKey(name))
            {
                name = emitter.ShortenTypeName(type);
            }


            if (simpleType != null && simpleType.TypeArguments.Count > 0 && !Helpers.IsIgnoreGeneric(simpleType, emitter))
            {
                StringBuilder sb = new StringBuilder(name);
                bool needComma = false;
                sb.Append("(");
                foreach (var typeArg in simpleType.TypeArguments)
                {
                    if (needComma)
                    {
                        sb.Append(",");
                    }

                    needComma = true;
                    sb.Append(Helpers.TranslateTypeReference(typeArg, emitter));
                }
                sb.Append(")");
                name = sb.ToString();
            }

            return name;
        }

        public static bool IsIgnoreGeneric(AstType astType, IEmitter emitter)
        {
            var fullname = emitter.ResolveType(Helpers.GetScriptName(astType, true), astType);
            if (emitter.TypeDefinitions.ContainsKey(fullname))
            {
                var typeDef = emitter.TypeDefinitions[fullname];
                if (emitter.Validator.HasAttribute(typeDef.CustomAttributes, "Bridge.CLR.IgnoreGenericAttribute"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsIgnoreCast(AstType astType, IEmitter emitter)
        {
            var fullname = emitter.ResolveType(Helpers.GetScriptName(astType, true), astType);
            if (emitter.TypeDefinitions.ContainsKey(fullname))
            {
                var typeDef = emitter.TypeDefinitions[fullname];
                if (emitter.Validator.HasAttribute(typeDef.CustomAttributes, "Bridge.CLR.IgnoreCastAttribute"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsIntegerType(IType type, IMemberResolver resolver)
        {
            type = type.IsKnownType(KnownTypeCode.NullableOfT) ? ((ParameterizedType)type).TypeArguments[0] : type;

            return type.Equals(resolver.Compilation.FindType(KnownTypeCode.Byte))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.SByte))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Char))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Int16))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.UInt16))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Int32))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.UInt32))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Int64))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.UInt64));
        }

        public static bool IsFloatType(IType type, IMemberResolver resolver)
        {
            type = type.IsKnownType(KnownTypeCode.NullableOfT) ? ((ParameterizedType)type).TypeArguments[0] : type;

            return type.Equals(resolver.Compilation.FindType(KnownTypeCode.Decimal))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Double))
                || type.Equals(resolver.Compilation.FindType(KnownTypeCode.Single));
        }

        public static void CheckValueTypeClone(ResolveResult resolveResult, Expression expression, IAbstractEmitterBlock block)
        {
            if (resolveResult == null || resolveResult.IsError)
            {
                return;
            }

            if (resolveResult is InvocationResolveResult ||
               block.Emitter.IsAssignment)
            {
                return;
            }

            if (resolveResult.Type.Kind == TypeKind.Struct)
            {
                var typeDef = block.Emitter.GetTypeDefinition(resolveResult.Type);
                if (block.Emitter.Validator.IsIgnoreType(typeDef))
                {
                    return;
                }

                var memberResult = resolveResult as MemberResolveResult;

                var field = memberResult != null ? memberResult.Member as DefaultResolvedField : null;

                if (field != null && field.IsReadOnly)
                {
                    block.Write(".$clone()");
                    return;
                }

                if (expression == null ||
                    expression.Parent is NamedExpression ||
                    expression.Parent is ObjectCreateExpression ||
                    expression.Parent is ArrayInitializerExpression || 
                    expression.Parent is ReturnStatement || 
                    expression.Parent is InvocationExpression || 
                    expression.Parent is AssignmentExpression ||
                    expression.Parent is VariableInitializer)
                {
                    block.Write(".$clone()");
                }
            }
        }
    }
}