using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Configuration;
using XrmMoq;

namespace TestPlugin2011.Tests
{
    [TestClass]
    public class ExamplePlugin4TestsIntegration
    {
        private static IOrganizationService _service;
        private static Guid _testAccountId;

        #region Test Initialization and Cleanup
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            //***Enter connection info in the app.config***

            //Create a Live connection to CRM & save in a class level variable
            string connString = ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString;
            _service = XrmMoq.Helpers.CrmConnection.Get(connString);

            //Create a test recod in CRM
            Entity account = new Entity("account")
            {
                ["name"] = "Some Unique Name",
                ["telephone1"] = "444-555-6677"
            };

            _testAccountId = _service.Create(account);
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void ClassCleanup() { }

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestMethodInitialize() { }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void TestMethodCleanup()
        {
            //Clean up test record
            _service.Delete("account", _testAccountId);
        }
        #endregion

        /// <summary>
        /// Tests a plug-in using a fake Target entity, specifying the MessageName using one of the helper methods, 
        /// using an Execute request, and validating the value from the Target entity.
        /// </summary>
        [TestMethod]
        public void Test_plugin_2011_method_livecrm_setup_teardown()
        {
            //Arrange

            //We don't really need to use the plug-in mock here since we've already got an 
            //instance at the class level - but this could be used if executing the entire plug-in
            CrmPluginMock pluginMock = new CrmPluginMock
            {
                LiveOrganizationService = _service
            };

            //Act

            //Execute the plug-in method providing the real CRM service
            string telephone1 = ExamplePlugin4.GetAccountTelephone(_service);

            //Assert

            //Check the value specified in the test record matches what was created and returned from CRM
            Assert.AreEqual(telephone1, "444-555-6677");
        }
    }
}