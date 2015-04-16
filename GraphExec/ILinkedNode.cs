
namespace GraphExec
{
    public interface ILinkedNode : INode
    {
        INode Left { get; set; }

        INode Right { get; set; }
    }

    public interface ILinkedNode<T> : ILinkedNode, INode<T>
    {
    }

    public interface ILinkedNode<T, TNodeInfo> : ILinkedNode<T>, INode<T, TNodeInfo>
        where TNodeInfo : class, INodeInfo, new()
    {
    }

}
