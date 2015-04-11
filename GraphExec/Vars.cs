using System;

namespace GraphExec
{
    public static class Vars
    {
        public static void HandleNull<TVariable>(TVariable var, Action handleNull)
        {
            Args.IsNotNull(() => handleNull);

            if (var == null)
            {
                handleNull();
            }
        }

        public static void HandleNull<TVariable>(TVariable var, Action<Type> handleNull)
        {
            Args.IsNotNull(() => handleNull);

            if (var == null)
            {
                handleNull(typeof(TVariable));
            }
        }

        public static void HandleNull<TVariable, THandleInfo>(TVariable var, THandleInfo info, Action<THandleInfo> handleNull)
        {
            Args.IsNotNull(() => handleNull);

            if (var == null)
            {
                handleNull(info);
            }
        }

        public static void HandleNull<TVariable, THandleInfo>(TVariable var, THandleInfo info, Action<Type, THandleInfo> handleNull)
        {
            Args.IsNotNull(() => handleNull);

            if (var == null)
            {
                handleNull(typeof(TVariable), info);
            }
        }
    }
}
