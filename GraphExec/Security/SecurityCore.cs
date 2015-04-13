using System;
using System.Collections.Generic;

namespace GraphExec.Security
{
    internal sealed class SecurityCore
    {
        internal SecurityCore()
        {
            this.recordedPermissionChecks = new Dictionary<string, PermissionCheckResult>();
            this.permissionRegistry = new Dictionary<Type, PermissionRegistryItem>();
        }

        private readonly Dictionary<string, PermissionCheckResult> recordedPermissionChecks;
        private readonly Dictionary<Type, PermissionRegistryItem> permissionRegistry;

        internal bool Check(Type type)
        {
            return this.permissionRegistry.ContainsKey(type);
        }

        internal bool Check(string permission)
        {
            return this.recordedPermissionChecks.ContainsKey(permission);
        }

        internal void Register<T, TPermissionCheck, TResult>(TPermissionCheck permissionCheck)
            where TResult : PermissionCheckResult, new()
            where TPermissionCheck : BasePermissionCheck<TResult>, new()
        {
            var item = new PermissionRegistryItem();
            item.Set<TPermissionCheck, TResult>(permissionCheck);

            this.permissionRegistry.Add(typeof(T), item);
        }

        internal void RecordCheck(string permission, PermissionCheckResult result)
        {
            if (!this.Check(permission))
            {
                this.recordedPermissionChecks.Add(permission, result);
            }
        }

        internal PermissionRegistryItem GetPermission<T>()
        {
            var type = typeof(T);

            PermissionRegistryItem value = null;
            if (this.Check(type) && this.permissionRegistry.TryGetValue(type, out value))
            {
                return value;
            }

            return null;
        }

        internal sealed class PermissionRegistryItem
        {
            internal void Set<TPermissionCheck, TResult>(TPermissionCheck permissionCheck)
                where TResult : PermissionCheckResult, new()
                where TPermissionCheck : BasePermissionCheck<TResult>, new()
            {
                this.PermissionCheck = permissionCheck;

                this.ResultType = typeof(TResult);
                this.PermissionCheckType = typeof(TPermissionCheck);
            }

            internal object PermissionCheck { get; set; }

            internal Type PermissionCheckType { get; set; }

            internal Type ResultType { get; set; }
        }
    }
}
