using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExec
{
    internal sealed class InternalEventAggregator : BaseEventAggregator
    {
        internal void InternalSub<TEventType>(IHandle<TEventType> handler, bool handleOnce)
        {
            Args.IsNotNull(() => handler);

            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            var type = this.GetEventType<TEventType>();

            if (!this.m_eventRegistry.ContainsKey(type))
            {
                var item = new RegistryItem<TEventType, EventScope>(handler, (handler as EventScope), handleOnce);

                this.m_eventRegistry.Add(type, new List<IHandle>()
                {
                    item
                });
            }
            else
            {
                var handlers = this.m_eventRegistry[type].Select(x => x as RegistryItem<TEventType, EventScope>).Select(x => x.Handler);
                Vars.HandleNull(handlers, this.HandleNull);

                Throw.Exception<InvalidOperationException>(() => handlers.Contains(handler));

                // Note: if we made it this far, the event type is registered correctly

                var item = new RegistryItem<TEventType, EventScope>(handler, (handler as EventScope), handleOnce);
                Vars.HandleNull(item, this.HandleNull);

                this.m_eventRegistry[type].Add(item);
            }
        }

        internal bool InternalUnsub<TEventType>()
        {
            if (!this.EnsureRegistry<TEventType>())
            {
                return false;
            }

            var type = this.GetEventType<TEventType>();

            return this.m_eventRegistry.Remove(type);
        }

        internal bool InternalUnsub<TEventType>(IHandle<TEventType> handler)
        {
            if (!this.EnsureRegistry<TEventType>())
            {
                return false;
            }

            var type = this.GetEventType<TEventType>();

            var handlers = this.GetScopedHandlers<TEventType>();
            Vars.HandleNull(handlers, this.HandleNull);

            if (!handlers.Contains(handler))
            {
                return false;
            }

            return this.RemoveRegistryItem(type, handler);
        }

        internal void InternalUnsubAll()
        {
            Throw.Exception<NullReferenceException>(() => this.m_eventRegistry == null, "EventAggregator registry is null. Please re-initialize aggregator");

            this.m_eventRegistry.Clear();
        }

        internal void InternalPub<TEventType>(TEventType evt)
        {
            if (!this.EnsureRegistry<TEventType>())
            {
                return;
            }

            var scopedSubscribers = this.GetScopedSubscribers<TEventType>().ToList();
            Vars.HandleNull(scopedSubscribers, this.HandleNull);

            if (!scopedSubscribers.Any())
            {
                return;
            }

            foreach (var subscriber in scopedSubscribers)
            {
                if (subscriber.HandleOnce)
                {
                    Throw.Exception<InternalGraphExecException>(() => !this.InternalUnsub(subscriber.Handler));

                    subscriber.Handler.OnHandle(evt);
                }
                else
                {
                    subscriber.Handler.OnHandle(evt);
                }
            }
        }
    }
}
