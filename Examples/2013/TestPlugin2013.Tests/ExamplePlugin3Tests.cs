using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using XrmMoq;

namespace TestPlugin2013.Tests
{
    [TestClass]
    public class ExamplePlugin3Tests
    {
        [TestMethod]
        public void Test_plugin_2013_preimage()
        {
            //Arrange

            //Create the plug-in mock
            CrmPluginMock pluginMock = new CrmPluginMock();
            //Define the Pre-Image
            Entity preImage = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["fullname"] = "Fred Flintstone"
            };
            //Set the PreIMage
            pluginMock.PluginPreImage = new PreImage("PreImage", preImage);

            //Act

            //Execute the plug-in
            pluginMock.Execute<ExamplePlugin3>();

            //Assert

            //Review the test Output to see the 'fullname' from the PreImage
            Assert.IsNotNull(1);
        }
    }
}