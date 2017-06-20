using XrmMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestWorkflow2011.Tests
{
    [TestClass]
    public class ExampleWorkflowTests
    {
        [TestMethod]
        public void Test_workflow_2011_input_output()
        {
            //Arrange

            //Create the workflow mock and set any Input Parameters the workflow might need
            CrmWorkflowMock workflowMock = new CrmWorkflowMock
            {
                WorkflowContext = new FakeWorkflowContext
                {
                    InputParameters = new ParameterCollection
                    {
                        { "DateToCheck", new DateTime(2012, 5, 7)}
                    }
                }
            };

            //Act

            //Execute the workflow
            var output = workflowMock.Execute<ExampleWorkflow>();

            //Assert

            //Check to see if the output value matches the expected value
            bool isHoliday = (bool)output["IsPublicHoliday"];
            Assert.IsFalse(isHoliday);
        }

        [TestMethod]
        public void Test_workflow_2011_callwebservice()
        {
            //Arrange

            //Use the adapter patternt to create a wrapper for the standard HttpClient
            Mock<HttpClientAdapter> adapterMock = new Mock<HttpClientAdapter>();

            //Create the response
            //Note - not all properties have setters
            HttpResponseMessage response = new HttpResponseMessage();
            StringContent content = new StringContent("{\"isPublicHoliday\":true}");
            response.StatusCode = HttpStatusCode.OK; 
            response.Content = content;

            adapterMock.Setup(t => t.SendAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(response);

            //Act

            //Execute the method
            ExampleWorkflow workflow = new ExampleWorkflow();
            Task<bool> result = workflow.CallWebService(adapterMock.Object, "25-12-2015");

            //Assert

            //Check to see if the output value matches the expected value
            bool isHoliday = Boolean.Parse(result.Result.ToString());
            Assert.IsTrue(isHoliday);
        }
    }
}