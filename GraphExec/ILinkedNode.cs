
namespace GraphExec
{
    public interface ILinkedNode : INode
    {
        INode Parent { get; set; }
        INode Child { get; set; }
    }

    public interface ILinkedNode<T> : ILinkedNode
    {
        T Value { get; set; }
    }

    public interface ILinkedNode<T, TNodeInfo> : ILinkedNode<T>
        where TNodeInfo : class, INodeInfo, new()
    {
        TNodeInfo Info { get; set; }
    }

}
