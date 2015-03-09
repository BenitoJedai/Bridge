using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.NET
{
    public class EventBlock : AbstractEmitterBlock
    {
        public EventBlock(IEmitter emitter, IEnumerable<EventDeclaration> events)
        {
            this.Emitter = emitter;
            this.Events = events;
        }

        public IEnumerable<EventDeclaration> Events 
        { 
            get; 
            set; 
        }

        public override void Emit()
        {
            this.EmitEvents(this.Events);
        }

        protected virtual void EmitEvents(IEnumerable<EventDeclaration> events)
        {
            foreach (var evt in events)
            {
                foreach (var evtVar in evt.Variables)
                {
                    string name = this.Emitter.GetEntityName(evt);

                    this.Write("this.", name, " = ");
                    evtVar.Initializer.AcceptVisitor(this.Emitter);
                    this.WriteSemiColon();
                    this.WriteNewLine();
                }                
            }
        }        
    }
}
