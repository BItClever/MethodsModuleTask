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

        [SetUp]
        public void Setup()
        {
            var serviceLocator = new ServiceLocator();
            fileSystemVisitor = serviceLocator.Resolve<IFileSystemVisitor>();
        }

        [Test]
        public void GetAllFilesTest()
        {
            //Arrange:

            //Act:
            var result = fileSystemVisitor.GetAllFiles(Path);

            //Assert:
            Assert.Pass();
            Assert.IsInstanceOf<IEnumerable<string>>(result);
            Assert.NotNull(result);
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
            //Arrange:

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