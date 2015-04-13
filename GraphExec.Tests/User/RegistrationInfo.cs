using GraphExec.Providers;

namespace GraphExec.Tests.User
{
    public sealed class RegistrationInfo : BaseProviderInfo
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Access { get; set; }
    }
}
