
namespace GraphExec.Tests.Event
{
    public sealed class EventTypeInfo
    {
        public EventTypeInfo(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
