using MethodsModuleTask.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using messages = Resources.Messages;

namespace MethodsModuleTask
{
    public class FileSystemVisitor : IFileSystemVisitor
    { 

        public event EventHandler<VisitorEventArgs> WorkStart;
        public event EventHandler<VisitorEventArgs> WorkFinish;

        public IEnumerable<string> GetAllFiles(string path)
        {
            var message = messages.ExceptionMessage;
            if (!CheckPathExists(path))
            {
                throw new FileSystemVisitorException(string.Concat(message, path));
            }
            OnWorkStart();
            var di = new DirectoryInfo(path);
            var result = di.GetFiles("*.*", SearchOption.AllDirectories).Select(x => x.FullName);
            OnWorkFinish();
            return result;
        }

        public IEnumerable<string> GetAllFiles(string path, Func<string, bool> filter)
        {
            var files = GetAllFiles(path).ToList();
            foreach (var file in files)
            {
                if(filter(file))
                {
                    yield return file;
                }
            }
        }

        public IEnumerable<string> GetAllFolders(string path)
        {
            var message = messages.ExceptionMessage;
            if (!CheckPathExists(path))
            {
                throw new FileSystemVisitorException(string.Concat(message, path));
            }
            OnWorkStart();
            var di = new DirectoryInfo(path);
            var result = di.GetDirectories("*.*", SearchOption.AllDirectories).AsEnumerable().Select(f => f.FullName);
            OnWorkFinish();
            return result;
        }

        public IEnumerable<string> GetAllFolders(string path, Func<string, bool> filter)
        {
            var folders = GetAllFolders(path).ToList();
            foreach (var folder in folders)
            {
                if (filter(folder))
                {
                    yield return folder;
                }
            }
        }

        private void OnWorkStart()
        {
            WorkStart?.Invoke(this, new VisitorEventArgs(messages.Start));
        }

        private void OnWorkFinish()
        {
            WorkFinish?.Invoke(this, new VisitorEventArgs(messages.Finish));
        }

        private bool CheckPathExists(string path)
        {
            return Directory.Exists(path);
        }

    }
}
