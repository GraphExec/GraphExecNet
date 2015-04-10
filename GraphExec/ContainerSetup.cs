using Autofac;
using GraphExec.Security;

namespace GraphExec
{
    internal class ContainerSetup
    {
        internal IContainer Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance<SecurityCore>(new SecurityCore());

            return builder.Build();
        }
    }
}
