using GraphExec.Security;

namespace GraphExec
{
    /// <summary>
    /// Simple base behavior node allowing secure retrieval of a processing result.
    /// </summary>
    /// <typeparam name="TResult">The processing result.</typeparam>
    /// <typeparam name="TProvider">The provider of the process.</typeparam>
    /// <typeparam name="TCheck">The provider of the permission checking process.</typeparam>
    /// <typeparam name="TCheckResult">The processed permission checking processing result.</typeparam>
    public abstract class BaseBehaviorNode<TResult, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TCheck, TCheckResult>, IBehaviorNode<TResult, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseBehaviorProvider<TResult>
    {
        public TProvider Provider { get; set; }

        public override void Execute()
        {
            base.Execute();

            this.ExecutionCompleted = false;

            var func = this.Provider.GetBehavior();

            if (func != null)
            {
                var result = func();

                this.Value = result;
            }
        }
    }

    /// <summary>
    /// Base behavior node allowing secure retrieval of a processing result given specific parameters
    /// </summary>
    /// <typeparam name="TResult">The processing result.</typeparam>
    /// <typeparam name="TProviderInfo">The specific parameters sent to the process provider.</typeparam>
    /// <typeparam name="TProvider">The provider of the process.</typeparam>
    /// <typeparam name="TCheck">The provider of the permission checking process.</typeparam>
    /// <typeparam name="TCheckResult">The processed permission checking processing result.</typeparam>
    public abstract class BaseBehaviorNode<TResult, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TCheck, TCheckResult>, IBehaviorNode<TResult, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseBehaviorProvider<TResult, TProviderInfo>
    {
        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public override void Execute()
        {
            base.Execute();

            this.ExecutionCompleted = false;

            var func = this.Provider.GetBehavior(this.ProviderInfo);

            if (func != null)
            {
                var result = func();

                this.Value = result;
            }
        }
    }

    public abstract class BaseBehaviorNode<TResult, TBehaviorInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TBehaviorInfo, TCheck, TCheckResult>, IBehaviorNode<TResult, TBehaviorInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TProvider : BaseBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
    {
        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }

        public override void Execute()
        {
            base.Execute();

            this.ExecutionCompleted = false;

            var func = this.Provider.GetBehavior(this.ProviderInfo);

            if (func != null)
            {
                var result = func(this.BehaviorInfo);

                this.Value = result;
            }
        }
    }

    public abstract class BaseBehaviorNode<TResult, TNodeInfo, TBehaviorInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TResult, TNodeInfo, TCheck, TCheckResult>, IBehaviorNode<TResult, TBehaviorInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TBehaviorInfo : BaseBehaviorInfo, new()
        where TNodeInfo : BaseNodeInfo, new()
        where TProvider : BaseBehaviorProvider<TResult, TBehaviorInfo, TProviderInfo>
    {
        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }

        public override void Execute()
        {
            base.Execute();

            this.ExecutionCompleted = false;

            var func = this.Provider.GetBehavior(this.ProviderInfo);

            if (func != null)
            {
                var result = func(this.BehaviorInfo);

                this.Value = result;
            }
        }
    }

}