using System;

namespace GraphExec.NDepend
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class ThrowsExceptionAttribute : Attribute
    {
    }
}
