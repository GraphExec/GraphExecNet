
namespace GraphExec
{
    public interface IScopeManager<TScopeLevel, TScopeType>
        where TScopeType : IScope<TScopeLevel, TScopeType>
    {
        TScopeLevel CurrentLevel { get; }
        TScopeType CurrentScope { get; }
        void BeginScope(TScopeType scope);
        void EndScope(TScopeType scope);
    }
}
