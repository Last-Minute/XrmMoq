using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TestPlugin2011
{
    public class ExamplePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                string fullName = null;

                if (context.MessageName == "create")
                {
                    Guid userId = ExecuteWhoAmI(service);
                    fullName = userId.ToString();
                }

                if (context.MessageName == "update")
                {
                    fullName = RetrieveUserFullName(service, tracer, context.UserId);
                }

                entity["name"] = fullName;
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public string RetrieveUserFullName(IOrganizationService service, ITracingService tracer, Guid userId)
        {
            Entity user = service.Retrieve("systemuser", userId, new ColumnSet("fullname"));

            tracer.Trace("UserId: " + user.Id);

            return user.GetAttributeValue<string>("fullname");
        }

        private static Guid ExecuteWhoAmI(IOrganizationService service)
        {
            WhoAmIRequest request = new WhoAmIRequest();
            WhoAmIResponse response = (WhoAmIResponse)service.Execute(request);

            return response.UserId;
        }
    }
}