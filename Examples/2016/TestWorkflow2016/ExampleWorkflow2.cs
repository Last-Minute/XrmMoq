using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace TestWorkflow2016
{
    public class ExampleWorkflow2 : CodeActivity, IExampleWorkflow2
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                //Do stuff...
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public int Test()
        {
            var x = Test2() / 2;

            return x;
        }

        public virtual int Test2()
        {
            var x = 10;

            return x;
        }

        public string Test3()
        {
            string value = "Hello World!";

            return value;
        }
    }

    public interface IExampleWorkflow2
    {
        int Test();
    }
}