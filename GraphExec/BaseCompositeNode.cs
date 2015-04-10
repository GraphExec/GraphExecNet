using GraphExec.Providers;
using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseCompositeNode<TResult, TData, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TProvider, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseCompositeProvider<TResult, TData>
    {
        public TProvider Provider { get; set; }
    }

    public abstract class BaseCompositeNode<TResult, TData, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TProvider, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseCompositeProvider<TResult, TData, TProviderInfo>
    {
        public TProvider Provider { get; set; }
    }

    public abstract class BaseCompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TProvider, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseCompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        public TProvider Provider { get; set; }
    }

}