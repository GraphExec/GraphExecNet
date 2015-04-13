using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseDataNode<TData, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TCheck, TCheckResult>, IDataNode<TData, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseDataProvider<TData>
    {
        public TProvider Provider { get; set; }
    }

    public abstract class BaseDataNode<TData, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TCheck, TCheckResult>, IDataNode<TData, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseDataProvider<TData, TProviderInfo>
    {
        public TProvider Provider { get; set; }
    }

    public abstract class BaseDataNode<TData, TNodeInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TNodeInfo, TCheck, TCheckResult>, IDataNode<TData, TNodeInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TNodeInfo : BaseNodeInfo, new()
        where TProvider : BaseDataProvider<TData, TProviderInfo>
    {
        public TProvider Provider { get; set; }
    }

}