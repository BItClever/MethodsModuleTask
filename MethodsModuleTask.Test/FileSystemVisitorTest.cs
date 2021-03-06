using MethodsModuleTask.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MethodsModuleTask.Test
{
    [TestFixture]
    public class FileSystemVisitorTest
    {
        private IFileSystemVisitor fileSystemVisitor;
        private const string Path = "D:\\test";
        private const string WrongPath = "qwerty";

        [SetUp]
        public void Setup()
        {
            var serviceLocator = new ServiceLocator();
            fileSystemVisitor = serviceLocator.Resolve<IFileSystemVisitor>();
        }

        [Test]
        public void GetAllFilesTest()
        {
            //Act:
            var result = fileSystemVisitor.GetAllFiles(Path);

            //Assert:
            Assert.Pass();
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.NotNull(result);
        }

        [Test]
        public void GetAllFilesException()
        {
            Assert.Throws<FileSystemVisitorException>(() => fileSystemVisitor.GetAllFiles(WrongPath));
        }

        [Test]
        public void GetAllFilesFilteredTest()
        {
            //Arrange:
            Func<string, bool> filter = x => x.EndsWith(".pdf");

            //Act:
            var result = fileSystemVisitor.GetAllFiles(Path, filter);

            //Assert:
            Assert.Pass();
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.NotNull(result);
        }

        [Test]
        public void GetAllFoldersTest()
        {
            //Act:
            var result = fileSystemVisitor.GetAllFolders(Path);

            //Assert:
            Assert.Pass();
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.NotNull(result);
        }

        [Test]
        public void GetAllFoldersFilteredTest()
        {
            //Arrange:
            Func<string, bool> filter = x => x.ToLower().Contains("new");

            //Act:
            var result = fileSystemVisitor.GetAllFolders(Path);

            //Assert:
            Assert.Pass();
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.NotNull(result);
        }
    }
}