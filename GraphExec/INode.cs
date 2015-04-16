
namespace GraphExec
{
    public interface INode
    {
        INode Parent { get; set; }
        NodeExecutionState ExecutionState { get; }
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
