using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestConsole2013.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_console_2013_create_account()
        {
            //Arrange
            CrmConnectionMock connectionMock = new CrmConnectionMock();
            connectionMock.SetMockCreates(new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));

            //Act
            Guid result = Program.CreateAccount(connectionMock.CrmConnection2011Adapter);


            //Assert
            Assert.AreEqual(result, new Guid("27311EC2-35E7-4178-A8E0-9C27039A21BA"));
        }
    }
}