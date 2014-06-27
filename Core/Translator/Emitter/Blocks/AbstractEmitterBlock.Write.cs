using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public partial class AbstractEmitterBlock
    {
        protected virtual void Indent()
        {
            ++this.Emitter.Level;
        }

        protected virtual void Outdent()
        {
            if (this.Emitter.Level > 0)
            {
                this.Emitter.Level--;
            }
        }

        protected virtual void WriteIndent()
        {
            if (!this.Emitter.IsNewLine)
            {
                return;
            }

            for (var i = 0; i < this.Emitter.Level; i++)
            {
                this.Emitter.Output.Append("    ");
            }

            this.Emitter.IsNewLine = false;
        }

        protected virtual void WriteNewLine()
        {
            this.Emitter.Output.Append('\n');
            this.Emitter.IsNewLine = true;
        }

        protected virtual void BeginBlock()
        {
            this.WriteOpenBrace();
            this.WriteNewLine();
            this.Indent();
        }

        protected virtual void EndBlock()
        {
            this.Outdent();
            this.WriteCloseBrace();
        }

        protected virtual void Write(object value)
        {
            this.WriteIndent();
            this.Emitter.Output.Append(value);
        }

        protected virtual void Write(params object[] values)
        {
            foreach (var item in values)
            {
                this.Write(item);
            }
        }

        protected virtual void WriteScript(object value)
        {
            this.WriteIndent();
            this.Emitter.Output.Append(this.Emitter.ToJavaScript(value));
        }

        protected virtual void WriteComment(string text)
        {
            this.Write("/* " + text + " */");
            this.WriteNewLine();
        }

        protected virtual void WriteComma()
        {
            this.WriteComma(false);
        }

        protected virtual void WriteComma(bool newLine)
        {
            this.Write(",");

            if (newLine)
            {
                this.WriteNewLine();
            }
            else
            {
                this.WriteSpace();
            }
        }

        protected virtual void WriteThis()
        {
            this.Write("this");
            this.Emitter.ThisRefCounter++;
        }

        protected virtual void WriteSpace()
        {
            this.WriteSpace(true);
        }

        protected virtual void WriteSpace(bool addSpace)
        {
            if (addSpace)
            {
                this.Write(" ");
            }
        }

        protected virtual void WriteDot()
        {
            this.Write(".");
        }

        protected virtual void WriteColon()
        {
            this.Write(": ");
        }

        protected virtual void WriteSemiColon()
        {
            this.WriteSemiColon(false);
        }

        protected virtual void WriteSemiColon(bool newLine)
        {
            if (this.Emitter.SkipSemiColon)
            {
                this.Emitter.SkipSemiColon = false;
                return;
            }

            this.Write(";");

            if (newLine)
            {
                this.WriteNewLine();
            }
        }

        protected virtual void WriteNew()
        {
            this.Write("new ");
        }

        protected virtual void WriteVar()
        {
            this.Write("var ");
        }

        protected virtual void WriteIf()
        {
            this.Write("if ");
        }

        protected virtual void WriteElse()
        {
            this.Write("else ");
        }

        protected virtual void WriteWhile()
        {
            this.Write("while ");
        }

        protected virtual void WriteFor()
        {
            this.Write("for ");
        }

        protected virtual void WriteThrow()
        {
            this.Write("throw ");
        }

        protected virtual void WriteTry()
        {
            this.Write("try ");
        }

        protected virtual void WriteCatch()
        {
            this.Write("catch ");
        }

        protected virtual void WriteFinally()
        {
            this.Write("finally ");
        }

        protected virtual void WriteDo()
        {
            this.Write("do ");
        }

        protected virtual void WriteSwitch()
        {
            this.Write("switch ");
        }

        protected virtual void WriteReturn(bool addSpace)
        {
            this.Write("return");
            this.WriteSpace(addSpace);
        }

        protected virtual void WriteOpenBracket()
        {
            this.WriteOpenBracket(false);
        }

        protected virtual void WriteOpenBracket(bool addSpace)
        {
            this.Write("[");
            this.WriteSpace(addSpace);
        }

        protected virtual void WriteCloseBracket()
        {
            this.WriteCloseBracket(false);
        }

        protected virtual void WriteCloseBracket(bool addSpace)
        {
            this.WriteSpace(addSpace);
            this.Write("]");
        }

        protected virtual void WriteOpenParentheses()
        {
            this.WriteOpenParentheses(false);
        }

        protected virtual void WriteOpenParentheses(bool addSpace)
        {
            this.Write("(");
            this.WriteSpace(addSpace);
        }

        protected virtual void WriteCloseParentheses()
        {
            this.WriteCloseParentheses(false);
        }

        protected virtual void WriteCloseParentheses(bool addSpace)
        {
            this.WriteSpace(addSpace);
            this.Write(")");
        }

        protected virtual void WriteOpenCloseParentheses()
        {
            this.WriteOpenCloseParentheses(false);
        }

        protected virtual void WriteOpenCloseParentheses(bool addSpace)
        {
            this.Write("()");
            this.WriteSpace(addSpace);
        }

        protected virtual void WriteOpenBrace()
        {
            this.WriteOpenBrace(false);
        }

        protected virtual void WriteOpenBrace(bool addSpace)
        {
            this.Write("{");
            this.WriteSpace(addSpace);
        }

        protected virtual void WriteCloseBrace()
        {
            this.WriteCloseBrace(false);
        }

        protected virtual void WriteCloseBrace(bool addSpace)
        {
            this.WriteSpace(addSpace);
            this.Write("}");
        }

        protected virtual void WriteOpenCloseBrace()
        {
            this.Write("{ }");
        }

        protected virtual void WriteFunction()
        {
            this.Write("function ");
        }

        protected virtual void PushWriter(string format)
        {
            this.Emitter.Writers.Push(new Tuple<string, StringBuilder, bool>(format, this.Emitter.Output, this.Emitter.IsNewLine));
            this.Emitter.IsNewLine = false;
            this.Emitter.Output = new StringBuilder();
        }

        protected virtual string PopWriter(bool preventWrite = false)
        {
            string result = this.Emitter.Output.ToString();
            var tuple = this.Emitter.Writers.Pop();
            this.Emitter.Output = tuple.Item2;
            result = tuple.Item1 != null ? string.Format(tuple.Item1, result) : result;
            this.Emitter.IsNewLine = tuple.Item3;
            if (!preventWrite)
            {
                this.Write(result);
            }

            return result;
        }

        protected virtual string WriteIndentToString(string value)
        {
            StringBuilder output = new StringBuilder();
            for (var i = 0; i < this.Emitter.Level; i++)
            {
                output.Append("    ");
            }

            string indent = output.ToString();

            return value.Replace("\n", "\n" + indent);
        }

        protected virtual void EnsureComma()
        {
            if (this.Emitter.Comma)
            {
                this.WriteComma(true);
                this.Emitter.Comma = false;
            }
        }
    }
}
