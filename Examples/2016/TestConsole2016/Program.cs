using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.ServiceModel;

namespace TestConsole2016
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CrmServiceClient client = new CrmServiceClient(ConfigurationManager
                     .ConnectionStrings["CRMConnectionString"].ConnectionString);
     
                CrmServiceClientAdapter adapter = new CrmServiceClientAdapter(client);

                Guid callerId = GetCallerId(adapter);

                Guid newAccountId = CreateAccount(adapter);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        public static Guid GetCallerId(CrmServiceClientAdapter client)
        {
            return client.CallerId;
        }

        public static Guid CreateAccount(CrmServiceClientAdapter client)
        {
            Guid newAccountId;

            Entity account = new Entity("account")
            {
                ["name"] = "Test 1234"
            };

            using (client)
            {
                newAccountId = client.Create(account);
            }

            return newAccountId;
        }
    }
}