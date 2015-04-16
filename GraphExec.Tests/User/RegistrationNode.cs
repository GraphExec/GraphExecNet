using GraphExec.Security;

namespace GraphExec.Tests.User
{
    public sealed class RegistrationNode : BaseBehaviorNode<LoginUser, RegistrationInfo, RegistrationProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
    {
        public RegistrationNode(RegistrationInfo info)
        {
            this.Parent = null;
            this.Left = null;
            this.Right = null;

            this.PermissionCheck = new AnonymousPermissionCheck();

            this.ProviderInfo = info;
            this.Provider = new RegistrationProvider(info);
        }
    }
}
