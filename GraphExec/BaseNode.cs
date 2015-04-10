using Autofac;
using GraphExec.NDepend;
using GraphExec.Security;
using System;

namespace GraphExec
{
    public abstract class BaseNode<T, TCheck, TCheckResult> : INode<T>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        private bool m_executionCompleted;
        public bool ExecutionCompleted
        {
            [ThrowsException]
            get
            {
                return this.m_executionCompleted;
            }
            protected set
            {
                this.m_executionCompleted = value;
            }
        }

        public T Value { get; set; }

        public INode Head { get; set; }

        public TCheck PermissionCheck { get; set; }

        public string Name
        {
            [ThrowsException]
            get
            {
                return this.GetType().Name;
            }
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
            Throw.Exception<NullReferenceException>(() => this.PermissionCheck == null);

            var result = this.SetCheckResult();

            this.SetSecurity(result);

            if (result != null || !result.AllowAction)
            {
                this.ExecutionCompleted = false;
            }

            return result;
        }

        [ThrowsException]
        public virtual void Execute()
        {
            if (!this.ExecutionCompleted)
            {
                var result = this.Check();

                if (result.AllowAction)
                {
                    if (this.Head != null)
                    {
                        this.Head.Execute();
                    }

                    this.ExecutionCompleted = true;
                }
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
