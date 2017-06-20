using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using System;

namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper methods to extract the IOrganizationService from a live Client Extensions CrmConnection to Dynamics CRM for use in testing or debugging.
    /// </summary>
    public static class CrmConnection
    {
        /// <summary>
        /// Gets the IOrganizationService from a Client Extensions (2011) CrmConnection instance.
        /// </summary>
        /// <param name="connectionString">Connection string to CRM organization.</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string connectionString)
        {
            Microsoft.Xrm.Client.CrmConnection connection = Microsoft.Xrm.Client.CrmConnection.Parse(connectionString);

            if (string.IsNullOrEmpty(connection.GetConnectionId()))
                throw new Exception("Unable to connect to CRM");

            return new OrganizationService(connection);
        }
    }
}