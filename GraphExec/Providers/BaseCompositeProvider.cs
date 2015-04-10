using System;

namespace GraphExec.Providers
{
    public abstract class BaseCompositeProvider<TResult, TData> : ICompositeProvider<TResult, TData>
    {
        public abstract Func<TResult> GetBehavior();

        public abstract TData GetData();
    }

    public abstract class BaseCompositeProvider<TResult, TData, TProviderInfo> : ICompositeProvider<TResult, TData, TProviderInfo>
        where TProviderInfo : BaseProviderInfo, new()
    {
        public abstract Func<TResult> GetBehavior(TProviderInfo info);

        public abstract TData GetData(TProviderInfo info);
    }

    public abstract class BaseCompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo> : ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProviderInfo : BaseProviderInfo, new()
    {
        public abstract Func<TBehaviorInfo, TResult> GetBehavior(TProviderInfo info);

        public abstract TData GetData(TProviderInfo info);
    }

}
