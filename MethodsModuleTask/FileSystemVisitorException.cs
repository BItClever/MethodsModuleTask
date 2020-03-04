using System;
using System.Runtime.Serialization;

namespace MethodsModuleTask
{
    public class FileSystemVisitorException : Exception
    {
        public FileSystemVisitorException()
        {

        }

        public FileSystemVisitorException(string message) : base(message)
        {

        }

        public FileSystemVisitorException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected FileSystemVisitorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
