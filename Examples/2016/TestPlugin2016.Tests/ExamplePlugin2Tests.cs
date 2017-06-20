using XrmMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace TestPlugin2016.Tests
{
    [TestClass]
    public class ExamplePlugin2Tests
    {
        [TestMethod]
        public void Test_plugin_2016_trace_configurations()
        {
            //Arrange

            //Create the plug-in mock and set the unsecure & secure configurations
            CrmPluginMock pluginMock = new CrmPluginMock
            {
                UnsecureConfiguration = "Hello World",
                SecureConfiguration = "Testing 1234"
            };

            //Act

            //Execute the plug-in
            pluginMock.Execute<ExamplePlugin2>();

            //Assert

            //Review the test Output to see the configuration values
            Assert.IsNotNull(1);
        }

        [TestMethod]
        public void Test_plugin_2016_multiple_retrievemultiple_method_using_orgservice()
        {
            //Arrange

            //Create the plug-in mock
            CrmPluginMock pluginMock = new CrmPluginMock();

            //Define the result of the first query - type QueryExpressions
            EntityCollection accounts = new EntityCollection
            {
                Entities =
                {
                    new Entity("account") {Id = Guid.NewGuid(), ["name"] = "test1", ["telephone1"] = "555-555-5555"},
                    new Entity("account") {Id = Guid.NewGuid(), ["name"] = "test2", ["telephone1"] = "333-333-3333"}
                }
            };

            //Define the result of the first query - type FetchExpression
            EntityCollection contacts = new EntityCollection
            {
                Entities =
                {
                    new Entity("contact") {Id = Guid.NewGuid(), ["fullname"] = "Joe Smith", ["telephone1"] = "444-444-4444"},
                    new Entity("contact") {Id = Guid.NewGuid(), ["fullname"] = "Bob Smith", ["telephone1"] = "777-777-7777"}
                }
            };
            //Set the RetrieveMultiple responses
            pluginMock.SetMockRetrieveMultiples(accounts, contacts);

            //Act

            //Execute the plug-in
            ExamplePlugin2 examplePlugin2 = new ExamplePlugin2(String.Empty, String.Empty);
            int count = examplePlugin2.GetAccountAndContactCount(pluginMock.FakeOrganizationService);

            //Assert

            //Review the test Output to see the configuration values
            Assert.AreEqual(count, 4);
        }
    }
}