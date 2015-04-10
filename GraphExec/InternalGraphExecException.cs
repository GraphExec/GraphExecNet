using System;

namespace GraphExec
{
    public sealed class InternalGraphExecException : Exception
    {
        public InternalGraphExecException()
            : base("GraphExec encountered an unexpected exception") { }

        public InternalGraphExecException(string message)
            : base(message) { }

        public InternalGraphExecException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
