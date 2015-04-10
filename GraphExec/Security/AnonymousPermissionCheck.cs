using System.Collections.Generic;

namespace GraphExec.Security
{
    public class AnonymousPermissionCheck : BasePermissionCheck<AllowPermissionCheckResult>
    {
        protected override AllowPermissionCheckResult Execute()
        {
            return new AllowPermissionCheckResult()
            {
                AllowAction = true,
                AllowedPermissions = new List<string>()
            };
        }
    }
}
