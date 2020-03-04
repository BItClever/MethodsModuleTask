using System;

namespace MethodsModuleTask
{
    public class VisitorEventArgs : EventArgs
    {
        public VisitorEventArgs(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
