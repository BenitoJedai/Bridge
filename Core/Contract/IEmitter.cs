using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Contract
{
    public interface IEmitter : ILog, IAstVisitor
    {
        IAssemblyInfo AssemblyInfo { get; set; }
        ICSharpCode.NRefactory.CSharp.AssignmentOperatorType AssignmentType { get; set; }
        IAsyncBlock AsyncBlock { get; set; }
        bool AsyncExpressionHandling { get; set; }
        ICSharpCode.NRefactory.CSharp.SwitchStatement AsyncSwitch { get; set; }
        System.Collections.Generic.List<string> AsyncVariables { get; set; }
        bool ChangeCase { get; set; }
        bool Comma { get; set; }
        int CompareTypeInfos(ITypeInfo x, ITypeInfo y);
        System.Collections.Generic.List<IPluginDependency> CurrentDependencies { get; set; }
        System.Collections.Generic.Dictionary<string, string> Emit();
        bool EnableSemicolon { get; set; }
        ICSharpCode.NRefactory.TypeSystem.IAttribute GetAttribute(System.Collections.Generic.IEnumerable<ICSharpCode.NRefactory.TypeSystem.IAttribute> attributes, string name);
        Mono.Cecil.CustomAttribute GetAttribute(System.Collections.Generic.IEnumerable<Mono.Cecil.CustomAttribute> attributes, string name);
        Mono.Cecil.TypeDefinition GetBaseMethodOwnerTypeDefinition(string methodName, int genericParamCount);
        Mono.Cecil.TypeDefinition GetBaseTypeDefinition();
        Mono.Cecil.TypeDefinition GetBaseTypeDefinition(Mono.Cecil.TypeDefinition type);
        string GetEntityName(ICSharpCode.NRefactory.CSharp.EntityDeclaration entity, bool cancelChangeCase = false);
        string GetEntityName(ICSharpCode.NRefactory.TypeSystem.IEntity member, bool cancelChangeCase = false);
        string GetInline(ICSharpCode.NRefactory.CSharp.EntityDeclaration method);
        string GetInline(ICSharpCode.NRefactory.TypeSystem.IEntity entity);
        string GetInline(Mono.Cecil.ICustomAttributeProvider provider);
        Tuple<bool, bool, string> GetInlineCode(ICSharpCode.NRefactory.CSharp.InvocationExpression node);
        string GetDefinitionName(IMemberDefinition member, bool changeCase = true);        
        System.Collections.Generic.IEnumerable<string> GetScript(ICSharpCode.NRefactory.CSharp.EntityDeclaration method);
        int GetSerializationPriority(Mono.Cecil.TypeDefinition type);
        Mono.Cecil.TypeDefinition GetTypeDefinition();
        Mono.Cecil.TypeDefinition GetTypeDefinition(ICSharpCode.NRefactory.CSharp.AstType reference, bool safe = false);
        Mono.Cecil.TypeDefinition GetTypeDefinition(IType type);
        string GetTypeHierarchy();
        ICSharpCode.NRefactory.CSharp.AstNode IgnoreBlock { get; set; }        
        bool IsAssignment { get; set; }
        bool IsAsync { get; set; }
        bool IsInlineConst(ICSharpCode.NRefactory.TypeSystem.IMember member);
        bool IsMemberConst(ICSharpCode.NRefactory.TypeSystem.IMember member);
        bool IsNativeMember(string fullName);
        bool IsNewLine { get; set; }
        int IteratorCount { get; set; }
        System.Collections.Generic.List<IJumpInfo> JumpStatements { get; set; }
        IWriterInfo LastSavedWriter { get; set; }
        int Level { get; set; }
        System.Collections.Generic.Dictionary<string, ICSharpCode.NRefactory.CSharp.AstType> Locals { get; set; }
        System.Collections.Generic.Dictionary<string, string> LocalsMap { get; set; }
        System.Collections.Generic.Stack<System.Collections.Generic.Dictionary<string, ICSharpCode.NRefactory.CSharp.AstType>> LocalsStack { get; set; }
        Action<string, string> Log { get; set; }
        void LogError(string message);
        void LogMessage(string level, string message);
        void LogMessage(string message);
        void LogWarning(string message);
        System.Collections.Generic.IEnumerable<Mono.Cecil.MethodDefinition> MethodsGroup { get; set; }
        System.Collections.Generic.Dictionary<int, System.Text.StringBuilder> MethodsGroupBuilder { get; set; }
        ICSharpCode.NRefactory.CSharp.AstNode NoBraceBlock { get; set; }
        System.Text.StringBuilder Output { get; set; }
        IEmitterOutputs Outputs { get; set; }
        System.Collections.Generic.IEnumerable<Mono.Cecil.AssemblyDefinition> References { get; set; }
        bool ReplaceAwaiterByVar { get; set; }
        string ResolveNamespaceOrType(string id, bool allowNamespaces);
        IMemberResolver Resolver { get; set; }
        string ResolveType(string id);
        string ResolveType(string id, ICSharpCode.NRefactory.CSharp.AstNode type);
        string ShortenTypeName(string name);
        bool SkipSemiColon { get; set; }
        System.Collections.Generic.IList<string> SourceFiles { get; set; }
        int ThisRefCounter { get; set; }
        string ToJavaScript(object value);
        System.Collections.Generic.IDictionary<string, Mono.Cecil.TypeDefinition> TypeDefinitions { get; }
        ITypeInfo TypeInfo { get; set; }
        System.Collections.Generic.Dictionary<string, ITypeInfo> TypeInfoDefinitions { get; set; }
        System.Collections.Generic.List<ITypeInfo> Types { get; set; }
        IValidator Validator { get; }
        System.Collections.Generic.Stack<Tuple<string, System.Text.StringBuilder, bool>> Writers { get; set; }
        IVisitorException CreateException(AstNode node);
        IVisitorException CreateException(AstNode node, string message);
        IPlugins Plugins { get; set; }
        Dictionary<string, OverloadsCollection> OverloadsCache { get; }
        string GetFieldName(FieldDeclaration field);
        string GetEventName(EventDeclaration evt);
    }
}
