using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ICSharpCode.NRefactory.TypeSystem;
using System.Text;
using Mono.Cecil;
using Ext.Net.Utilities;
using ICSharpCode.NRefactory.Semantics;
using Bridge.Plugin;

namespace Bridge.NET
{
    public class InjectMethodDetectorBlock : AbstractMethodBlock
    {
        public InjectMethodDetectorBlock(IEmitter emitter, BlockStatement blockStatement)
        {
            this.Emitter = emitter;
            this.BlockStatement = blockStatement;
        }

        public BlockStatement BlockStatement
        {
            get;
            set;
        }

        public override void Emit()
        {
            this.Emitter.InjectMethodDetectors = false;

            string detectors = this.GetMethodsDetector(this.Emitter.MethodsGroupBuilder);
            bool noChildren = this.BlockStatement.Children.ToList().Count == 0;

            if (noChildren)
            {
                detectors = Ext.Net.Utilities.StringUtils.ReplaceLastInstanceOf(detectors, Environment.NewLine, "");
            }

            detectors = this.WriteIndentToString(detectors);

            this.Write(detectors);

            if (noChildren)
            {
                this.WriteNewLine();
            }
            else
            {
                this.Write("else if (arguments.length == 0)");
                this.WriteSpace();
                this.BeginBlock();
            }
        }
    }
}
