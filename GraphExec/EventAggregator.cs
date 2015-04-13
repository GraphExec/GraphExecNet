using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExec
{
    /// <summary>
    /// Easily subscribe and publish events using a stream-lined pub-sub system with dependency injection support.
    /// </summary>
    public sealed class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// Create a new EventAggregator instance
        /// </summary>
        public EventAggregator()
        {
            this.m_eventRegistry = new Dictionary<Type, List<IHandle>>();
        }

        /// <summary>The event handling registry</summary>
        private Dictionary<Type, List<IHandle>> m_eventRegistry;

        /// <summary>
        /// The given IHandle&lt;<typeparamref name="TEventType"/>&gt; subscribes to the specified event type. Manual unsubscribing is not necessary if <paramref name="handleOnce"/> is true.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to subscribe to</typeparam>
        /// <param name="handler">The subscriber</param>
        /// <param name="handleOnce">Determines whether the handler should be called only once during publishing. <b>True</b> = handler is called only once</param>
        public void Sub<TEventType>(IHandle<TEventType> handler, bool handleOnce)
        {
            // Cannot accept a null handler
            Args.IsNotNull(() => handler);

            // Cannot accept a null registry
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            // store event Type
            var type = typeof(TEventType);
            Vars.HandleNull(type, this.HandleNull);

            // Handle cases:
            //
            // - When event type has not been registered
            // - When event type has been registered
            // - - Make sure the handler isn't already included

            // Event type not registered
            if (!this.m_eventRegistry.Keys.Contains(type))
            {
                // create registry item
                var item = new RegistryItem<TEventType>(handler, handleOnce);

                // add item to registry
                this.m_eventRegistry.Add(type, new List<IHandle>()
                {
                    item
                });
            }
            else // Event type has been registered
            {
                // get handler objects from registry - do not accept null handlers
                var handlers = this.m_eventRegistry[type].Select(x => x as RegistryItem<TEventType>).Select(x => x.Handler);
                Vars.HandleNull(handlers, this.HandleNull);

                // Do not support duplicate registrations
                Throw.Exception<InvalidOperationException>(() => handlers.Contains(handler));

                // Note: if we made it this far, the event type is registered correctly

                // create registry item - do not accept null registry item
                var item = new RegistryItem<TEventType>(handler, handleOnce);
                Vars.HandleNull(item, this.HandleNull);

                // add the registry item to the existing list of subscribers
                this.m_eventRegistry[type].Add(item);
            }
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

            // Cannot accept a null registry
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            // store event Type
            var type = typeof(TEventType);
            Vars.HandleNull(type, this.HandleNull);

            // Only proceed if there are subscribed items in the registry for the given event type
            if (!this.m_eventRegistry.Keys.Contains(type))
            {
                return false;
            }

            // Only proceed if there are items to unsubscribe
            if (!this.m_eventRegistry[type].Any())
            {
                return false;
            }

            // store registry items - do not accept null registry
            var registry = this.m_eventRegistry[type].Select(x => x as RegistryItem<TEventType>);
            Vars.HandleNull(registry, this.HandleNull);

            // store handler objects from registry - do not accept null handlers
            var handlers = registry.Select(x => x.Handler);
            Vars.HandleNull(handlers, this.HandleNull);

            // Handle cases:
            //
            // - Given handler is not in registry
            // - Handler is in registry (remove handler)
            // - - registry for event type is not empty
            // - - registry for event type is empty (remove registry for event type)

            // Only proceed if the registry contains the given handler
            if (!handlers.Contains(handler))
            {
                return false;
            }

            // store desired registry item - do not accept null registry item
            var registryItem = registry.Where(x => x.Handler == handler);
            Vars.HandleNull(registryItem, this.HandleNull);

            // remove registry item
            return this.m_eventRegistry[type].Remove(registryItem as IHandle);
        }

        /// <summary>
        /// Unsubscribes all handlers of the given event type. Does not throw if no handlers are currently subscribed.
        /// </summary>
        /// <typeparam name="TEventType">The type of event to unsubscribe from</typeparam>
        /// <returns>true if handlers are successfully unsubscribed; otherwise, false. This method also
        ///     returns false if the handlers were not found in the registry.</returns>
        public bool Unsub<TEventType>()
        {
            // Cannot accept a null registry
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            // store event Type - do not accept null Type
            var type = typeof(TEventType);
            Vars.HandleNull(type, this.HandleNull);

            // Only proceed if there are handlers for the given event type
            if (!this.m_eventRegistry.Keys.Contains(type))
            {
                return false;
            }

            // remove registry items
            return this.m_eventRegistry.Remove(type);
        }

        /// <summary>
        /// Unsubscribes all registered handlers
        /// </summary>
        /// <returns>true if all handlers were found and unsubscribed; otherwise, false.</returns>
        public void UnsubAll()
        {
            // Cannot accept a null registry
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            // remove all handlers
            this.m_eventRegistry.Clear();
        }

        /// <summary>
        /// Publish the specified event type passing the given event object to all Subscribers
        /// </summary>
        /// <typeparam name="TEventType">The type of event to publish</typeparam>
        /// <param name="evt">The event to publish. This object is passed to any subscribers</param>
        public void Pub<TEventType>(TEventType evt)
        {
            // Cannot accept a null registry
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            // store event Type - do not accept null Type
            var type = typeof(TEventType);
            Vars.HandleNull(type, this.HandleNull);

            // Publish cases:
            //
            // - Event is not subscribed to (Throw)
            // - Event is subscribed to (Handle)

            // Only proceed if there are handlers for the given event type
            if (!this.m_eventRegistry.Keys.Contains(type))
            {
                return;
            }

            // store subscribers - do not accept null subscribers
            var subscribers = this.m_eventRegistry[type];
            Vars.HandleNull(subscribers, this.HandleNull);

            // Only proceed if there are subscribers
            if (!subscribers.Any())
            {
                return;
            }

            // process subscribers if there are any
            foreach (var subscriber in subscribers)
            {
                // store subscriber as RegistryItem<TEventType> - do not accept null subscriber
                var sub = subscriber as RegistryItem<TEventType>;
                Vars.HandleNull(sub, this.HandleNull);

                // Handle once case - unsubscribe and handle event
                if (sub.HandleOnce)
                {
                    // throw if the handler was not unsubscribed
                    Throw.Exception<InternalGraphExecException>(() => !this.Unsub(sub.Handler));

                    sub.Handler.OnHandle(evt);
                }
                else // Handle until removed case - handle event
                {
                    sub.Handler.OnHandle(evt);
                }
            }
        }

        /// <summary>
        /// Default method to handle null variables
        /// </summary>
        private void HandleNull()
        {
            throw new InternalGraphExecException();
        }

        /// <summary>
        /// Internal class to encapsulate event handler
        /// </summary>
        /// <typeparam name="TEventType">The type of event to be handled</typeparam>
        private class RegistryItem<TEventType> : IHandle
        {
            internal RegistryItem(IHandle<TEventType> handler, bool handleOnce)
            {
                this.Handler = handler;
                this.HandleOnce = handleOnce;
            }

            internal IHandle<TEventType> Handler { get; set; }

            internal bool HandleOnce { get; set; }
        }
    }
}
