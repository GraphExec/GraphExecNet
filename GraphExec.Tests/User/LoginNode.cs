using GraphExec.Security;

namespace GraphExec.Tests.User
{
    public sealed class LoginNode : BaseBehaviorNode<LoginUser, LoginInfo, LoginProvider, AnonymousPermissionCheck, AllowPermissionCheckResult>
    {
        public LoginNode(LoginInfo info)
        {
            this.Head = null;
            this.Parent = null;
            this.Child = null;

            this.PermissionCheck = new AnonymousPermissionCheck();

            this.ProviderInfo = info;
            this.Provider = new LoginProvider(info);
        }
    }
}
