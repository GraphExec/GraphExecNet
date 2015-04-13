
namespace GraphExec
{
    public interface IHandle
    {
        
    }

    public interface IHandle<TEventType> : IHandle
    {
        void OnHandle(TEventType evt);
    }
}
