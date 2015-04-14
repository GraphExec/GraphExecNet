using System;
using System.Linq.Expressions;

namespace GraphExec
{
    public static class Throw
    {
        public static void Exception<TException>(Expression<Func<bool>> predicate, string message = "An error condition was met: {0}")
            where TException : Exception, new()
        {
            Args.IsNotNull(() => predicate);

            var expressionResult = ExpressionHelper.Process(predicate);
            Vars.HandleNull(expressionResult, ExpressionHelper.HandleNull);

            if (expressionResult.Value)
            {
                var msg = string.Format(message ?? "An error condition was met: {0}", expressionResult.Description);
                var ex = (TException)Activator.CreateInstance(typeof(TException), new object[] { msg });

                throw ex;
            }
        }
    }
}
