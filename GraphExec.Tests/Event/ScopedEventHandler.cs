using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests.Event
{
    public class ScopedEventHandler : EventScope<EventTypeInfo>
    {
        public ScopedEventHandler(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }

        public override void OnHandle(EventTypeInfo evt)
        {
            Assert.AreEqual(this.Message, evt.Message);
        }
    }
}
