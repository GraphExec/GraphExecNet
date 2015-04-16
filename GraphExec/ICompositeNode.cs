
namespace GraphExec
{
    public interface ICompositeNode : IBehaviorNode, IDataNode
    {
    }

    public interface ICompositeNode<TResult, TData, TProvider> : ICompositeNode, ILinkedNode<TResult>
        where TProvider : ICompositeProvider<TResult, TData>
    {
        TProvider Provider { get; set; }
    }

    public interface ICompositeNode<TResult, TData, TProviderInfo, TProvider> : ICompositeNode, ILinkedNode<TResult>
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : ICompositeProvider<TResult, TData, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }
    }

    public interface ICompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider> : ICompositeNode, ILinkedNode<TResult>
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }

        TBehaviorInfo BehaviorInfo { get; set; }
    }

    public interface ICompositeNode<TResult, TData, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider> : ICompositeNode, ILinkedNode<TResult, TNodeInfo>
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TProviderInfo : class, IProviderInfo, new()
        where TNodeInfo : class, INodeInfo, new()
        where TProvider : ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }

        TBehaviorInfo BehaviorInfo { get; set; }
    }

}
