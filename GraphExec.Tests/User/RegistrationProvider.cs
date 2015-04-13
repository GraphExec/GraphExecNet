using GraphExec.Providers;
using System.Collections.Generic;

namespace GraphExec.Tests.User
{
    public class RegistrationProvider : BaseBehaviorProvider<LoginUser, RegistrationInfo>
    {
        public RegistrationProvider(RegistrationInfo info)
        {
            this.Info = info;
        }

        protected RegistrationInfo Info { get; set; }

        protected override LoginUser Func()
        {
            // ... hyper-secure registration implementation ...

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
