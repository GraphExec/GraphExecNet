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

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetBehavior);

                    var func = this.Provider.GetBehavior();

                    if (func != null)
                    {
                        var result = func();

                        this.Value = result;
                    }
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
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
        public BaseBehaviorNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetBehavior);

                    var func = this.Provider.GetBehavior(this.ProviderInfo);

                    if (func != null)
                    {
                        var result = func();

                        this.Value = result;
                    }
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
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
        public BaseBehaviorNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();

            this.BehaviorInfo = new TBehaviorInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetBehavior);

                    var func = this.Provider.GetBehavior(this.ProviderInfo);

                    if (func != null)
                    {
                        var result = func(this.BehaviorInfo);

                        this.Value = result;
                    }
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
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
        public BaseBehaviorNode()
            : base()
        {
            this.ProviderInfo = new TProviderInfo();

            this.BehaviorInfo = new TBehaviorInfo();
        }

        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public TBehaviorInfo BehaviorInfo { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetBehavior);

                    var func = this.Provider.GetBehavior(this.ProviderInfo);

                    if (func != null)
                    {
                        var result = func(this.BehaviorInfo);

                        this.Value = result;
                    }
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

}