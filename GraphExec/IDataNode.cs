
namespace GraphExec
{
    public interface IDataNode : ILinkedNode
    {

    }

    public interface IDataNode<TData, TProvider> : IDataNode, ILinkedNode<TData>
        where TProvider : class, IDataProvider<TData>
    {
        TProvider Provider { get; set; }
    }

    public interface IDataNode<TData, TProviderInfo, TProvider> : IDataNode, ILinkedNode<TData>
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : class, IDataProvider<TData, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }
    }

    public interface IDataNode<TData, TNodeInfo, TProviderInfo, TProvider> : IDataNode, ILinkedNode<TData, TNodeInfo>
        where TProviderInfo : class, IProviderInfo, new()
        where TNodeInfo : class, INodeInfo, new()
        where TProvider : class, IDataProvider<TData, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }
    }
}
