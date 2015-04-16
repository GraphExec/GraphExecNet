
namespace GraphExec
{
    /// <summary>
    /// An enumeration representing the prescribed states of node execution
    /// </summary>
    public enum NodeExecutionState
    {
        Initialized,
        Executing,
        ExecutingGetBehavior,
        ExecutingGetData,
        CheckingSecurity,
        SecurityFailed,
        SecuritySuccessful,
        ExecutingParent,
        ExecutingLeft,
        ExecutingRight,
        Executed
    }
}
