using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using System.Text;
using Bridge.Contract;

namespace Bridge.NET
{
    public class InlineArgumentsBlock : AbstractEmitterBlock
    {
        public InlineArgumentsBlock(IEmitter emitter, ArgumentsInfo argsInfo, string inline)
        {
            this.Emitter = emitter;
            this.ArgumentsInfo = argsInfo;
            this.InlineCode = inline;

            argsInfo.AddExtensionParam();
        }
        
        public ArgumentsInfo ArgumentsInfo 
        { 
            get; 
            set; 
        }

        public string InlineCode 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.EmitInlineExpressionList(this.ArgumentsInfo, this.InlineCode);
        }

        private static Regex _formatArg = new Regex(@"\{(\*?)(\w+)\}");
        protected virtual IList<Expression> GetExpressionsByKey(IEnumerable<NamedParamExpression> expressions, string key)
        {

            if (Regex.IsMatch(key, "^\\d+$"))
            {
                var list = new List<Expression>();
                list.Add(expressions.Skip(int.Parse(key)).First().Expression);
                return list;
            }

            return expressions.Where(e => e.Name == key).Select(e => e.Expression).ToList();
        }

        protected virtual AstType GetAstTypeByKey(IEnumerable<TypeParamExpression> types, string key)
        {
            return types.Where(e => e.Name == key && e.AstType != null).Select(e => e.AstType).FirstOrDefault();
        }

        protected virtual IType GetITypeByKey(IEnumerable<TypeParamExpression> types, string key)
        {
            return types.Where(e => e.Name == key && e.IType != null).Select(e => e.IType).FirstOrDefault();
        }

        protected virtual void EmitInlineExpressionList(ArgumentsInfo argsInfo, string inline)
        {
            IEnumerable<NamedParamExpression> expressions = argsInfo.NamedExpressions;
            IEnumerable<TypeParamExpression> typeParams = argsInfo.TypeArguments;
            Expression paramsArg = argsInfo.ParamsExpression;

            var matches = _formatArg.Matches(inline);

            this.Write("");
            inline = _formatArg.Replace(inline, delegate(Match m)
            {
                int count = this.Emitter.Writers.Count;
                string key = m.Groups[2].Value;
                bool ignoreArray = (m.Groups[1].Success && m.Groups[1].Value == "*") || argsInfo.ParamsExpression == null;

                StringBuilder oldSb = this.Emitter.Output;
                this.Emitter.Output = new StringBuilder();

                if (key == "this" || key == argsInfo.ThisName || (key == "0" && argsInfo.IsExtensionMethod))
                {
                    string thisValue = argsInfo.GetThisValue();

                    if (thisValue != null)
                    {
                        this.Write(thisValue);
                    }
                }
                else
                {
                    IList<Expression> exprs = this.GetExpressionsByKey(expressions, key);
                    if (exprs.Count > 0)
                    {
                        if (exprs.Count > 1)
                        {
                            if (!ignoreArray)
                            {
                                this.Write("[");
                            }

                            new ExpressionListBlock(this.Emitter, exprs, null).Emit();

                            if (!ignoreArray)
                            {
                                this.Write("]");
                            }
                        }
                        else
                        {
                            exprs[0].AcceptVisitor(this.Emitter);
                        }
                    }
                    else if (typeParams != null)
                    {
                        var type = this.GetAstTypeByKey(typeParams, key);

                        if (type != null)
                        {
                            type.AcceptVisitor(this.Emitter);
                        }
                        else
                        {
                            var iType = this.GetITypeByKey(typeParams, key);

                            if (iType != null)
                            {
                                new CastBlock(this.Emitter, iType).Emit();
                            }
                        }
                    }
                }

                if (this.Emitter.Writers.Count != count)
                {
                    this.PopWriter();
                }

                string replacement = this.Emitter.Output.ToString();
                this.Emitter.Output = oldSb;

                return replacement;
            });

            this.Write(inline);
        }
    }
}
