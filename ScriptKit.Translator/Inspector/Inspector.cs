using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using Ext.Net.Utilities;

namespace ScriptKit.NET 
{
    public partial class Inspector : Visitor 
    {
        public Inspector() 
        {
            this.Usings = new HashSet<string>();
            this.Types = new List<TypeInfo>();
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
                }
            }

            return false;
        }

        protected virtual bool HasIgnore(EntityDeclaration declaration) 
        {

            return this.HasAttribute(declaration, "ScriptKit.Core.Ignore");
        }

        protected virtual bool HasInline(EntityDeclaration declaration) 
        {
            return this.HasAttribute(declaration, "ScriptKit.Core.Inline");
        }

        protected virtual bool HasScript(EntityDeclaration declaration)
        {
            return this.HasAttribute(declaration, "ScriptKit.Core.Script");
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
    }
}
