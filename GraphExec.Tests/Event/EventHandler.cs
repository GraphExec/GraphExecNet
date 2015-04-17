using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphExec.Tests.Event
{
    public class EventHandler : IHandle<EventTypeInfo>
    {
        public EventHandler(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }

        public void OnHandle(EventTypeInfo evt)
        {
            Assert.AreEqual(this.Message, evt.Message);
        }
    }
}
