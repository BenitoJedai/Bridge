using Bridge.Contract;
using Mono.Cecil;
using System.Collections.Generic;

namespace Bridge.NET
{
    public partial class Emitter : Visitor, IEmitter
    {
        public Emitter(IDictionary<string, TypeDefinition> typeDefinitions, List<ITypeInfo> types, IValidator validator)
        {
            this.TypeDefinitions = typeDefinitions;
            this.Types = types;
            this.Types.Sort(this.CompareTypeInfos);
            this.Validator = validator;            
        }

        public virtual Dictionary<string, string> Emit()
        {
            new EmitBlock(this).Emit();
            return this.TransformOutputs();
        }        
    }
}
