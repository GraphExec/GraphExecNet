using System;

namespace GraphExec.Tests.Math
{
    public class OperationProvider<TOperationInfo> : BaseBehaviorProvider<double, OperandInfo, TOperationInfo>
        where TOperationInfo : OperationInfo, new()
    {
        private TOperationInfo Info { get; set; }

        public override Func<OperandInfo, double> GetBehavior(TOperationInfo info)
        {
            this.Info = info;

            return this.Func;
        }

        protected override double Func(OperandInfo info)
        {
            var result = 0d;

            switch (this.Info.Operation)
            {
                case Operations.Add:
                    {
                        result = info.LHS + info.RHS;
                        break;
                    }
                case Operations.Subtract:
                    {
                        result = info.LHS - info.RHS;
                        break;
                    }
                case Operations.Multiply:
                    {
                        result = info.LHS * info.RHS;
                        break;
                    }
                case Operations.Divide:
                    {
                        result = info.LHS / info.RHS;
                        break;
                    }
                case Operations.Power:
                    {
                        result = System.Math.Pow(info.LHS, info.RHS);
                        break;
                    }
            }

            return result;
        }
    }
}
