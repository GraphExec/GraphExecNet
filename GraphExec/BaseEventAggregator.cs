using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExec
{
    internal class BaseEventAggregator
    {
        internal BaseEventAggregator()
        {
            this.m_eventRegistry = new Dictionary<Type, List<IHandle>>();
        }

        protected Dictionary<Type, List<IHandle>> m_eventRegistry;

        internal EventScope CurrentScope { get; set; }

        protected bool EnsureRegistry<TEventType>()
        {
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            var type = this.GetEventType<TEventType>();

            return this.m_eventRegistry.ContainsKey(type);
        }

        protected bool RemoveRegistryItem<TEventType>(Type eventType, IHandle<TEventType> handler)
        {
            var registryItem = this.GetSubscriberByHandler(handler);
            Vars.HandleNull(registryItem, this.HandleNull);

            if (this.m_eventRegistry[eventType].Remove(registryItem))
            {
                this.CleanUpRegistry();

                return true;
            }

            return false;
        }

        protected void CleanUpRegistry()
        {
            var removeable = this.m_eventRegistry.Where(x => x.Value == null || !x.Value.Any());
            for (var i = 0; i < removeable.Count(); i++)
            {
                var item = removeable.ElementAt(i);
                if (this.m_eventRegistry.ContainsKey(item.Key))
                {
                    this.m_eventRegistry.Remove(item.Key);
                }
            }
        }

        protected IEnumerable<IHandle<TEventType>> GetScopedHandlers<TEventType>()
        {
            var subscribers = this.GetScopedSubscribers<TEventType>();
            Vars.HandleNull(subscribers, this.HandleNull);

            return subscribers.Select(x => x.Handler);
        }

        protected RegistryItem<TEventType, EventScope> GetSubscriberByHandler<TEventType>(IHandle<TEventType> handler)
        {
            var subscribers = this.GetScopedSubscribers<TEventType>();
            Vars.HandleNull(subscribers, this.HandleNull);

            return subscribers.Where(x => x.Handler == handler).Single();
        }

        protected IEnumerable<RegistryItem<TEventType, EventScope>> GetScopedSubscribers<TEventType>()
        {
            var subscribers = this.GetAllSubscribers<TEventType>();
            Vars.HandleNull(subscribers, this.HandleNull);

            if (this.CurrentScope.ScopeLevel == EventLevel.Local)
            {
                return subscribers.Where(x => x.Scope == this.CurrentScope);
            }
            else
            {
                return subscribers;
            }
        }

        protected IEnumerable<RegistryItem<TEventType, EventScope>> GetAllSubscribers<TEventType>()
        {
            var type = this.GetEventType<TEventType>();

            return this.m_eventRegistry[type].Select(x => x as RegistryItem<TEventType, EventScope>);
        }

        /// <summary>
        /// Get Type of <typeparamref name="TEventType"/>. Throws if null
        /// </summary>
        /// <typeparam name="TEventType">The event type</typeparam>
        /// <returns>Type of <typeparamref name="TEventType"/></returns>
        protected Type GetEventType<TEventType>()
        {
            var type = typeof(TEventType);
            Vars.HandleNull(type, this.HandleNull);

            return type;
        }

        /// <summary>
        /// Default method to handle null variables
        /// </summary>
        protected void HandleNull()
        {
            throw new InternalGraphExecException();
        }

        /// <summary>
        /// Internal class to encapsulate event handler
        /// </summary>
        /// <typeparam name="TEventType">The type of event to be handled</typeparam>
        /// <typeparam name="TScope">The scope type</typeparam>
        protected class RegistryItem<TEventType, TScope> : IHandle
            where TScope : IScope<EventLevel, TScope>
        {
            internal RegistryItem(IHandle<TEventType> handler, TScope scope, bool handleOnce)
            {
                this.Handler = handler;
                this.Scope = scope;
                this.HandleOnce = handleOnce;
            }

            internal IHandle<TEventType> Handler { get; set; }

            internal TScope Scope { get; set; }

            internal bool HandleOnce { get; set; }
        }
    }
}
