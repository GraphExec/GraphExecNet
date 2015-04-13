using System.Linq.Expressions;
using System.Reflection;

namespace GraphExec
{
    internal sealed class ExpressionProcessResult<T>
    {
        internal MemberExpression Member { get; set; }
        internal ConstantExpression Constant { get; set; }
        internal FieldInfo Field { get; set; }
        internal string Description { get; set; }
        internal T Value { get; set; }
    }
}
