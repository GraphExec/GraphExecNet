
namespace GraphExec
{
    public static class NodeExtensions
    {
        public static bool HasParent(this INode _this)
        {
            return _this.Parent != null;
        }

        public static bool HasExecuted(this INode _this)
        {
            return _this.ExecutionState == NodeExecutionState.Executed;
        }

        public static bool IsExecuting(this INode _this)
        {
            return _this.ExecutionState != NodeExecutionState.Initialized
                && _this.ExecutionState != NodeExecutionState.Executed;
        }

        public static INode<T> As<T>(this INode _this)
        {
            return _this as INode<T>;
        }

        public static INode<T, TNodeInfo> As<T, TNodeInfo>(this INode _this)
            where TNodeInfo : class, INodeInfo, new()
        {
            return _this as INode<T, TNodeInfo>;
        }

        public static bool HasValue<T>(this INode<T> _this)
        {
            return _this.Value != null;
        }

        public static bool HasInfo<T, TNodeInfo>(this INode<T, TNodeInfo> _this)
            where TNodeInfo : class, INodeInfo, new()
        {
            return _this.Info != null;
        }
    }
}
