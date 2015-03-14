using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Bridge.Contract;

namespace Bridge.NET
{
    public class IndexerBlock : AbstractEmitterBlock
    {
        public IndexerBlock(IEmitter emitter, IndexerExpression indexerExpression)
        {
            this.Emitter = emitter;
            this.IndexerExpression = indexerExpression;
        }

        public IndexerExpression IndexerExpression 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitIndexerExpression();
        }

        protected void VisitIndexerExpression()
        {
            IndexerExpression indexerExpression = this.IndexerExpression;

            IAttribute inlineAttr = null;
            var resolveResult = this.Emitter.Resolver.ResolveNode(indexerExpression, this.Emitter);

            if (resolveResult is InvocationResolveResult)
            {
                var member = ((InvocationResolveResult)resolveResult).Member;
                if (member is SpecializedProperty)
                {
                    var specProp = (SpecializedProperty)member;
                    var method = this.Emitter.IsAssignment ? specProp.Setter : specProp.Getter;
                    inlineAttr = this.Emitter.GetAttribute(method.Attributes, Translator.Bridge_ASSEMBLY + ".TemplateAttribute");
                }
            }

            indexerExpression.Target.AcceptVisitor(this.Emitter);

            if (inlineAttr != null)
            {
                var inlineCode = inlineAttr.PositionalArguments[0];
                if (inlineCode.ConstantValue != null)
                {
                    string code = inlineCode.ConstantValue.ToString();

                    this.WriteDot();
                    this.PushWriter(code);
                    new ExpressionListBlock(this.Emitter, indexerExpression.Arguments, null).Emit();

                    if (!this.Emitter.IsAssignment)
                    {
                        this.PopWriter();
                    }
                    else
                    {
                        this.WriteComma();
                    }
                }
            }
            else
            {
                if (indexerExpression.Arguments.Count != 1)
                {
                    throw (Exception)this.Emitter.CreateException(indexerExpression, "Only one index is supported");
                }

                var index = indexerExpression.Arguments.First();

                var primitive = index as PrimitiveExpression;

                if (primitive != null && primitive.Value != null && Regex.Match(primitive.Value.ToString(), "^[_$a-z][_$a-z0-9]*$", RegexOptions.IgnoreCase).Success)
                {
                    this.WriteDot();
                    this.Write(primitive.Value);
                }
                else
                {
                    this.WriteOpenBracket();
                    index.AcceptVisitor(this.Emitter);
                    this.WriteCloseBracket();
                }
            }
        }
    }
}
