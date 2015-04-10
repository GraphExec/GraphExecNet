using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GraphExec
{
    public static class Throw
    {
        public static void Exception<TException>(Expression<Func<bool>> predicate, string message = "An error condition was met: {0}")
            where TException : Exception, new()
        {
            Args.IsNotNull(() => predicate);

            var memberSelector = predicate.Body as MemberExpression;
            ConstantExpression constantSelector = null;
            bool? result = null;

            if (memberSelector == null)
            {
                if (predicate.Body is ConstantExpression)
                {
                    constantSelector = predicate.Body as ConstantExpression;
                    result = constantSelector.Value as bool?;
                }
                else
                {
                    var func = predicate.Compile();
                    result = func.Invoke() as bool?;
                }
            }
            else
            {
                constantSelector = memberSelector.Expression as ConstantExpression;
                result = (memberSelector.Member as FieldInfo).GetValue(constantSelector.Value) as bool?;
            }

            var isTrue = result != null && result.HasValue && result.Value;
            if (isTrue)
            {
                string name = string.Empty;
                if (memberSelector == null)
                {
                    name = predicate.Body.ToString();
                }
                else
                {
                    name = memberSelector.Member.Name;
                }

                var instance = Activator.CreateInstance(typeof(TException), new object[] { message ?? "An error condition was met: {0}", name });

                var exception = (TException)instance;

                throw exception;
            }
        }
    }
}
