using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;

namespace TestConsole2011.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_console_2011_create_account()
        {
            //Arrange
            Mock<CrmConnectionAdapter> mockServiceClientAapter = new Mock<CrmConnectionAdapter>();
            mockServiceClientAapter.Setup(t => t.Create(It.IsAny<Entity>())).Returns(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //CrmServiceClientMock mockServiceClient = new CrmServiceClientMock();
            //mockServiceClient.SetMockCreates(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //Act
            Guid result = Program.CreateAccount(mockServiceClientAapter.Object);
            //Guid result = Program.CreateAccount(mockServiceClient.CrmServiceClientAdapter);


            //Assert
            Assert.AreEqual(result, new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));
        }
    }
}