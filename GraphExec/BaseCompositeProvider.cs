using System;

namespace GraphExec
{
    public abstract class BaseCompositeProvider<TResult, TData> : ICompositeProvider<TResult, TData>
    {
        public virtual Func<TResult> GetBehavior()
        {
            return this.Func;
        }

        protected abstract TResult Func();

        public abstract TData GetData();
    }

    public abstract class BaseCompositeProvider<TResult, TData, TProviderInfo> : ICompositeProvider<TResult, TData, TProviderInfo>
        where TProviderInfo : BaseProviderInfo, new()
    {
        public virtual Func<TResult> GetBehavior(TProviderInfo info)
        {
            return this.Func;
        }

        protected abstract TResult Func();

        public abstract TData GetData(TProviderInfo info);
    }

    public abstract class BaseCompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo> : ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo>
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProviderInfo : BaseProviderInfo, new()
    {
        public virtual Func<TBehaviorInfo, TResult> GetBehavior(TProviderInfo info)
        {
            return this.Func;
        }

        protected abstract TResult Func(TBehaviorInfo info);

        public abstract TData GetData(TProviderInfo info);
    }

}
