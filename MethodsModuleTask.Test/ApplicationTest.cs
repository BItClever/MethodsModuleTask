using MethodsModuleTask.Interfaces;
using NUnit.Framework;
using Moq;
using Moq.Protected;

namespace MethodsModuleTask.Test
{
    [TestFixture]
    public class ApplicationTest
    {
        private IFileSystemVisitor fileSystemVisitor;
        private const string Path = "D:\\test";

        [SetUp]
        public void SetUp()
        {
            var serviceLocator = new ServiceLocator();
            fileSystemVisitor = serviceLocator.Resolve<IFileSystemVisitor>();
        }

        [Test]
        public void SubscribeTest()
        {
            //Arrange:
            var testApp = new ApplicationUT(fileSystemVisitor);
            var appMock = new Moq.Mock<Application>(fileSystemVisitor) { CallBase = true };
            appMock
                .Protected()
                .Setup<bool>("Subscribe")
                .Returns(true);
            
            //Act:
            var result = testApp.SubscribeTest();
            appMock.Object.Start();

            //Assert:
            Assert.IsTrue(result);
            appMock.Protected().Verify("Subscribe", Times.Once());
        }
    }



    public class ApplicationUT : Application
    {
        public ApplicationUT(IFileSystemVisitor fileSystemVisitor) : base(fileSystemVisitor)
        {

        }

        public bool SubscribeTest()
        {
            return base.Subscribe();
        }

        public void WorkStartHandlerTest(object sender, VisitorEventArgs args)
        {
            base.WorkStartHandler(sender, args);
        }

        public void WorkFinishHandlerTest(object sender, VisitorEventArgs args)
        {
            base.WorkFinishHandler(sender, args);
        }
    }
}
