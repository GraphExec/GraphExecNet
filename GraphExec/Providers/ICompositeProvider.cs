
namespace GraphExec.Providers
{

    public interface ICompositeProvider
    {

    }

    public interface ICompositeProvider<TResult, TData> : ICompositeProvider, IBehaviorProvider<TResult>, IDataProvider<TData>
    {

    }

    public interface ICompositeProvider<TResult, TData, TProviderInfo> : ICompositeProvider, IBehaviorProvider<TResult, TProviderInfo>, IDataProvider<TData, TProviderInfo>
        where TProviderInfo : class, IProviderInfo, new()
    {

    }

    public interface ICompositeProvider<TResult, TData, TBehaviorInfo, TProviderInfo> : ICompositeProvider, IBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>, IDataProvider<TData, TProviderInfo>
        where TBehaviorInfo : class, IBehaviorInfo, new()
        where TProviderInfo : class, IProviderInfo, new()
    {

    }

}
