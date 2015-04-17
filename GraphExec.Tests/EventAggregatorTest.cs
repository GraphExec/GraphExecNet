using GraphExec.Tests.Event;
using GraphExec.Tests.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests
{
    [TestClass]
    public class EventAggregatorTest : BaseTestClass
    {
        public EventAggregator Aggregator { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            this.Aggregator = new EventAggregator();
        }

        [TestMethod]
        public void EventAggregator_Constructor()
        {
            Assert.IsNotNull(this.Aggregator.Scope);
            Assert.IsInstanceOfType(this.Aggregator.Scope, typeof(EventAggregator));
        }

        [TestMethod]
        public void EventAggregator_Sub_NullHandler()
        {
            this.ExpectException<System.ArgumentNullException>(() =>
            {
                EventHandler handler = null;

                this.Aggregator.Sub(handler);
            });
        }

        [TestMethod]
        public void EventAggregator_Sub_Handler()
        {
            var msg = "EventAggregator_Sub_Handler";
            var handler = new EventHandler(msg);

            this.Aggregator.Sub(handler);

            this.Aggregator.Pub(new EventTypeInfo(msg));
        }

        [TestMethod]
        public void EventAggregator_Sub_HandleOnce()
        {
            var msg = "EventAggregator_Sub_Handler";
            var handler = new EventHandler(msg);

            this.Aggregator.Sub(handler, true);

            this.Aggregator.Pub(new EventTypeInfo(msg));

            var result = this.Aggregator.Unsub(handler);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EventAggregator_Unsub_Handler()
        {
            var msg = "EventAggregator_Unsub_Handler";
            var handler = new EventHandler(msg);

            this.Aggregator.Sub(handler);

            this.Aggregator.Unsub(handler);
        }

        [TestMethod]
        public void EventAggregator_Unsub_HandlerNotRegistered()
        {
            var msg = "EventAggregator_Unsub_Handler";
            var handler = new EventHandler(msg);

            var result = this.Aggregator.Unsub(handler);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EventAggregator_Unsub_NoHandle()
        {
            var msg = "EventAggregator_Unsub_NoHandle";
            var handler = new EventHandler(msg);

            this.Aggregator.Sub(handler);

            var result = this.Aggregator.Unsub<EventTypeInfo>();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EventAggregator_Unsub_All()
        {
            var msg = "EventAggregator_Unsub_All";
            var handler = new EventHandler(msg);

            this.Aggregator.Sub(handler);

            this.Aggregator.UnsubAll();
        }
    }
}
