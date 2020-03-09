namespace IoC
{
    public interface IContainer
    {
        void Register<TypeToResolve, TargetType>(bool isSingleton = false);
        void Register<TypeToResolve>(bool isSingleton = false);
        TypeToResolve Resolve<TypeToResolve>();

    }
}
