using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class EventBlock : AbstractEmitterBlock
    {
        public EventBlock(Emitter emitter, IEnumerable<EventDeclaration> events)
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
                string name = this.Emitter.GetEntityName(evt);

                this.Write("this.", name, " = new Bridge.Event()");
                this.WriteSemiColon();
                this.WriteNewLine();
            }
        }        
    }
}
