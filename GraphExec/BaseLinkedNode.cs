using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseLinkedNode<T, TCheck, TCheckResult> : BaseNode<T, TCheck, TCheckResult>, ILinkedNode<T>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        public INode Left { get; set; }

        public INode Right { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var result = this.Check();

                if (result.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }

        protected virtual void ExecuteLeft()
        {
            if (this.Left != null)
            {
                this.EventAggregator.Pub(NodeExecutionState.ExecutingLeft);

                this.Left.Execute();
            }
        }

        protected virtual void ExecuteRight()
        {
            if (this.Right != null)
            {
                this.EventAggregator.Pub(NodeExecutionState.ExecutingRight);

                this.Right.Execute();
            }
        }
    }

    public abstract class BaseLinkedNode<T, TNodeInfo, TCheck, TCheckResult> : BaseNode<T, TNodeInfo, TCheck, TCheckResult>, ILinkedNode<T, TNodeInfo>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TNodeInfo : BaseNodeInfo, new()
    {
        public INode Left { get; set; }

        public INode Right { get; set; }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                this.EventAggregator.Pub(NodeExecutionState.Executing);

                var result = this.Check();

                if (result.AllowAction)
                {
                    this.ExecuteParent();

                    this.ExecuteLeft();

                    this.ExecuteRight();
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }

        protected virtual void ExecuteLeft()
        {
            if (this.Left != null)
            {
                this.EventAggregator.Pub(NodeExecutionState.ExecutingLeft);

                this.Left.Execute();
            }
        }

        protected virtual void ExecuteRight()
        {
            if (this.Right != null)
            {
                this.EventAggregator.Pub(NodeExecutionState.ExecutingRight);

                this.Right.Execute();
            }
        }
    }


}
