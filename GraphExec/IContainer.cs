
namespace GraphExec
{
    internal interface IContainer
    {
        T Resolve<T>();
    }
}
