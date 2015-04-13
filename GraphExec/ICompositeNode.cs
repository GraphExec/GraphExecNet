
namespace GraphExec
{
    public interface ICompositeNode : IBehaviorNode, IDataNode
    {
    }

    public interface ICompositeNode<TResult, TData, TProvider> : ICompositeNode, ILinkedNode<TProvider>
        where TProvider : ICompositeProvider<TResult, TData>
    {
        TProvider Provider { get; set; }
    }

    public interface ICompositeNode<TResult, TData, TProviderInfo, TProvider> : ICompositeNode, ILinkedNode<TProvider>
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : ICompositeProvider<TResult, TData, TProviderInfo>
    {
        TProvider Provider { get; set; }
    }

    public interface ICompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider> : ICompositeNode, ILinkedNode<TProvider>
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        TProvider Provider { get; set; }
    }

}
