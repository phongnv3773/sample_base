using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace ServerAPI.Dependency
{
    /// <summary>
    ///     An implementation of the <see cref="IDependencyResolver" /> interface that wraps a Unity container.
    /// </summary>
    public sealed class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;
        private readonly SharedDependencyScope sharedScope;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnityDependencyResolver" /> class for a container.
        /// </summary>
        /// <param name="container">
        ///     The <see cref="IUnityContainer" /> to wrap with the <see cref="IDependencyResolver" />
        ///     interface implementation.
        /// </param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            sharedScope = new SharedDependencyScope(container);
        }

        /// <summary>
        ///     Reuses the same scope to resolve all the instances.
        /// </summary>
        /// <returns>The shared dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            return sharedScope;
        }

        /// <summary>
        ///     Disposes the wrapped <see cref="IUnityContainer" />.
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
            sharedScope.Dispose();
        }

        /// <summary>
        ///     Resolves an instance of the default requested type from the container.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type" /> of the object to get from the container.</param>
        /// <returns>The requested object.</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        ///     Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        private sealed class SharedDependencyScope : IDependencyScope
        {
            private readonly IUnityContainer container;

            public SharedDependencyScope(IUnityContainer container)
            {
                this.container = container;
            }

            public object GetService(Type serviceType)
            {
                return container.Resolve(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return container.ResolveAll(serviceType);
            }

            public void Dispose()
            {
                // NO-OP, as the container is shared.

            }
        }
    }
}