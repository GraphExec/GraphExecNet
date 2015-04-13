using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseDataNode<TData, TProvider, TCheck, TCheckResult> : BaseLinkedNode<TData, TCheck, TCheckResult>, IDataNode<TData, TProvider>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TProvider : BaseDataProvider<TData>
    {
        public TProvider Provider { get; set; }

        public override void Execute()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    if (this.Head != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingHead);

                        this.Head.Execute();
                    }

                    if (this.Parent != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingParent);

                        this.Parent.Execute();
                    }

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData();
                    this.Value = data;

                    if (this.Child != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingChild);

                        this.Child.Execute();
                    }
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
        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public override void Execute()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    if (this.Head != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingHead);

                        this.Head.Execute();
                    }

                    if (this.Parent != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingParent);

                        this.Parent.Execute();
                    }

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData(this.ProviderInfo);
                    this.Value = data;

                    if (this.Child != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingChild);

                        this.Child.Execute();
                    }
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
        public TProviderInfo ProviderInfo { get; set; }

        public TProvider Provider { get; set; }

        public override void Execute()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    if (this.Head != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingHead);

                        this.Head.Execute();
                    }

                    if (this.Parent != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingParent);

                        this.Parent.Execute();
                    }

                    this.EventAggregator.Pub(NodeExecutionState.ExecutingGetData);

                    var data = this.Provider.GetData(this.ProviderInfo);
                    this.Value = data;

                    if (this.Child != null)
                    {
                        this.EventAggregator.Pub(NodeExecutionState.ExecutingChild);

                        this.Child.Execute();
                    }
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

}