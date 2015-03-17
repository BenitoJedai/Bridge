using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bridge.NET
{
    public class ObjectCreateBlock : ConversionBlock
    {
        public ObjectCreateBlock(IEmitter emitter, ObjectCreateExpression objectCreateExpression)
        {
            this.Emitter = emitter;
            this.ObjectCreateExpression = objectCreateExpression;
        }

        public ObjectCreateExpression ObjectCreateExpression 
        { 
            get; 
            set; 
        }

        protected override Expression GetExpression()
        {
            return this.ObjectCreateExpression;
        }

        protected override void EmitConversionExpression()
        {
            this.VisitObjectCreateExpression();
        }

        protected void VisitObjectCreateExpression()
        {
            ObjectCreateExpression objectCreateExpression = this.ObjectCreateExpression;

            var type = this.Emitter.GetTypeDefinition(objectCreateExpression.Type);

            if (type.BaseType != null && type.BaseType.FullName == "System.MulticastDelegate")
            {
                objectCreateExpression.Arguments.First().AcceptVisitor(this.Emitter);
                return;
            }

            var argsInfo = new ArgumentsInfo(this.Emitter, objectCreateExpression);
            var argsExpressions = argsInfo.ArgumentsExpressions;
            var argsNames = argsInfo.ArgumentsNames;
            var paramsArg = argsInfo.ParamsExpression;
            var argsCount = argsExpressions.Count();

            var invocationResolveResult = this.Emitter.Resolver.ResolveNode(objectCreateExpression, this.Emitter) as InvocationResolveResult;
            string inlineCode = null;
            if (invocationResolveResult != null)
            {
                inlineCode = this.Emitter.GetInline(invocationResolveResult.Member);
            }

            var customCtor = this.Emitter.Validator.GetCustomConstructor(type) ?? "";
            var hasInitializer = !objectCreateExpression.Initializer.IsNull && objectCreateExpression.Initializer.Elements.Count > 0;


            bool isCollectionInitializer = false;
            AstNodeCollection<Expression> elements = null;
            if (hasInitializer)
            {
                elements = objectCreateExpression.Initializer.Elements;
                isCollectionInitializer = elements.Count > 0 && elements.First() is ArrayInitializerExpression;
            }

            if (inlineCode == null && Regex.Match(customCtor, @"\s*\{\s*\}\s*").Success)
            {
                this.WriteOpenBrace();
                this.WriteSpace();

                if (hasInitializer)
                {
                    this.WriteObjectInitializer(objectCreateExpression.Initializer.Elements, this.Emitter.ChangeCase);

                    this.WriteSpace();
                    this.WriteCloseBrace();
                }
                else
                {
                    this.WriteCloseBrace();
                }
            }
            else
            {
                if (hasInitializer)
                {
                    this.Write(Bridge.NET.Emitter.ROOT);
                    this.WriteDot();
                    this.Write(Bridge.NET.Emitter.MERGE_OBJECT);
                    this.WriteOpenParentheses();
                }

                if (inlineCode != null)
                {
                    new InlineArgumentsBlock(this.Emitter, argsInfo, inlineCode).Emit();
                }
                else
                {

                    if (String.IsNullOrEmpty(customCtor))
                    {
                        this.WriteNew();
                        objectCreateExpression.Type.AcceptVisitor(this.Emitter);
                    }
                    else
                    {
                        this.Write(customCtor);
                    }

                    this.WriteOpenParentheses();

                    if (!this.Emitter.Validator.IsIgnoreType(type) && type.Methods.Count(m => m.IsConstructor && !m.IsStatic) > (type.IsValueType ? 0 : 1))
                    {
                        this.WriteScript(OverloadsCollection.Create(this.Emitter, ((InvocationResolveResult)this.Emitter.Resolver.ResolveNode(objectCreateExpression, this.Emitter)).Member).GetOverloadName());

                        if (objectCreateExpression.Arguments.Count > 0)
                        {
                            this.WriteComma();
                        }
                    }
                    new ExpressionListBlock(this.Emitter, argsExpressions, paramsArg).Emit();
                    this.WriteCloseParentheses();
                }

                if (hasInitializer)
                {
                    this.WriteComma();                    
                    
                    bool needComma = false;
                    
                    if (isCollectionInitializer)
                    {
                        this.Write("[");
                        this.WriteNewLine();
                        this.Indent();
                    }
                    else
                    {
                        this.BeginBlock();
                    }

                    foreach (Expression item in elements)
                    {
                        if (needComma)
                        {
                            this.WriteComma();
                            this.WriteNewLine();
                        }

                        needComma = true;

                        if (item is NamedExpression)
                        {
                            var namedExpression = (NamedExpression)item;
                            new NameBlock(this.Emitter, namedExpression.Name, namedExpression, namedExpression.Expression).Emit();
                        }
                        else if (item is NamedArgumentExpression)
                        {
                            var namedArgumentExpression = (NamedArgumentExpression)item;
                            new NameBlock(this.Emitter, namedArgumentExpression.Name, namedArgumentExpression, namedArgumentExpression.Expression).Emit();
                        }
                        else if (item is ArrayInitializerExpression)
                        {
                            var arrayInitializer = (ArrayInitializerExpression)item;
                            this.Write("[");
                            foreach (var el in arrayInitializer.Elements)
                            {
                                this.EnsureComma(false);
                                el.AcceptVisitor(this.Emitter);
                                this.Emitter.Comma = true;
                            }
                            this.Write("]");
                            this.Emitter.Comma = false;
                        }
                    }

                    this.WriteNewLine();
                    if (isCollectionInitializer)
                    {
                        this.Outdent();
                        this.Write("]");
                    }
                    else
                    {                        
                        this.EndBlock();
                    }                   
 
                    this.WriteSpace();
                    this.WriteCloseParentheses();
                }
            }

            Helpers.CheckValueTypeClone(invocationResolveResult, this.ObjectCreateExpression, this);
        }

        protected virtual void WriteObjectInitializer(IEnumerable<Expression> expressions, bool changeCase)
        {
            bool needComma = false;

            foreach (Expression item in expressions)
            {
                NamedExpression namedExression = item as NamedExpression;
                NamedArgumentExpression namedArgumentExpression = item as NamedArgumentExpression;

                if (needComma)
                {
                    this.WriteComma();
                }

                needComma = true;
                string name = namedExression != null ? namedExression.Name : namedArgumentExpression.Name;
                if (changeCase)
                {
                    name = Object.Net.Utilities.StringUtils.ToLowerCamelCase(name);
                }
                Expression expression = namedExression != null ? namedExression.Expression : namedArgumentExpression.Expression;

                this.Write(name, ": ");
                expression.AcceptVisitor(this.Emitter);
            }
        }
    }
}
