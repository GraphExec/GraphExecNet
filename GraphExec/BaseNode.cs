using GraphExec.NDepend;
using GraphExec.Security;
using System;

namespace GraphExec
{
    public abstract class BaseNode<T, TCheck, TCheckResult> : INode<T>, IHandle<NodeExecutionState>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        protected BaseNode()
        {
            this.EventAggregator.Sub(this);

            this.EventAggregator.Pub(NodeExecutionState.Initialized);
        }

        public NodeExecutionState ExecutionState { get; private set; }

        public T Value { get; set; }

        public INode Head { get; set; }

        public TCheck PermissionCheck { get; set; }

        public IEventAggregator EventAggregator
        {
            get
            {
                return IoC.Container.Resolve<IEventAggregator>();
            }
        }

        public string Name
        {
            [ThrowsException]
            get
            {
                return this.GetType().Name;
            }
        }

        public void OnHandle(NodeExecutionState evt)
        {
            this.ExecutionState = evt;
        }

        [ThrowsException]
        private void SetSecurity(TCheckResult result)
        {
            var security = IoC.Container.Resolve<SecurityCore>();
            if (!security.Check(this.Name))
            {
                security.RecordCheck(this.Name, result);
            }
        }

        [ThrowsException]
        private TCheckResult SetCheckResult()
        {
            var check = this.PermissionCheck.GetBehavior();

            TCheckResult result = null;

            if (check != null)
            {
                result = check();
            }

            return result;
        }

        [ThrowsException]
        public TCheckResult Check()
        {
            this.EventAggregator.Pub(NodeExecutionState.CheckingSecurity);

            Throw.Exception<NullReferenceException>(() => this.PermissionCheck == null);

            var result = this.SetCheckResult();

            this.SetSecurity(result);

            if (result == null || !result.AllowAction)
            {
                this.EventAggregator.Pub(NodeExecutionState.SecurityFailed);
            }
            else
            {
                this.EventAggregator.Pub(NodeExecutionState.SecuritySuccessful);
            }

            return result;
        }

        [ThrowsException]
        public virtual void Execute()
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
                }

                this.EventAggregator.Pub(NodeExecutionState.Executed);
            }
        }
    }

    public abstract class BaseNode<T, TNodeInfo, TCheck, TCheckResult> : BaseNode<T, TCheck, TCheckResult>, INode<T, TNodeInfo>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
        where TNodeInfo : BaseNodeInfo, new()
    {
        public TNodeInfo Info { get; set; }
    }

}
