using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseDataNode<TData, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TCheck, TCheckResult>, IDataNode<TData, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseDataProvider<TData>
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

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData();
                    this.Value = data;
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

    public abstract class BaseDataNode<TData, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TCheck, TCheckResult>, IDataNode<TData, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TProvider : BaseDataProvider<TData, TProviderInfo>
    {
        public BaseDataNode()
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

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData(this.ProviderInfo);
                    this.Value = data;
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

    public abstract class BaseDataNode<TData, TNodeInfo, TProviderInfo, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TNodeInfo, TCheck, TCheckResult>, IDataNode<TData, TNodeInfo, TProviderInfo, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProviderInfo : BaseProviderInfo, new()
        where TNodeInfo : BaseNodeInfo, new()
        where TProvider : BaseDataProvider<TData, TProviderInfo>
    {
        public BaseDataNode()
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

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData(this.ProviderInfo);
                    this.Value = data;
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

}