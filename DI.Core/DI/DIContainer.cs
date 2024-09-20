using System;
using System.Collections.Generic;

namespace DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, object> _bindings = new();

        public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _bindings[typeof(TInterface)] = typeof(TImplementation);
        }

        public void Register<TInterface>(TInterface implementation)
            where TInterface : class
        {
            if (implementation == null)
            {
                throw new ArgumentNullException($"Implementation for {implementation.GetType().Name} cannot be null");
            }
            
            _bindings[typeof(TInterface)] = implementation;
        }

        public void Register(Type interfaceType, object implementation)
        {
            _bindings[interfaceType] = implementation;
        }

        internal object Resolve(Type serviceType)
        {
            if (_bindings.TryGetValue(serviceType, out object implementation))
            {
                return implementation;
            }

            throw new InvalidOperationException($"No binding found for type {serviceType.Name}");
        }
    }
}
