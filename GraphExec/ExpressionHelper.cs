using GraphExec.NDepend;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GraphExec
{
    internal static class ExpressionHelper
    {
        [AllowNullVars]
        internal static ExpressionProcessResult<object> Process<T>(Expression<Func<T>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");

            var memberSelector = selector.Body as MemberExpression;
            if (memberSelector == null) throw new ArgumentNullException("memberSelector");

            var constantSelector = memberSelector.Expression as ConstantExpression;
            if (constantSelector == null) throw new ArgumentNullException("constantSelector");

            var field = memberSelector.Member as FieldInfo;
            var value = field.GetValue(constantSelector.Value);

            return new ExpressionProcessResult<object>()
            {
                Member = memberSelector,
                Constant = constantSelector,
                Field = field,
                Description = memberSelector.Member.Name,
                Value = value
            };
        }

        [AllowNullVars]
        internal static ExpressionProcessResult<bool> Process(Expression<Func<bool>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");

            var memberSelector = selector.Body as MemberExpression;
            ConstantExpression constantSelector = null;
            FieldInfo field = null;
            bool? result = null;
            var name = string.Empty;

            if (memberSelector == null)
            {
                if (selector.Body is ConstantExpression)
                {
                    constantSelector = selector.Body as ConstantExpression;
                    result = constantSelector.Value as bool?;
                }
                else
                {
                    var func = selector.Compile();
                    result = func.Invoke() as bool?;
                }
            }
            else
            {
                constantSelector = memberSelector.Expression as ConstantExpression;
                field = memberSelector.Member as FieldInfo;
                result = field.GetValue(constantSelector.Value) as bool?;
            }

            if (memberSelector == null)
            {
                name = selector.Body.ToString();
            }
            else
            {
                name = memberSelector.Member.Name;
            }

            return new ExpressionProcessResult<bool>()
            {
                Member = memberSelector,
                Constant = constantSelector,
                Field = field,
                Description = name,
                Value = result != null && result.HasValue && result.Value
            };
        }

        internal static void HandleNull()
        {
            throw new InternalGraphExecException();
        }
    }
}
