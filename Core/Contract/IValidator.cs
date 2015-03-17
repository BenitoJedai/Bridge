using System;
namespace Bridge.Contract
{
    public interface IValidator
    {
        bool CanIgnoreType(Mono.Cecil.TypeDefinition type);
        void CheckConstructors(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckFields(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckFileName(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckIdentifier(string name, ICSharpCode.NRefactory.CSharp.AstNode context);
        void CheckMethodArguments(Mono.Cecil.MethodDefinition method);
        void CheckMethods(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckModule(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckModuleDependenies(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckProperties(Mono.Cecil.TypeDefinition type, ITranslator translator);
        void CheckType(Mono.Cecil.TypeDefinition type, ITranslator translator);
        int EnumEmitMode(ICSharpCode.NRefactory.TypeSystem.Implementation.DefaultResolvedTypeDefinition type);
        ICSharpCode.NRefactory.TypeSystem.IAttribute GetAttribute(System.Collections.Generic.IEnumerable<ICSharpCode.NRefactory.TypeSystem.IAttribute> attributes, string name);
        Mono.Cecil.CustomAttribute GetAttribute(System.Collections.Generic.IEnumerable<Mono.Cecil.CustomAttribute> attributes, string name);
        string GetAttributeValue(System.Collections.Generic.IEnumerable<Mono.Cecil.CustomAttribute> attributes, string name);
        string GetCustomConstructor(Mono.Cecil.TypeDefinition type);
        string GetCustomTypeName(Mono.Cecil.TypeDefinition type);
        string GetInlineCode(Mono.Cecil.MethodDefinition method);
        string GetInlineCode(Mono.Cecil.PropertyDefinition property);
        string GetMethodSignatureKey(Mono.Cecil.MethodDefinition method);
        System.Collections.Generic.HashSet<string> GetParentTypes(System.Collections.Generic.IDictionary<string, Mono.Cecil.TypeDefinition> allTypes);
        bool HasAttribute(System.Collections.Generic.IEnumerable<ICSharpCode.NRefactory.TypeSystem.IAttribute> attributes, string name);
        bool HasAttribute(System.Collections.Generic.IEnumerable<Mono.Cecil.CustomAttribute> attributes, string name);
        bool IsDelegateOrLambda(ICSharpCode.NRefactory.Semantics.ResolveResult result);
        bool IsIgnoreType(Mono.Cecil.ICustomAttributeProvider type);
        bool IsIgnoreType(ICSharpCode.NRefactory.TypeSystem.ITypeDefinition typeDefinition);
        bool IsInlineMethod(Mono.Cecil.MethodDefinition method);
        bool IsNameEnum(ICSharpCode.NRefactory.TypeSystem.Implementation.DefaultResolvedTypeDefinition type);
        bool IsObjectLiteral(ICSharpCode.NRefactory.TypeSystem.ITypeDefinition type);
        bool IsObjectLiteral(Mono.Cecil.TypeDefinition type);
        bool IsStringNameEnum(ICSharpCode.NRefactory.TypeSystem.Implementation.DefaultResolvedTypeDefinition type);
        bool IsValueEnum(ICSharpCode.NRefactory.TypeSystem.Implementation.DefaultResolvedTypeDefinition type);
    }
}
