using GraphExec.NDepend;
using System;
using System.Linq.Expressions;

namespace GraphExec
{
    public static class Args
    {
        [ThrowsException]
        public static void IsTrue(Expression<Func<bool>> selector)
        {
            var expressionResult = ExpressionHelper.Process(selector);
            Vars.HandleNull(expressionResult, ExpressionHelper.HandleNull);

            if (!expressionResult.Value)
            {
                throw new ArgumentException("True condition was not met.", expressionResult.Description);
            }
        }

        [ThrowsException]
        public static void IsFalse(Expression<Func<bool>> selector)
        {
            var expressionResult = ExpressionHelper.Process(selector);
            Vars.HandleNull(expressionResult, ExpressionHelper.HandleNull);

            if (expressionResult.Value)
            {
                throw new ArgumentException("False condition was not met.", expressionResult.Description);
            }
        }

        [ThrowsException, AllowNullVars]
        public static void IsNotNull<TObject>(Expression<Func<TObject>> selector)
        {
            var expressionResult = ExpressionHelper.Process(selector);

            if (expressionResult == null)
            {
                ExpressionHelper.HandleNull();
            }

            if (expressionResult.Value == null)
            {
                throw new ArgumentNullException(expressionResult.Description);
            }
        }
    }
}
