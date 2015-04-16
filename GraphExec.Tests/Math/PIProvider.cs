
namespace GraphExec.Tests.Math
{
    public sealed class PIProvider : BaseDataProvider<double>
    {
        public override double GetData()
        {
            return System.Math.PI;
        }
    }
}
