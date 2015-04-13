using System.Collections.Generic;

namespace GraphExec.Tests.User
{
    public sealed class LoginProvider : BaseBehaviorProvider<LoginUser, LoginInfo>
    {
        public LoginProvider(LoginInfo info)
        {
            this.Info = info;
        }

        private LoginInfo Info { get; set; }

        protected override LoginUser Func()
        {
            // ... hyper-secure login system implementation ...

            return new LoginUser()
            {
                Name = this.Info.UserName,
                Email = "Guest@email.com",
                Permissions = new List<string>()
                {
                    "PerformMath",
                    "PerformAdd",
                    "PerformSubtract",
                    "PerformMultiply",
                    "PerformDivide"
                }
            };
        }
    }
}
