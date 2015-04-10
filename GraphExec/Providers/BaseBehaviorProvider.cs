using System;

namespace GraphExec.Providers
{
    public abstract class BaseBehaviorProvider<TResult> : IBehaviorProvider<TResult>
    {
        public virtual Func<TResult> GetBehavior()
        {
            return this.Func;
        }

        protected abstract TResult Func();
    }

    public abstract class BaseBehaviorProvider<TResult, TProviderInfo> : IBehaviorProvider<TResult, TProviderInfo>
        where TProviderInfo : BaseProviderInfo, new()
    {
        public virtual Func<TResult> GetBehavior(TProviderInfo info)
        {
            return this.Func;
        }

        protected abstract TResult Func();
    }

    public abstract class BaseBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo> : IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
        where TProviderInfo : BaseProviderInfo, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
    {
        public virtual Func<TBehaviorInfo, TResult> GetBehavior(TProviderInfo info)
        {
            return this.Func;
        }

        protected abstract TResult Func(TBehaviorInfo info);
    }

}
