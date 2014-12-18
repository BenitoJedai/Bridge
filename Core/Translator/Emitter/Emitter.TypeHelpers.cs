using ICSharpCode.NRefactory.CSharp;
using Mono.Cecil;
using System;
using System.Collections;
using Ext.Net.Utilities;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem.Implementation;

namespace Bridge.NET
{
    public partial class Emitter
    {
        public virtual int CompareTypeInfos(TypeInfo x, TypeInfo y)
        {
            if (x == y)
            {
                return 0;
            }

            if (x.FullName == Emitter.ROOT)
            {
                return -1;
            }

            if (y.FullName == Emitter.ROOT)
            {
                return 1;
            }

            var xTypeDefinition = this.TypeDefinitions[Helpers.GetTypeMapKey(x)];
            var yTypeDefinition = this.TypeDefinitions[Helpers.GetTypeMapKey(y)];

            if (Helpers.IsSubclassOf(xTypeDefinition, yTypeDefinition))
            {
                return 1;
            }

            if (Helpers.IsSubclassOf(yTypeDefinition, xTypeDefinition))
            {
                return -1;
            }

            if (xTypeDefinition.IsInterface && Helpers.IsImplementationOf(yTypeDefinition, xTypeDefinition))
            {
                return -1;
            }

            if (yTypeDefinition.IsInterface && Helpers.IsImplementationOf(xTypeDefinition, yTypeDefinition))
            {
                return 1;
            }

            return Comparer.Default.Compare(x.FullName, y.FullName);
        }

        public virtual TypeDefinition GetTypeDefinition()
        {
            return this.TypeDefinitions[Helpers.GetTypeMapKey(this.TypeInfo)];
        }

        public virtual TypeDefinition GetTypeDefinition(AstType reference)
        {
            string name = Helpers.GetScriptName(reference, true);
            name = this.ResolveType(name, reference);

            if (name.IsEmpty() || !this.TypeDefinitions.ContainsKey(name))
            {
                var resolveResult = this.Resolver.ResolveNode(reference, this) as TypeResolveResult;

                if (resolveResult != null)
                {
                    var type = resolveResult.Type as DefaultTypeParameter;

                    if (type != null && type.EffectiveBaseClass != null)
                    {
                        name = Helpers.GetScriptFullName(type.EffectiveBaseClass);
                        name = this.ResolveType(name, reference);

                        if (this.TypeDefinitions.ContainsKey(name))
                        {
                            return this.TypeDefinitions[name];
                        }
                    }
                }

                throw new Exception("Type cannot be resolved: " + reference.ToString());
            }

            return this.TypeDefinitions[name];
        }

        public virtual TypeDefinition GetBaseTypeDefinition()
        {
            return this.GetBaseTypeDefinition(this.GetTypeDefinition());
        }

        public virtual TypeDefinition GetBaseTypeDefinition(TypeDefinition type)
        {
            var reference = this.TypeDefinitions[Helpers.GetTypeMapKey(type)].BaseType;

            if (reference == null)
            {
                return null;
            }

            return this.TypeDefinitions[Helpers.GetTypeMapKey(reference)];
        }

        public virtual TypeDefinition GetBaseMethodOwnerTypeDefinition(string methodName, int genericParamCount)
        {
            TypeDefinition type = this.GetBaseTypeDefinition();

            while (true)
            {
                var methods = type.Methods.Where(m => m.Name == methodName);

                foreach (var method in methods)
                {
                    if (genericParamCount < 1 || method.GenericParameters.Count == genericParamCount)
                    {
                        return type;
                    }
                }

                type = this.GetBaseTypeDefinition(type);
            }
        }

        public string ShortenTypeName(string name)
        {
            var type = this.TypeDefinitions[name];
            string module = null;

            if (this.TypeInfo.FullName != name && this.TypeInfoDefinitions.ContainsKey(name))
            {
                var typeInfo = this.TypeInfoDefinitions[name];
                module = typeInfo.Module;
                if (typeInfo.Module != null && this.TypeInfo.Module != typeInfo.Module && !this.CurrentDependencies.Any(d => d.DependencyName == typeInfo.Module))
                {
                    this.CurrentDependencies.Add(new ModuleDependency { DependencyName = typeInfo.Module });
                }
            }

            var customName = this.Validator.GetCustomTypeName(type);

            name = !String.IsNullOrEmpty(customName) ? customName : name;

            if (module.IsNotEmpty() && this.TypeInfo.GenericFullName != name && this.TypeInfo.Module != module)
            {
                name = module + "." + name;
            }

            return name;
        }

        public virtual string GetTypeHierarchy()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            var list = new List<string>();
            var baseType = this.GetBaseTypeDefinition();

            if (baseType != null)
            {
                list.Add(Helpers.GetScriptFullName(baseType));
            }

            foreach (Mono.Cecil.TypeReference i in this.GetTypeDefinition().Interfaces)
            {
                if (i.FullName == "System.Collections.IEnumerable")
                {
                    continue;
                }

                if (i.FullName == "System.Collections.IEnumerator")
                {
                    continue;
                }

                list.Add(Helpers.GetScriptFullName(i));
            }

            if (list.Count == 1 && (baseType.FullName == "System.Object" || baseType.FullName == "System.ValueType"))
            {
                return "";
            }

            bool needComma = false;

            foreach (var item in list)
            {
                if (needComma)
                {
                    sb.Append(",");
                }

                needComma = true;
                sb.Append(this.ShortenTypeName(item));
            }

            sb.Append("]");

            return sb.ToString();
        }

        public virtual string ResolveNamespaceOrType(string id, bool allowNamespaces)
        {
            id = id.LeftOf('<').Replace("`", "$");

            if (allowNamespaces && this.Namespaces.Contains(id))
            {
                return id;
            }

            if (this.TypeDefinitions.ContainsKey(id))
            {
                return id;
            }

            string guess;
            string namespacePrefix = this.TypeInfo.Namespace;

            if (!String.IsNullOrEmpty(namespacePrefix))
            {
                while (true)
                {
                    guess = namespacePrefix + "." + id;

                    if (allowNamespaces && this.Namespaces.Contains(guess))
                    {
                        return guess;
                    }

                    if (this.TypeDefinitions.ContainsKey(guess))
                    {
                        return guess;
                    }

                    int index = namespacePrefix.LastIndexOf(".");

                    if (index < 0)
                    {
                        break;
                    }

                    namespacePrefix = namespacePrefix.Substring(0, index);
                }
            }

            foreach (string usingPrefix in this.TypeInfo.Usings)
            {
                guess = usingPrefix + "." + id;

                if (this.TypeDefinitions.ContainsKey(guess))
                {
                    return guess;
                }
            }

            return null;
        }

        public virtual string ResolveType(string id)
        {
            return this.ResolveNamespaceOrType(id, false);
        }

        public virtual string ResolveType(string id, AstNode type)
        {
            var name = this.ResolveNamespaceOrType(id, false);

            if (name.IsEmpty())
            {
                var resolveResult = this.Resolver.ResolveNode(type, this) as TypeResolveResult;

                if (resolveResult != null)
                {
                    name = resolveResult.Type.FullName;
                }
            }

            return name;
        }        
    }
}
