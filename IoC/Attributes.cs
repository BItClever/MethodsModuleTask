using System;

namespace IoC
{ 
    [AttributeUsage(AttributeTargets.Class)]
    public class ImportConstructorAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ImportAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute()
        {

        }

        public Type TypeToResolve { get; private set; }

        public ExportAttribute(Type typeToResolve)
        {
            TypeToResolve = typeToResolve;
        }
    }
}
