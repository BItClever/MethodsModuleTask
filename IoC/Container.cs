using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace IoC
{
    public class Container : IContainer
    {
        private List<RegisteredObject> _registeredObjects;

        public Container()
        {
            _registeredObjects = new List<RegisteredObject>();

            RegisterInjectedAttributes();
        }


        public void Register<TypeToResolve, TargetType>(bool isSingleton = false)
        {
            Register(typeof(TypeToResolve), typeof(TargetType), isSingleton);
        }

        private void Register(Type typeToResolve, Type targetType, bool isSingleton = false)
        {
            _registeredObjects.Add(new RegisteredObject
            {
                TypeToResolve = typeToResolve,
                TargetType = targetType,
                IsSingleton = isSingleton
            });
        }

        public void Register<TypeToResolve>(bool isSingleton = false)
        {
            Register(typeof(TypeToResolve), isSingleton);
        }

        private void Register(Type typeToResolve, bool isSingleton = false)
        {
            Register(typeToResolve, typeToResolve, isSingleton);
        }

        private void RegisterInjectedAttributes()
        {
            var definedTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var definedType in definedTypes)
            {
                var isImportedConstructor = definedType.GetCustomAttribute<ImportConstructorAttribute>() != null;

                if (isImportedConstructor)
                {
                    Register(definedType);
                }

                var exportAttr = definedType.GetCustomAttribute<ExportAttribute>();

                if (exportAttr != null)
                {
                    if (exportAttr.TypeToResolve != null)
                    {
                        Register(exportAttr.TypeToResolve, definedType);
                    }
                    Register(definedType);
                }
            }
        }

        public TypeToResolve Resolve<TypeToResolve>()
        {
            return (TypeToResolve)Resolve(typeof(TypeToResolve));
        }

        private object Resolve(Type typeToResolve)
        {
            var registerdType = _registeredObjects.First(t => t.TypeToResolve == typeToResolve);

            if (registerdType != null)
            {
                if (registerdType.IsSingleton)
                {
                    if (registerdType.SingletonInstance == null)
                    {
                        registerdType.SingletonInstance = CreateInstance(registerdType.TargetType);
                    }
                    return registerdType.SingletonInstance;
                }
                else
                {
                    return CreateInstance(registerdType.TargetType);
                }
            }
            else
            {
                throw new Exception($"You tried to resolve not regiterd type: {typeToResolve}");
            }
        }

        private object CreateInstance(Type type)
        {
            var constructorParameters = GetConstructorParameters(type).ToArray();

            var instance = constructorParameters.Any() ? Activator.CreateInstance(type, constructorParameters) : Activator.CreateInstance(type);

            ResolveImportedProperties(instance);

            return instance;
        }

        private void ResolveImportedProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();

            foreach (var property in properties)
            {
                var isImported = property.GetCustomAttribute<ImportAttribute>() != null;
                if (isImported)
                {
                    var propertyValue = Resolve(property.PropertyType);
                    property.SetValue(instance, propertyValue);
                }
            }
        }

        private IEnumerable<object> GetConstructorParameters(Type type)
        {
            var costructorInfo = type.GetConstructors().First();
            var parameters = costructorInfo.GetParameters();

            foreach (var parameter in parameters)
            {
                yield return Resolve(parameter.ParameterType);
            }
        }
    }
}
