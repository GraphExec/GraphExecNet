using System;

namespace GraphExec.Providers
{

    public interface IBehaviorProvider : IProvider
    {

    }

    public interface IBehaviorProvider<TResult> : IBehaviorProvider
    {
        Func<TResult> GetBehavior();
    }

    public interface IBehaviorProvider<TResult, TProviderInfo> : IBehaviorProvider
        where TProviderInfo : class, IProviderInfo, new()
    {
        Func<TResult> GetBehavior(TProviderInfo info);
    }

    public interface IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo> : IBehaviorProvider
        where TProviderInfo : class, IProviderInfo, new()
        where TBehaviorInfo : class, IBehaviorInfo, new()
    {
        Func<TBehaviorInfo, TResult> GetBehavior(TProviderInfo info);
    }

}
