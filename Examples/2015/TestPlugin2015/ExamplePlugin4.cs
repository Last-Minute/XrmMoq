using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TestPlugin2015
{
    public class ExamplePlugin4 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {
               string telephone = GetAccountTelephone(service);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public static string GetAccountTelephone(IOrganizationService service)
        {
            QueryExpression query = new QueryExpression
            {
                EntityName = "account",
                ColumnSet = new ColumnSet("telephone1"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = "name",
                            Operator = ConditionOperator.Equal,
                            Values = {"Some Unique Name"}
                        }
                    }
                }
            };

            EntityCollection results = service.RetrieveMultiple(query);

            return results.Entities[0].GetAttributeValue<string>("telephone1");
        }
    }
}