
namespace GraphExec
{
    internal static class IoC
    {
        static IoC()
        {
            IoC.Container = ContainerSetup.Setup();
        }

        internal static IContainer Container { get; private set; }
    }
}
