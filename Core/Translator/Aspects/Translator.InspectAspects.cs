using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;
using System;

namespace Bridge.NET
{
    public partial class Translator
    {
        protected virtual void InspectGlobalAspects(Emitter emitter)
        {
            if (this.AssemblyDefinition.HasCustomAttributes)
            {
                foreach (var attr in this.AssemblyDefinition.CustomAttributes)
                {
                    if (AspectHelpers.IsMulticastAspectAttribute(attr.AttributeType, emitter))
                    {
                        var globalAspects = this.AssemblyInfo.Aspects;

                        List<AspectInfo> aspects;
                        if (globalAspects.ContainsKey(AttributeTargets.Assembly))
                        {
                            aspects = globalAspects[AttributeTargets.Assembly];
                        }
                        else
                        {
                            aspects = new List<AspectInfo>();
                            globalAspects.Add(AttributeTargets.Assembly, aspects);
                        }

                        AspectInfo aspect = AspectHelpers.GetAspectInfo(attr, emitter, AttributeTargets.Assembly, null);
                        aspects.Add(aspect);                
                    }
                }
            }
        }
    }
}
