using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphExec
{
    internal class DependencyBuilder
    {
        internal DependencyBuilder()
        {
            this.m_registry = new Dictionary<Type, object>();
        }

        private Dictionary<Type, object> m_registry;

        internal void Register<T>(T instance)
        {
            // Do not accept null instance
            Args.IsNotNull(() => instance);

            // Do not accept null registry
            Throw.Exception<NullReferenceException>(() => this.m_registry == null, "Dependency build registry is null. Please re-initialize DependencyBuilder");

            // store type
            var type = typeof(T);
            Vars.HandleNull(type, this.HandleNull);

            // Throw if registry already contains type
            Throw.Exception<InvalidOperationException>(() => this.m_registry.Keys.Contains(type));

            // Add registry instance
            this.m_registry.Add(type, instance);
        }

        internal IContainer Build()
        {
            return new DependencyContainer(this.m_registry);
        }

        private void HandleNull()
        {
            throw new InternalGraphExecException();
        }
    }
}
