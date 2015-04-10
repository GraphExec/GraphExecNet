using GraphExec.NDepend;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GraphExec
{
    public static class Args
    {
        [ThrowsException]
        public static void IsTrue(Expression<Func<bool>> selector)
        {
            Args.IsNotNull(() => selector);

            var memberSelector = selector.Body as MemberExpression;
            ConstantExpression constantSelector = null;
            bool? result = null;

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
                result = (memberSelector.Member as FieldInfo).GetValue(constantSelector.Value) as bool?;
            }

            var isTrue = result != null && result.HasValue && result.Value;
            if (!isTrue)
            {
                string name = string.Empty;
                if (memberSelector == null)
                {
                    name = selector.Body.ToString();
                }
                else
                {
                    name = memberSelector.Member.Name;
                }
                throw new ArgumentException("True condition was not met.", name);
            }
        }

        [ThrowsException]
        public static void IsFalse(Expression<Func<bool>> selector)
        {
            Args.IsNotNull(() => selector);

            var memberSelector = selector.Body as MemberExpression;
            ConstantExpression constantSelector = null;
            bool? result = null;

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
                result = (memberSelector.Member as FieldInfo).GetValue(constantSelector.Value) as bool?;
            }

            var isFalse = result != null && result.HasValue && !result.Value;
            if (!isFalse)
            {
                string name = string.Empty;
                if (memberSelector == null)
                {
                    name = selector.Body.ToString();
                }
                else
                {
                    name = memberSelector.Member.Name;
                }
                throw new ArgumentException("False condition was not met.", name);
            }
        }

        [ThrowsException]
        public static void IsNotNull<TObject>(Expression<Func<TObject>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var memberSelector = selector.Body as MemberExpression;

            if (memberSelector == null)
            {
                throw new ArgumentNullException("memberSelector");
            }

            var constantSelector = memberSelector.Expression as ConstantExpression;

            if (constantSelector == null) throw new ArgumentNullException("constantSelector");

            var value = (memberSelector.Member as FieldInfo).GetValue(constantSelector.Value);

            if (value == null)
            {
                var name = memberSelector.Member.Name;
                throw new ArgumentNullException(name);
            }
        }
    }
}
