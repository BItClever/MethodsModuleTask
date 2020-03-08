using MethodsModuleTask.Interfaces;
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using messages = Resources.Messages;

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
            var path = ConfigurationManager.AppSettings["path"];
            try
            {
                var folders = _fileSystemVisitor.GetAllFolders(path);
                var files = _fileSystemVisitor.GetAllFiles(path);
                var newFolders = _fileSystemVisitor.GetAllFolders(path, x => x.Contains("new"));
            }
            catch(FileSystemVisitorException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(messages.CheckConfig);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unexpected exception " + e.Message);
            }
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
