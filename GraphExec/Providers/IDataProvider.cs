
namespace GraphExec.Providers
{

    public interface IDataProvider : IProvider
    {

    }

    public interface IDataProvider<TData> : IDataProvider
    {
        TData GetData();
    }

    public interface IDataProvider<TData, TProviderInfo> : IDataProvider
        where TProviderInfo : class, IProviderInfo, new()
    {
        TData GetData(TProviderInfo info);
    }

}
