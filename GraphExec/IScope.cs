
namespace GraphExec
{
    public interface IScope<TScopeLevel, TScopeType>
    {
        TScopeType Scope { get; set; }

        TScopeLevel ScopeLevel { get; set; }
    }
}
