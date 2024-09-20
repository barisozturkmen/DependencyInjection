using System;

namespace DI
{
    public abstract class ServiceInstaller
    {
        private DIContainer Container { get; }

        protected ServiceInstaller()
        {
            Container = new DIContainer();
            DIContext.Injector = new DependencyInjector(Container);
            InstallServices();
        }
    
        protected ServiceInstaller Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            Container.Register<TInterface, TImplementation>();
            return this;
        }

        protected ServiceInstaller Register<TInterface>(TInterface instance)
            where TInterface : class
        {
            Container.Register(instance);
            return this;
        }

        protected ServiceInstaller Register(Type interfaceType, object implementation)
        {
            Container.Register(interfaceType, implementation);
            return this;
        }

        public abstract void InstallServices();
    }
}
