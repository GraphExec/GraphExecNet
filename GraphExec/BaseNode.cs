using GraphExec.NDepend;
using GraphExec.Security;
using System;

namespace GraphExec
{
    public abstract class BaseNode<T, TCheck, TCheckResult> : EventScope<NodeExecutionState>, INode<T>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        protected BaseNode()
        {
            this.InitializeScope();

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

        public IEventScopeManager ScopeManager
        {
            get
            {
                return (this.EventAggregator as EventScopeManager);
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

        public override void OnHandle(NodeExecutionState evt)
        {
            this.ExecutionState = evt;
        }

        private void InitializeScope()
        {
            this.ScopeLevel = EventLevel.Local;
            this.ScopeManager.BeginScope(this);
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

        public void Execute()
        {
            this.InitializeScope();
            this.ExecuteCore();
            this.ScopeManager.EndScope(this);
        }

        [ThrowsException]
        protected virtual void ExecuteCore()
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
