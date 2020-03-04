namespace MethodsModuleTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.Resolve<Application>().Start();
        }
    }
}
