using GraphExec.Providers;

namespace GraphExec.Security
{
    public abstract class BasePermissionCheck<TResult> : BaseBehaviorProvider<TResult>
        where TResult : PermissionCheckResult, new()
    {
        protected abstract TResult Execute();

        protected override TResult Func()
        {
            return this.Execute();
        }
    }
}
