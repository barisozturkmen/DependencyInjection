#if UNITY
using System;
using UnityEngine;

namespace DI
{
    public abstract class ServiceInstallerMono : MonoBehaviour
    {
        private DIContainer Container { get; set; }

        private void Awake()
        {
            Container = new DIContainer();
            DIContext.Injector = new DependencyInjector(Container);
            InstallServices();
        }

        protected ServiceInstallerMono Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            Container.Register<TInterface, TImplementation>();
            return this;
        }

        protected ServiceInstallerMono Register<TInterface>(TInterface instance)
            where TInterface : class
        {
            Container.Register(instance);
            return this;
        }

        protected ServiceInstallerMono Register(Type interfaceType, object implementation)
        {
            Container.Register(interfaceType, implementation);
            return this;
        }

        public abstract void InstallServices();
    }
}
#endif