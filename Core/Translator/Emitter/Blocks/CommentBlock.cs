using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bridge.NET
{
    public class CommentBlock : AbstractEmitterBlock
    {
        public CommentBlock(IEmitter emitter, Comment comment)
        {
            this.Emitter = emitter;
            this.Comment = comment;
        }

        public Comment Comment 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.VisitComment();
        }

        private static Regex injectComment = new Regex("@(.*)@?", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static Regex removeStars = new Regex("(^\\s*)(\\* )", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        protected virtual void WriteMultiLineComment(string text)
        {
            this.Write("/* " + text + "*/");
            this.WriteNewLine();
        }

        protected virtual void WriteSingleLineComment(string text)
        {
            this.Write("//" + text);
            this.WriteNewLine();
        }

        protected void VisitComment()
        {
            Comment comment = this.Comment;

            Match injection = injectComment.Match(comment.Content);
            if (comment.CommentType == CommentType.MultiLine && injection.Success)
            {
                string code = removeStars.Replace(injection.Groups[1].Value, "$1");
                if (code.EndsWith("@"))
                {
                    code = code.Substring(0, code.Length - 1);
                }
                this.Write(code);
                this.WriteNewLine();
            }
            else if (comment.CommentType == CommentType.MultiLine)
            {
                this.WriteMultiLineComment(comment.Content);
            }
            else if (comment.CommentType == CommentType.SingleLine)
            {
                this.WriteSingleLineComment(comment.Content);
            }
        }
    }
}
