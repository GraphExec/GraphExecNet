using GraphExec.Security;

namespace GraphExec.Tests.Math
{
    public class BasicGeometryNode<TProvider, TOperationInfo> : BaseBehaviorNode<double, OperandInfo, TOperationInfo, TProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
        where TOperationInfo : OperationInfo, new()
        where TProvider : OperationProvider<TOperationInfo>, new()
    {
        public BasicGeometryNode(OperandInfo info)
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
                    // Nodes [ [ [PI] * [d^2] ] / 4 ]
                    // parent = d^2
                    // left = PI
                    // right = left * parent
                    // formula = right / 4

                    if (this.HasLeft() && this.HasParent())
                    {
                        this.Parent.Execute();
                        this.Left.Execute();
                    }

                    if (this.HasLeft() && !this.HasParent() && !this.HasRight())
                    {
                        var left = this.Left.As<double>();
                        left.Execute();

                        this.Info.LHS = left.Value;
                    }

                    if (this.HasRight() && this.HasLeft() && this.HasParent())
                    {
                        var right = this.Right.As<double, OperandInfo>();
                        right.Info.LHS = this.Left.As<double>().Value;
                        right.Info.RHS = this.Parent.As<double>().Value;
                        right.Execute();
                        this.Info.LHS = right.Value;
                    }

                    if (this.HasProvider())
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
