using GraphExec.Security;

namespace GraphExec.Tests.Math
{
    public class BasicMathNode<TProvider, TOperationInfo> : BaseBehaviorNode<double, OperandInfo, TOperationInfo, TProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
        where TOperationInfo : OperationInfo, new()
        where TProvider : OperationProvider<TOperationInfo>, new()
    {
        public BasicMathNode(OperandInfo info)
        {
            this.BehaviorInfo = info;
            this.ProviderInfo = new TOperationInfo();
            this.Info = info;

            this.Provider = new TProvider();

            this.PermissionCheck = new AnonymousPermissionCheck();
        }

        protected override void ExecuteCore()
        {
            if (this.ExecutionState == NodeExecutionState.Initialized)
            {
                var checkResult = this.Check();

                if (checkResult.AllowAction)
                {
                    if (this.Parent != null)
                    {
                        this.Parent.Execute();

                        if (this.Right != null)
                        {
                            var right = (this.Right as INode<double, OperandInfo>);
                            right.Info.LHS = (this.Parent as INode<double>).Value;
                            right.Execute();
                            this.Info.RHS = right.Value;
                        }

                        if (this.Left != null)
                        {
                            var left = (this.Left as INode<double, OperandInfo>);
                            left.Execute();
                            this.Info.LHS = left.Value;
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