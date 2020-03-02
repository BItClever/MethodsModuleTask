using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
