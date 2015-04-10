using System.Collections.Generic;

namespace GraphExec.Security
{
    public class PermissionCheckResult
    {
        public bool AllowAction { get; set; }

        public List<string> AllowedPermissions { get; set; }
    }
}
