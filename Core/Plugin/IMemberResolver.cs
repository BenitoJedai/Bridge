using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Plugin
{
    public interface IMemberResolver
    {
        ResolveResult ResolveNode(AstNode node, ILog log);
    }
}
