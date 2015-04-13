
namespace GraphExec
{
    public interface IEventAggregator
    {
        /// <summary>
        /// The given IHandle&lt;<typeparamref name="TEventType"/>&gt; subscribes to the specified event type. Manual unsubscribing is not necessary if <paramref name="handleOnce"/> is true.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to subscribe to</typeparam>
        /// <param name="handler">The subscriber</param>
        /// <param name="handleOnce">Determines whether the handler should be called only once during publishing. <b>True</b> = handler is called only once</param>
        void Sub<TEventType>(IHandle<TEventType> handler, bool handleOnce);

        /// <summary>
        /// The given IHandle&lt;<typeparamref name="TEventType"/>&gt; subscribes to the specified event type. Manual unsubscribing is necessary.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to subscribe to</typeparam>
        /// <param name="handler">The subscriber</param>
        void Sub<TEventType>(IHandle<TEventType> handler);

        /// <summary>
        /// Unsubscribe the given handler. Does not throw if handler is not currently subscribed.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to unsubscribe from</typeparam>
        /// <param name="handler">The subscriber to remove from subscription</param>
        /// <returns>true if handler is successfully subscribed; otherwise, false. This method also
        ///     returns false if handler was not found in the registry.</returns>
        bool Unsub<TEventType>(IHandle<TEventType> handler);

        /// <summary>
        /// Unsubscribes all handlers of the given event type. Does not throw if no handlers are currently subscribed.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to unsubscribe from</typeparam>
        /// <returns>true if handlers are successfully unsubscribed; otherwise, false. This method also
        ///     returns false if the handlers were not found in the registry.</returns>
        bool Unsub<TEventType>();

        /// <summary>
        /// Unsubscribes all registered handlers
        /// </summary>
        /// <returns>true if all handlers were found and unsubscribed; otherwise, false.</returns>
        void UnsubAll();

        /// <summary>
        /// Publish the specified event type passing the given event object to all Subscribers
        /// </summary>
        /// <typeparam name="TEventType">The type of event to publish</typeparam>
        /// <param name="evt">The event to publish. This object is passed to any subscribers</param>
        void Pub<TEventType>(TEventType evt);
    }
}
