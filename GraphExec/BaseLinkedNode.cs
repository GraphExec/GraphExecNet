using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseLinkedNode<T, TCheck, TCheckResult> : BaseNode<T, TCheck, TCheckResult>, ILinkedNode<T>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        public INode Parent { get; set; }

        public INode Child { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var result = this.Check();

                if (result.AllowAction)
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

    public abstract class BaseLinkedNode<T, TNodeInfo, TCheck, TCheckResult> : BaseNode<T, TNodeInfo, TCheck, TCheckResult>, ILinkedNode<T, TNodeInfo>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TNodeInfo : BaseNodeInfo, new()
    {
        public INode Parent { get; set; }

        public INode Child { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var result = this.Check();

                if (result.AllowAction)
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
