
namespace GraphExec
{
    public interface IEventScopeManager : IScopeManager<EventLevel, EventScope>
    {
        
    }

    /// <summary>
    /// Provides basic event scope management
    /// </summary>
    public class EventScopeManager : EventScope, IEventScopeManager
    {
        /// <summary>
        /// Gets the scope level of the current scope
        /// </summary>
        public EventLevel CurrentLevel
        {
            get
            {
                if (this.CurrentScope != null)
                {
                    return this.CurrentScope.ScopeLevel;
                }
                else
                {
                    return EventLevel.Domain;
                }
            }
        }

        /// <summary>
        /// Gets or sets the previous scope object
        /// </summary>
        private EventScope PreviousScope { get; set; }

        /// <summary>
        /// Gets the current scope
        /// </summary>
        public EventScope CurrentScope { get; protected set; }

        /// <summary>
        /// Sets the current scope to the given EventScope object
        /// </summary>
        /// <param name="scope">The EventScope object</param>
        public void BeginScope(EventScope scope)
        {
            this.PreviousScope = this.CurrentScope;
            this.CurrentScope = scope;
        }

        /// <summary>
        /// Revert to the previously used scope
        /// </summary>
        /// <param name="scope"></param>
        public void EndScope(EventScope scope)
        {
            this.CurrentScope = this.PreviousScope;
            this.PreviousScope = scope;
        }
    }
}
