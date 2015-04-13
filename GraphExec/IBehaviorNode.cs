
namespace GraphExec
{
    public interface IBehaviorNode : ILinkedNode
    {

    }

    public interface IBehaviorNode<TResult, TProvider> : IBehaviorNode, ILinkedNode<TResult>
        where TProvider : class, IBehaviorProvider<TResult>
    {
        TProvider Provider { get; set; }
    }

    public interface IBehaviorNode<TResult, TProviderInfo, TProvider> : IBehaviorNode, ILinkedNode<TResult>
        where TProviderInfo : class, IProviderInfo, new()
        where TProvider : class, IBehaviorProvider<TResult, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TProvider Provider { get; set; }
    }

    public interface IBehaviorNode<TResult, TBehaviorInfo, TProviderInfo, TProvider> : IBehaviorNode, ILinkedNode<TResult>
        where TProviderInfo : class, IProviderInfo, new()
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TProvider : class, IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
    {
        TProviderInfo ProviderInfo { get; set; }

        TBehaviorInfo BehaviorInfo { get; set; }
    }

    public interface IBehaviorNode<TResult, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider> : IBehaviorNode, ILinkedNode<TResult>
        where TProviderInfo : class, IProviderInfo, new()
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TNodeInfo : class, INodeInfo, new()
        where TProvider : class, IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
    {
        TNodeInfo Info { get; set; }

        TProviderInfo ProviderInfo { get; set; }

        TBehaviorInfo BehaviorInfo { get; set; }
    }

}
