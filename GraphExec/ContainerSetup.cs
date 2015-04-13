using GraphExec.Security;

namespace GraphExec
{
    internal static class ContainerSetup
    {
        internal static IContainer Setup()
        {
            var builder = new DependencyBuilder();
            builder.Register<SecurityCore>(new SecurityCore());
            builder.Register<IEventAggregator>(new EventAggregator());

            return builder.Build();
        }
    }
}
