using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Contract
{
    public interface IMemberResolver
    {
        ResolveResult ResolveNode(AstNode node, ILog log);
        CSharpAstResolver Resolver
        {
            get;
        }

        ICompilation Compilation
        {
            get;
        }
    }
}
