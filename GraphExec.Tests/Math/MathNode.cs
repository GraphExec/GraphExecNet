using GraphExec.Security;

namespace GraphExec.Tests.Math
{
    public class MathNode<TProvider, TOperationInfo> : BaseBehaviorNode<double, OperandInfo, TOperationInfo, TProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
        where TOperationInfo : OperationInfo, new()
        where TProvider : OperationProvider<TOperationInfo>, new()
    {
        public MathNode(OperandInfo info)
        {
            this.BehaviorInfo = info;
            this.ProviderInfo = new TOperationInfo();
            this.Info = info;

            this.Provider = new TProvider();

            this.PermissionCheck = new AnonymousPermissionCheck();
        }

        public override void Execute()
        {
            if (!this.ExecutionCompleted)
            {
                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    if (this.Head != null)
                    {
                        this.Head.Execute();

                        if (this.Parent != null)
                        {
                            var parent = (this.Parent as INode<double, OperandInfo>);
                            parent.Info.LHS = (this.Head as INode<double>).Value;
                            parent.Execute();
                            this.Info.RHS = parent.Value;
                        }

                        if (this.Child != null)
                        {
                            var child = (this.Child as INode<double, OperandInfo>);
                            child.Execute();
                            this.Info.LHS = child.Value;
                        }
                    }

                    if (this.Provider != null)
                    {
                        this.BehaviorInfo = this.Info;

                        var operation = this.Provider.GetBehavior(this.ProviderInfo);
                        this.Value = operation(this.BehaviorInfo);
                    }
                }
            }
        }
    }
}