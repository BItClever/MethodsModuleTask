using MethodsModuleTask.Interfaces;
using IoC;

namespace MethodsModuleTask
{
    public class ServiceLocator
    {
        private readonly IContainer _customContainer;
        public ServiceLocator()
        {
            _customContainer = new Container();
            RegisterDepandencies();
        }

        private void RegisterDepandencies()
        {
            _customContainer.Register<IFileSystemVisitor, FileSystemVisitor>();
            _customContainer.Register<Application>();
        }

        public T Resolve<T>()
        {
            return _customContainer.Resolve<T>();
        }
    }
}
