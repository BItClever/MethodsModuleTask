using MethodsModuleTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MethodsModuleTask
{
    public class Application
    {
        private readonly IFileSystemVisitor _fileSystemVisitor;

        public Application(IFileSystemVisitor fileSystemVisitor)
        {
            _fileSystemVisitor = fileSystemVisitor;
        }

        public void Start()
        {
            if (ConfigurationManager.AppSettings["language"] == "russian")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            }
            Subscribe();
            var path = "D:\\test";
            var folders = _fileSystemVisitor.GetAllFolders(path);
            var files = _fileSystemVisitor.GetAllFiles(path);
            var newFolders = _fileSystemVisitor.GetAllFolders(path, x => x.Contains("new"));
        }

        protected virtual bool Subscribe()
        {
            _fileSystemVisitor.WorkStart += WorkStartHandler;
            _fileSystemVisitor.WorkFinish += WorkFinishHandler;
            return true;
        }

        protected virtual void WorkStartHandler(object sender, VisitorEventArgs args)
        {
            Console.WriteLine(args.Message);
        }

        protected virtual void WorkFinishHandler(object sender, VisitorEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
