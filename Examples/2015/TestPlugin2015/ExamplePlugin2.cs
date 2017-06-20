using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TestPlugin2015
{
    public class ExamplePlugin2 : IPlugin
    {
        private readonly string _unsecureConfig;
        private readonly string _secureConfig;
        public ExamplePlugin2(string unsecureConfig, string secureConfig)
        {
            _unsecureConfig = unsecureConfig;
            _secureConfig = secureConfig;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {
                tracer.Trace("Unsecure: " + _unsecureConfig);
                tracer.Trace("Secure: " + _secureConfig);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public int GetAccountAndContactCount(IOrganizationService service)
        {
            QueryExpression query1 = new QueryExpression
            {
                EntityName = "account",
                ColumnSet = new ColumnSet("name", "telephone1")
            };

            EntityCollection accounts = service.RetrieveMultiple(query1);

            FetchExpression query2 = new FetchExpression(@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                                                  <entity name='contact'>
                                                                    <attribute name='fullname' />
                                                                    <attribute name='telephone1' />
                                                                    <attribute name='contactid' />
                                                                  </entity>
                                                                </fetch>");

            EntityCollection contacts = service.RetrieveMultiple(query2);

            return accounts.Entities.Count + contacts.Entities.Count;
        }
    }
}