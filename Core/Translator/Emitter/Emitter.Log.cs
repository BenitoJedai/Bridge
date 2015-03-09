using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.NET
{
    public partial class Emitter : ILog
    {
        public Action<string, string> Log
        {
            get;
            set;
        }

        public virtual void LogWarning(string message)
        {
            this.LogMessage("warning", message);
        }

        public virtual void LogError(string message)
        {
            this.LogMessage("error", message);
        }

        public virtual void LogMessage(string message)
        {
            this.LogMessage("message", message);
        }

        public virtual void LogMessage(string level, string message)
        {
            if (this.Log != null)
            {
                this.Log(level, message);
            }
        }    
    }
}
