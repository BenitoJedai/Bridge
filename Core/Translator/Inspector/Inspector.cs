using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Object.Net.Utilities;
using ICSharpCode.NRefactory;
using Bridge.Contract;

namespace Bridge.NET 
{
    public partial class Inspector : Visitor
    {
        public Inspector() 
        {
            this.Usings = new HashSet<string>();
            this.Types = new List<ITypeInfo>();
            this.AssemblyInfo = new AssemblyInfo();
        }
        
        protected virtual bool HasAttribute(EntityDeclaration type, string name)
        {

            foreach (var i in type.Attributes)
            {
                foreach (var j in i.Attributes)
                {
                    if (j.Type.ToString() == name)
                    {
                        return true;
                    }

                    var resolveResult = this.Resolver.ResolveNode(j, null);
                    if (resolveResult != null && resolveResult.Type != null && resolveResult.Type.FullName == (name + "Attribute"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected virtual bool IsObjectLiteral(EntityDeclaration declaration)
        {
            return this.HasAttribute(declaration, Translator.Bridge_ASSEMBLY + ".ObjectLiteral");
        }

        protected virtual bool HasIgnore(EntityDeclaration declaration) 
        {
            return this.HasAttribute(declaration, Translator.Bridge_ASSEMBLY + ".Ignore") || this.IsObjectLiteral(declaration);
        }

        protected virtual bool HasInline(EntityDeclaration declaration) 
        {
            return this.HasAttribute(declaration, Translator.Bridge_ASSEMBLY + ".Template");
        }

        protected virtual bool HasScript(EntityDeclaration declaration)
        {
            return this.HasAttribute(declaration, Translator.Bridge_ASSEMBLY + ".Script");
        }

        private Expression GetDefaultFieldInitializer(AstType type) 
        {
            return new PrimitiveExpression(this.GetDefaultFieldValue(type), "?");
        }

        private object GetDefaultFieldValue(AstType type)
        {
            if (type is PrimitiveType)
            {
                var primitiveType = (PrimitiveType)type;
                
                switch (primitiveType.KnownTypeCode)
                {
                    case KnownTypeCode.Int16:
                    case KnownTypeCode.Int32:
                    case KnownTypeCode.Int64:
                    case KnownTypeCode.UInt16:
                    case KnownTypeCode.UInt32:
                    case KnownTypeCode.UInt64:
                    case KnownTypeCode.Byte:
                    case KnownTypeCode.Double:
                    case KnownTypeCode.Decimal:
                    case KnownTypeCode.SByte:
                    case KnownTypeCode.Single:
                        return 0;
                    case KnownTypeCode.Boolean:
                        return false;
                }                
            }

            var resolveResult = this.Resolver.ResolveNode(type, null);

            if (!resolveResult.IsError && resolveResult.Type.Kind == TypeKind.Enum)
            {
                return 0;
            }

            if (!resolveResult.IsError && resolveResult.Type.Kind == TypeKind.Struct)
            {
                return type;
            }

            return null;
        }

        protected virtual bool IsValidStaticInitializer(Expression expr) 
        {
            if (expr.IsNull || expr is PrimitiveExpression)
            {
                return true;
            }

            var arrayExpr = expr as ArrayCreateExpression;

            if (arrayExpr == null)
            {
                return false;
            }

            try 
            {
                new ArrayInitializerVisitor().VisitArrayCreateExpression(arrayExpr);

                return true;
            } 
            catch(Exception) 
            {
                return false;
            }
        }

        protected virtual void FixMethodParameters(AstNodeCollection<ParameterDeclaration> parameters, BlockStatement body)
        {
            /*if (parameters.Count == 0)
            {
                return;
            }

            foreach (var p in parameters)
            {
                string newName = Emitter.FIX_ARGUMENT_NAME + p.Name;
                string oldName = p.Name;


                VariableDeclarationStatement varState = new VariableDeclarationStatement(p.Type.Clone(), oldName, new CastExpression(p.Type.Clone(), new IdentifierExpression(newName)));

                p.Name = newName;
                
                body.InsertChildBefore(body.FirstChild, varState, new Role<VariableDeclarationStatement>("Statement"));
                
            }*/
        }
    }
}
