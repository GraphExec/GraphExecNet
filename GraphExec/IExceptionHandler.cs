using System;

namespace GraphExec
{
    public interface IExceptionHandler
    {
    }

    public interface IExceptionHandler<TException> : IExceptionHandler
        where TException : Exception
    {
        void Handle(TException exception);
    }

    public interface IExceptionHandler<TException, THandleInfo> : IExceptionHandler
        where TException : Exception
        where THandleInfo : class
    {
        void Handle(TException exception, THandleInfo info);
    }

    public interface IExceptionHandler<TException, THandleInfo, TResult> : IExceptionHandler
        where TException : Exception
        where THandleInfo : class
    {
        TResult Handle(TException exception, THandleInfo info);
    }
}
