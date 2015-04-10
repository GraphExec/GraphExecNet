using GraphExec.Security;

namespace GraphExec
{
    public abstract class BaseLinkedNode<T, TCheck, TCheckResult> : BaseNode<T, TCheck, TCheckResult>, ILinkedNode<T>
        where TCheckResult : PermissionCheckResult, new()
        where TCheck : BasePermissionCheck<TCheckResult>, new()
    {
        public INode Parent { get; set; }

        public INode Child { get; set; }

        public override void Execute()
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

                    if (this.Parent != null)
                    {
                        this.Parent.Execute();
                    }

                    if (this.Child != null)
                    {
                        this.Child.Execute();
                    }

                    this.ExecutionCompleted = true;
                }
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

        public override void Execute()
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

                    if (this.Parent != null)
                    {
                        this.Parent.Execute();
                    }

                    if (this.Child != null)
                    {
                        this.Child.Execute();
                    }

                    this.ExecutionCompleted = true;
                }
            }
        }
    }


}
