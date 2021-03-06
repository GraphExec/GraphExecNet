﻿using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseCompositeNode<TResult, TData, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseCompositeProvider<TResult, TData>
    {
        public TProvider Provider { get; set; }
    }

    public abstract class BaseCompositeNode<TResult, TData, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseCompositeProvider<TResult, TData, TProviderInfo>
    {
        public BaseCompositeNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }
    }

    public abstract class BaseCompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TBehaviorInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseCompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        public BaseCompositeNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();

            this.BehaviorInfo = new TBehaviorInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }
    }

    public abstract class BaseCompositeNode<TResult, TData, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TNodeInfo, TCheck, TCheckResult>, ICompositeNode<TResult, TData, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TNodeInfo : BaseNodeInfo, new()
        where TProvider : BaseCompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
    {
        public BaseCompositeNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();

            this.BehaviorInfo = new TBehaviorInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }
    }

}