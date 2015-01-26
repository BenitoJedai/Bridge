using ICSharpCode.NRefactory.CSharp;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using System;
using System.Text;

namespace Bridge.NET
{
    public partial class ConstructorBlock
    {
        private IEnumerable<string> aspects;
        public virtual IEnumerable<string> GetAspects()
        {
            if (this.aspects != null)
            {
                return this.aspects;
            }

            var parameters = new ParameterAspectBlock(this).GetAspects();
            var methods = new MethodAspectBlock(this).GetAspects();
            var properties = new PropertyAspectBlock(this).GetAspects();
            var types = new TypeAspectBlock(this).GetAspects();
            var notify = new NotifyPropertyChangedAspectBlock(this).GetAspects();

            this.aspects = parameters.Concat(types).Concat(methods).Concat(properties).Concat(notify);
            return this.aspects;
        }
    }
}
