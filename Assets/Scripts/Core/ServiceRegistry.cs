using System;
using System.Collections.Generic;

namespace Monsters.Core
{
    public sealed class ServiceRegistry
    {
        private readonly Dictionary<Type, object> _services = new();

        public void Register<TService>(TService instance) where TService : class
        {
            _services[typeof(TService)] = instance;
        }

        public TService Resolve<TService>() where TService : class
        {
            return _services.TryGetValue(typeof(TService), out var service)
                ? (TService)service
                : null;
        }
    }
}
