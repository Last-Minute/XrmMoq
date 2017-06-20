using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Configuration;
using System.ServiceModel;

namespace TestConsole2015
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Microsoft.Xrm.Client.CrmConnection connection = Microsoft.Xrm.Client.CrmConnection.Parse(ConfigurationManager
                    .ConnectionStrings["CRMConnectionString"].ConnectionString);

                CrmConnectionAdapter adapter = new CrmConnectionAdapter(new OrganizationService(connection));

                Guid newAccountId = CreateAccount(adapter);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string message = ex.Message;
                throw;
            }
        }

        public static Guid CreateAccount(CrmConnectionAdapter client)
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