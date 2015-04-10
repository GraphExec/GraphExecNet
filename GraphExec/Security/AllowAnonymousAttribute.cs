using System;

namespace GraphExec.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class AllowAnonymousAttribute : Attribute
    {
    }
}
