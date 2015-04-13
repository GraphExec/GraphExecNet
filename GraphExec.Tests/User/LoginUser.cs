using GraphExec.Security;
using System.Collections.Generic;

namespace GraphExec.Tests.User
{
    public sealed class LoginUser : IUser
    {
        public LoginUser()
        {
            this.Permissions = new List<string>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<string> Permissions { get; set; }
    }
}
