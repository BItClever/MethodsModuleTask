using MethodsModuleTask.Interfaces;
using Unity;

namespace MethodsModuleTask
{
    public class ServiceLocator
    {
        private readonly IUnityContainer _unityContainer;
        public ServiceLocator()
        {
            _unityContainer = new UnityContainer();
            RegisterDepandencies();
        }

        private void RegisterDepandencies()
        {
            _unityContainer.RegisterType<IFileSystemVisitor, FileSystemVisitor>();
            _unityContainer.RegisterType<Application>();

        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
