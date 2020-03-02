using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
