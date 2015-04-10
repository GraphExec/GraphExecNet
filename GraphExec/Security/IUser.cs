using System.Collections.Generic;

namespace GraphExec.Security
{
    public interface IUser
    {
        string Name { get; set; }

        string Email { get; set; }

        List<string> Permissions { get; set; }
    }
}
