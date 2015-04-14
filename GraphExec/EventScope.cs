
namespace GraphExec
{
    public class EventScope : IScope<EventLevel, EventScope>
    {
        public EventScope()
        {
            this.Scope = this;
            this.ScopeLevel = EventLevel.Domain;
        }

        public EventScope Scope { get; set; }

        public EventLevel ScopeLevel { get; set; }
    }

    public abstract class EventScope<TEventType> : EventScope, IHandle<TEventType>
    {
        public abstract void OnHandle(TEventType evt);
    }
}
