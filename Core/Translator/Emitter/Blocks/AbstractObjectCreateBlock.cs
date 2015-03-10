using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;

namespace Bridge.NET
{
    public abstract class AbstractObjectCreateBlock : AbstractEmitterBlock
    {
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
