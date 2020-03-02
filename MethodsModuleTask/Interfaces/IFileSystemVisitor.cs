using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsModuleTask.Interfaces
{
    public interface IFileSystemVisitor
    {
        IEnumerable<string> GetAllFiles(string path);
        IEnumerable<string> GetAllFiles(string path, Func<string, bool> filter);
        IEnumerable<string> GetAllFolders(string path);
        IEnumerable<string> GetAllFolders(string path, Func<string, bool> filter);
        event EventHandler<VisitorEventArgs> WorkStart;
        event EventHandler<VisitorEventArgs> WorkFinish;
    }
}
