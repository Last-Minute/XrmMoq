using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;

namespace TestConsole2016.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        [TestCategory("ExampleUnit")]
        public void Test_console_2016_get_callerid()
        {
            //Arrange
            Mock<CrmServiceClientAdapter> mockServiceClientAapter = new Mock<CrmServiceClientAdapter>();
            mockServiceClientAapter.Setup(t => t.CallerId).Returns(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //Act
            Guid result = Program.GetCallerId(mockServiceClientAapter.Object);

            //Assert
            Assert.AreEqual(result, new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));
        }

        [TestMethod]
        [TestCategory("ExampleUnit")]
        public void Test_console_2016_create_account_using_clientmock()
        {
            //Arrange
            CrmServiceClientMock mockServiceClient = new CrmServiceClientMock();
            mockServiceClient.SetMockCreates(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //Act
            Guid result = Program.CreateAccount(mockServiceClient.CrmServiceClientAdapter);

            //Assert
            Assert.AreEqual(result, new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));
        }

        [TestMethod]
        [TestCategory("ExampleUnit")]
        public void Test_console_2016_create_account_using_mockclientadapter()
        {
            //Arrange
            Mock<CrmServiceClientAdapter> mockServiceClientAapter = new Mock<CrmServiceClientAdapter>();
            mockServiceClientAapter.Setup(t => t.Create(It.IsAny<Entity>())).Returns(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //Act
            Guid result = Program.CreateAccount(mockServiceClientAapter.Object);

            //Assert
            Assert.AreEqual(result, new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));
        }
    }
}