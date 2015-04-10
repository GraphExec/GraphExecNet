using System;

namespace GraphExec
{
    public abstract class BaseExceptionHandler<TException> : IExceptionHandler<TException>
        where TException : Exception
    {
        public abstract void Handle(TException exception);
    }

    public abstract class BaseExceptionHandler<TException, THandleInfo> : IExceptionHandler<TException, THandleInfo>
        where TException : Exception
        where THandleInfo : class
    {
        public abstract void Handle(TException exception, THandleInfo info);
    }

    public abstract class BaseExceptionHandler<TException, THandleInfo, TResult> : IExceptionHandler<TException, THandleInfo, TResult>
        where TException : Exception
        where THandleInfo : class
    {
        public abstract TResult Handle(TException exception, THandleInfo info);
    }

}
