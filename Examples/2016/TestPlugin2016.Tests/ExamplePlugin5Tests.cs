using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using XrmMoq;

namespace TestPlugin2016.Tests
{
    [TestClass]
    public class ExamplePlugin5Tests
    {
        [TestMethod]
        public void Test_plugin_2016_method_using_interface()
        {
            //Arrange

            //An interface to expose the Test method has been created on the plug-in class
            //Once an interface is established, mocking becomes possible
            Mock<IExamplePlugin5> ex = new Mock<IExamplePlugin5>();

            //We can now use the core Moq (https://github.com/moq/moq4) functionality 
            //to set the method return value
            ex.Setup(t => t.Test()).Returns(5);

            //Act

            //Execute the method - using the Object property of the Mocked object 
            //this acts like the real instance
            int x = ex.Object.Test();

            //Assert

            //Compare the method result to the expected value
            Assert.AreEqual(x, 5);
        }

        [TestMethod]
        public void Test_plugin_2016_verify_virtual_method()
        {
            //Arrange

            //Mock the real workflow class
            Mock<ExamplePlugin5> ex = new Mock<ExamplePlugin5>(String.Empty, String.Empty);

            //The Test2 method can be configred to return a value because 
            //it is marked Virtual, if is was not, this would produce an error
            ex.Setup(t => t.Test2()).Returns(8);

            //Act

            //Execute the method - using the Object property of the Mocked object 
            //this acts like the real instance
            int result = ex.Object.Test();

            //Assert

            //Using additional Moq functionality use the "Verify" method on the Mock object 
            //to determine if a method was executed (Test2 is called from Test)
            ex.Verify(t => t.Test2(), Times.Once);
        }

        [TestMethod]
        public void Test_plugin_2016_execute_nonvirtual_method()
        {
            //Arrange

            string expected = "Hello World!";

            //Create a plug-in mock - this allows the constructor values to be set
            CrmPluginMock pluginMock = new CrmPluginMock { UnsecureConfiguration = expected };

            //Act

            //Use the ExecuteMethod method of the plug-in mock to execute 
            //a method in the workflow - in this case a method named Test3
            string unsecureConfig = String.Empty;
            object result = pluginMock.ExecuteMethod<ExamplePlugin5>("Test3");
            if (result != null)
                unsecureConfig = result.ToString();

            //Assert

            //Compare the method result to the expected value
            Assert.AreEqual(unsecureConfig, expected);
        }
    }
}