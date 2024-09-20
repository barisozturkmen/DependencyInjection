using System;
using System.Reflection;

namespace DI
{
    public class DependencyInjector
    {
        private readonly DIContainer _diContainer;

        public DependencyInjector(DIContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void InjectDependencies(object diClient)
        {
            InjectDependenciesForType(diClient.GetType(), diClient);
        }

        private void InjectDependenciesForType(Type type, object instance)
        {
            while (true)
            {
                FieldInfo[] fields =
                    type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (FieldInfo field in fields)
                {
                    if (!field.IsDefined(typeof(InjectAttribute), true))
                        continue;
                    Type fieldType = field.FieldType;
                    object dependency = _diContainer.Resolve(fieldType);
                    field.SetValue(instance, dependency);
                }

                PropertyInfo[] properties =
                    type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    if (!property.IsDefined(typeof(InjectAttribute), true))
                        continue;
                    Type propertyType = property.PropertyType;
                    object dependency = _diContainer.Resolve(propertyType);
                    property.SetValue(instance, dependency);
                }

                Type parentType = type.BaseType;
                if (parentType != null)
                {
                    type = parentType;
                    continue;
                }

                break;
            }
        }
    }
}
