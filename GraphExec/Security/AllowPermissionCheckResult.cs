
namespace GraphExec.Security
{
    public class AllowPermissionCheckResult : PermissionCheckResult
    {
        public AllowPermissionCheckResult()
        {
            this.AllowAction = true;
        }
    }
}
