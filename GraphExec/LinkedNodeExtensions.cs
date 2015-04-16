
namespace GraphExec
{
    public static class LinkedNodeExtensions
    {
        public static bool HasLeft(this ILinkedNode _this)
        {
            return _this.Left != null;
        }

        public static bool HasRight(this ILinkedNode _this)
        {
            return _this.Right != null;
        }
    }
}
