
namespace GraphExec
{
    public abstract class BaseDataProvider<TData> : IDataProvider<TData>
    {
        public abstract TData GetData();
    }

    public abstract class BaseDataProvider<TData, TProviderInfo> : IDataProvider<TData, TProviderInfo>
        where TProviderInfo : BaseProviderInfo, new()
    {
        public abstract TData GetData(TProviderInfo info);
    }
}
