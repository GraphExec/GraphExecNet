
namespace GraphExec
{
    public interface INode
    {
        INode Head { get; set; }
        bool ExecutionCompleted { get; }
        void Execute();
    }

    public interface INode<T> : INode
    {
        T Value { get; set; }
    }

    public interface INode<T, TNodeInfo> : INode<T>
        where TNodeInfo : class, INodeInfo, new()
    {
        TNodeInfo Info { get; set; }
    }

}
