using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExec
{
    internal sealed class DependencyContainer : IContainer
    {
        internal DependencyContainer(Dictionary<Type, object> registry)
        {
            this.m_registry = registry;
        }

        private readonly Dictionary<Type, object> m_registry;

        /// <summary>
        /// Resolve the given type
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>An instance of type <typeparamref name="T"/></returns>
        public T Resolve<T>()
        {
            var type = typeof(T);
            Vars.HandleNull(type, this.HandleNull);

            // Throw if registry does not include type
            Throw.Exception<InvalidOperationException>(() => !this.m_registry.Keys.Contains(type));

            return (T)this.m_registry[type];
        }

        private void HandleNull()
        {
            throw new InternalGraphExecException();
        }
    }
}
