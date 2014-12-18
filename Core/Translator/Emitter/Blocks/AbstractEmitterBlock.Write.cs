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

        protected virtual void WriteVar(bool ignoreAsync = false)
        {
            if (!this.Emitter.IsAsync || ignoreAsync)
            {
                this.Write("var ");
            }
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

        protected virtual void EnsureComma(bool newLine = true)
        {
            if (this.Emitter.Comma)
            {
                this.WriteComma(newLine);
                this.Emitter.Comma = false;
            }
        }

        protected WriterInfo SaveWriter()
        {
            /*if (this.Emitter.LastSavedWriter != null && this.Emitter.LastSavedWriter.Output == this.Emitter.Output)
            {
                this.Emitter.LastSavedWriter.IsNewLine = this.Emitter.IsNewLine;
                this.Emitter.LastSavedWriter.Level = this.Emitter.Level;
                this.Emitter.LastSavedWriter.Comma = this.Emitter.Comma;
                return this.Emitter.LastSavedWriter;
            }*/
            
            var info = new WriterInfo { 
                Output = this.Emitter.Output,
                IsNewLine = this.Emitter.IsNewLine,
                Level = this.Emitter.Level,
                Comma = this.Emitter.Comma
            };

            this.Emitter.LastSavedWriter = info;

            return info;
        }

        protected bool RestoreWriter(WriterInfo writer)
        {
            if (this.Emitter.Output != writer.Output)
            {
                this.Emitter.Output = writer.Output;
                this.Emitter.IsNewLine = writer.IsNewLine;
                this.Emitter.Level = writer.Level;
                this.Emitter.Comma = writer.Comma;

                return true;
            }

            return false;
        }

        protected StringBuilder NewWriter()
        {
            this.Emitter.Output = new StringBuilder();
            this.Emitter.IsNewLine = false;
            this.Emitter.Level = 0;
            this.Emitter.Comma = false;

            return this.Emitter.Output;
        }


        public int GetNumberOfEmptyLinesAtEnd()
        {
            return AbstractEmitterBlock.GetNumberOfEmptyLinesAtEnd(this.Emitter.Output);
        }

        public static int GetNumberOfEmptyLinesAtEnd(StringBuilder buffer)
        {
            int count = 0;
            bool lastNewLineFound = false;
            int i = buffer.Length - 1;
            var charArray = buffer.ToString().ToCharArray();

            while (i >= 0)
            {
                char c = charArray[i];
                if (!Char.IsWhiteSpace(c))
                {
                    return count;
                }

                if (c == '\n')
                {
                    if (!lastNewLineFound) 
                    {
                        lastNewLineFound = true;
                    }
                    else
                    {
                        count++; ;
                    }                        
                }
                i--;
            }

            return count;
        }

        public bool IsOnlyWhitespaceOnPenultimateLine(bool lastTwoLines = true)
        {
            return AbstractEmitterBlock.IsOnlyWhitespaceOnPenultimateLine(this.Emitter.Output, lastTwoLines);
        }

        public static bool IsOnlyWhitespaceOnPenultimateLine(StringBuilder buffer, bool lastTwoLines = true)
        {
            int i = buffer.Length - 1;
            var charArray = buffer.ToString().ToCharArray();

            while (i >= 0)
            {
                char c = charArray[i];
                if (!Char.IsWhiteSpace(c))
                {
                    return false;
                }

                if (c == '\n')
                {
                    if (lastTwoLines)
                    {
                        lastTwoLines = false;
                    }
                    else
                    {
                        return true;
                    }
                }
                i--;
            }

            return true;
        }

        public void RemovePenultimateEmptyLines(bool withLast = false)
        {
            AbstractMethodBlock.RemovePenultimateEmptyLines(this.Emitter.Output, withLast);
        }

        public static StringBuilder RemovePenultimateEmptyLines(StringBuilder buffer, bool withLast = false)
        {
            if (buffer.Length != 0)
            {
                int length = buffer.Length;
                int i = length - 1;
                var charArray = buffer.ToString().ToCharArray();
                int start = -1;
                int end = -1;
                bool firstCR = true;

                while (Char.IsWhiteSpace(charArray[i]) && (i > -1))
                {
                    if (charArray[i] == '\n')
                    {
                        if (firstCR)
                        {
                            firstCR = false;
                            end = i;

                            if (withLast)
                            {
                                start = i;
                            }                            
                        }
                        else
                        {                            
                            start = i + 1;
                        }
                    }

                    i--;
                }

                if (start > -1 && end > -1)
                {
                    buffer.Remove(start, end - start + 1);
                }
            }
            return buffer;
        }

        public static bool IsReturnLast(string str)
        {
            str = str.TrimEnd();
            return str.EndsWith("return;");
        }

        public static bool IsContinueLast(string str)
        {
            str = str.TrimEnd();
            return str.EndsWith("continue;");
        }

        public static bool IsJumpStatementLast(string str)
        {
            str = str.TrimEnd();
            return str.EndsWith("continue;") || str.EndsWith("return;") || str.EndsWith("break;");
        }
    }

    public class WriterInfo
    {
        public StringBuilder Output
        {
            get;
            set;
        }

        public bool IsNewLine
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }

        public bool Comma
        {
            get;
            set;
        }
    }
}
