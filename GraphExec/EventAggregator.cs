
namespace GraphExec
{
    /// <summary>
    /// Easily subscribe and publish events using a stream-lined pub-sub system with dependency injection support.
    /// </summary>
    public sealed class EventAggregator : EventScopeManager, IEventAggregator
    {
        /// <summary>
        /// Create a new EventAggregator instance
        /// </summary>
        public EventAggregator()
        {
            this.m_aggregator = new InternalEventAggregator();
        }

        private InternalEventAggregator m_aggregator;

        private InternalEventAggregator InternalAggregator
        {
            get
            {
                this.m_aggregator.CurrentScope = this.CurrentScope;

                return this.m_aggregator;
            }
        }

        /// <summary>
        /// The given IHandle&lt;<typeparamref name="TEventType"/>&gt; subscribes to the specified event type. Manual unsubscribing is not necessary if <paramref name="handleOnce"/> is true.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to subscribe to</typeparam>
        /// <param name="handler">The subscriber</param>
        /// <param name="handleOnce">Determines whether the handler should be called only once during publishing. <b>True</b> means the handler should be called only once</param>
        public void Sub<TEventType>(IHandle<TEventType> handler, bool handleOnce)
        {
            Args.IsNotNull(() => handler);

            this.InternalAggregator.InternalSub(handler, handleOnce);
        }

        /// <summary>
        /// The given IHandle&lt;<typeparamref name="TEventType"/>&gt; subscribes to the specified event type. Manual unsubscribing is necessary.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to subscribe to</typeparam>
        /// <param name="handler">The subscriber</param>
        public void Sub<TEventType>(IHandle<TEventType> handler)
        {
            this.Sub(handler, false);
        }

        /// <summary>
        /// Unsubscribe the given handler. Does not throw if handler is not currently subscribed.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to unsubscribe from</typeparam>
        /// <param name="handler">The subscriber to remove from subscription</param>
        /// <returns>true if handler is successfully subscribed; otherwise, false. This method also
        ///     returns false if handler was not found in the registry.</returns>
        public bool Unsub<TEventType>(IHandle<TEventType> handler)
        {
            // Do not accept unsubscribing of null objects
            Args.IsNotNull(() => handler);

            return this.InternalAggregator.InternalUnsub(handler);
        }

        /// <summary>
        /// Unsubscribes all handlers of the given event type. Does not throw if no handlers are currently subscribed.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to unsubscribe from</typeparam>
        /// <returns>true if handlers are successfully unsubscribed; otherwise, false. This method also
        ///     returns false if the handlers were not found in the registry.</returns>
        public bool Unsub<TEventType>()
        {
            return this.InternalAggregator.InternalUnsub<TEventType>();
        }

        /// <summary>
        /// Unsubscribes all registered handlers regardless of scope
        /// </summary>
        /// <returns>true if all handlers were found and unsubscribed; otherwise, false.</returns>
        public void UnsubAll()
        {
            this.InternalAggregator.InternalUnsubAll();
        }

        /// <summary>
        /// Publish the specified event type passing the given event object to all relevant subscribers
        /// </summary>
        /// <typeparam name="TEventType">The type of event to publish</typeparam>
        /// <param name="evt">The event to publish. This object is passed to any subscribers</param>
        public void Pub<TEventType>(TEventType evt)
        {
            this.InternalAggregator.InternalPub(evt);
        }
    }
}
