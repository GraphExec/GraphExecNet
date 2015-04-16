using GraphExec.Security;

namespace GraphExec.Tests.Math
{
    public sealed class PIDataNode : BaseDataNode<double, PIProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
    {
        public PIDataNode()
        {
            this.Provider = new PIProvider();
        }
    }
}
