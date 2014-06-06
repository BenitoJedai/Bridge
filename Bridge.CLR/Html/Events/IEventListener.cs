// Event WebAPI by Mozilla Contributors is licensed under CC-BY-SA 2.5.
// https://developer.mozilla.org/en-US/docs/Web/API/EventListener
namespace Bridge.CLR.Html
{
    [Ignore]
    [Name("Object")]
    [Bridge.CLR.Cast("function(obj){return Bridge.isFunction(obj.handleEvent);}")]
    public interface IEventListener
    {
        /// <summary>
        /// This method is called whenever an event occurs of the type for which the EventListener interface was registered.
        /// </summary>
        /// <param name="e">The DOM Event to register.</param>
        void HandleEvent(Event e);
    }
}
