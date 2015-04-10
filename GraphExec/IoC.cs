using Autofac;

namespace GraphExec
{
    internal static class IoC
    {
        internal static IContainer Container = (new ContainerSetup()).Setup();
    }
}
