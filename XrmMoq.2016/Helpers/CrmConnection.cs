using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Net;
using System.Security;
using AuthenticationType = Microsoft.Xrm.Tooling.Connector.AuthenticationType;

namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper methods to extract the IOrganizationService from a live CrmServiceClient connection to Dynamics CRM for use in testing or debugging.
    /// </summary>
    public static class CrmConnection
    {
        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using a connection string.
        /// </summary>
        /// <param name="connectionString">Connection string to CRM organization.</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string connectionString)
        {
            CrmServiceClient crmService = new CrmServiceClient(connectionString);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using an ExternalOrgServiceProxy.
        /// </summary>
        /// <param name="externalOrgServiceProxy">ExternalOrgServiceProxy</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(OrganizationServiceProxy externalOrgServiceProxy)
        {
            CrmServiceClient crmService = new CrmServiceClient(externalOrgServiceProxy);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using an ExternalOrgWebProxyClient.
        /// </summary>
        /// <param name="externalOrgWebProxyClient">ExternalOrgWebProxyClient</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(OrganizationWebProxyClient externalOrgWebProxyClient)
        {
            CrmServiceClient crmService = new CrmServiceClient(externalOrgWebProxyClient);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using a NetworkCredential.
        /// </summary>
        /// <param name="credential">NetworkCredential</param>
        /// <param name="hostName">HostName</param>
        /// <param name="port">Port</param>
        /// <param name="orgName">OrgName</param>
        /// <param name="useUniqueInstance">UseUniqueInstance</param>
        /// <param name="useSsl">UseSSL</param>
        /// <param name="orgDetail">OrgDetail</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(NetworkCredential credential, string hostName, string port, string orgName, bool useUniqueInstance = false, bool useSsl = false, OrganizationDetail orgDetail = null)
        {
            CrmServiceClient crmService = new CrmServiceClient(credential, hostName, port, orgName, useUniqueInstance, useSsl, orgDetail);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using a NetworkCredential.
        /// </summary>
        /// <param name="credential">NetworkCredential</param>
        /// <param name="authType">AuthType</param>
        /// <param name="hostName">HostName</param>
        /// <param name="port">Port</param>
        /// <param name="orgName">OrgName</param>
        /// <param name="useUniqueInstance">UseUniqueInstance</param>
        /// <param name="useSsl">UseSSL</param>
        /// <param name="orgDetail">OrgDetail</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(NetworkCredential credential, AuthenticationType authType, string hostName, string port, string orgName, bool useUniqueInstance = false, bool useSsl = false, OrganizationDetail orgDetail = null)
        {
            CrmServiceClient crmService = new CrmServiceClient(credential, authType, hostName, port, orgName, useUniqueInstance, useSsl, orgDetail);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using credentials.
        /// </summary>
        /// <param name="crmUserId">CRMUserId</param>
        /// <param name="crmPassword">CRMPassword</param>
        /// <param name="crmRegion">CRMRegion</param>
        /// <param name="orgName">OrgName</param>
        /// <param name="useUniqueInstance">UseUniqueInstance</param>
        /// <param name="useSsl">UseSSL</param>
        /// <param name="orgDetail">OrgDetail</param>
        /// <param name="isOffice365"></param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string crmUserId, SecureString crmPassword, string crmRegion, string orgName, bool useUniqueInstance = false, bool useSsl = false, OrganizationDetail orgDetail = null, bool isOffice365 = false)
        {
            CrmServiceClient crmService = new CrmServiceClient(crmUserId, crmPassword, crmRegion, orgName, useUniqueInstance, useSsl, orgDetail, isOffice365);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using credentials.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="password">Password</param>
        /// <param name="domain">Domain</param>
        /// <param name="homeRealm">HomeRealm</param>
        /// <param name="hostName">HostName</param>
        /// <param name="port">Port</param>
        /// <param name="orgName">OrgName</param>
        /// <param name="useUniqueInstance">UseUniqueInstance</param>
        /// <param name="useSsl">UseSSL</param>
        /// <param name="orgDetail">OrgDetail</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string userId, SecureString password, string domain, string homeRealm, string hostName, string port, string orgName, bool useUniqueInstance = false, bool useSsl = false, OrganizationDetail orgDetail = null)
        {
            CrmServiceClient crmService = new CrmServiceClient(userId, password, domain, homeRealm, hostName, port, orgName, useUniqueInstance, useSsl, orgDetail);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using OAuth.
        /// </summary>
        /// <param name="crmUserId">CRMUserId.</param>
        /// <param name="crmPassword">CRMPassword.</param>
        /// <param name="crmRegion">CRMRegion.</param>
        /// <param name="orgName">OrgName.</param>
        /// <param name="useUniqueInstance">UseUniqueInstance.</param>
        /// <param name="orgDetail">OrgDetail.</param>
        /// <param name="user">User.</param>
        /// <param name="clientId">ClientId.</param>
        /// <param name="redirectUri">RedirectUri.</param>
        /// <param name="tokenCachePath">TokenCachePath.</param>
        /// <param name="externalOrgWebProxyClient">ExternalOrgWebProxyClient.</param>
        /// <param name="promptBehavior">PromptBehavior</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string crmUserId, SecureString crmPassword, string crmRegion, string orgName, bool useUniqueInstance, OrganizationDetail orgDetail, UserIdentifier user, string clientId, Uri redirectUri, string tokenCachePath, OrganizationWebProxyClient externalOrgWebProxyClient, PromptBehavior promptBehavior = PromptBehavior.Auto)
        {
            CrmServiceClient crmService = new CrmServiceClient(crmUserId, crmPassword, crmRegion, orgName, useUniqueInstance, orgDetail, user, clientId, redirectUri, tokenCachePath, externalOrgWebProxyClient, promptBehavior);

            return GetOrgaizationService(crmService);
        }

        /// <summary>
        /// Gets the IOrganizationService from a CrmServiceClient (2016) instance using OAuth.
        /// </summary>
        /// <param name="crmUserId">CRMUserId.</param>
        /// <param name="crmPassword">CRMPassword.</param>
        /// <param name="domain">Domain</param>
        /// <param name="homeRealm">HomeRealm</param>
        /// <param name="hostName">HostName</param>
        /// <param name="port">Port</param>
        /// <param name="orgName">OrgName.</param>
        /// <param name="useSsl">UseSSL</param>
        /// <param name="useUniqueInstance">UseUniqueInstance.</param>
        /// <param name="orgDetail">OrgDetail.</param>
        /// <param name="user">User.</param>
        /// <param name="clientId">ClientId.</param>
        /// <param name="redirectUri">RedirectUri.</param>
        /// <param name="tokenCachePath">TokenCachePath.</param>
        /// <param name="externalOrgWebProxyClient">ExternalOrgWebProxyClient.</param>
        /// <param name="promptBehavior">PromptBehavior</param>
        /// <returns>IOrganizationService.</returns>
        public static IOrganizationService Get(string crmUserId, SecureString crmPassword, string domain, string homeRealm, string hostName, string port, string orgName, bool useSsl, bool useUniqueInstance, OrganizationDetail orgDetail, UserIdentifier user, string clientId, Uri redirectUri, string tokenCachePath, OrganizationWebProxyClient externalOrgWebProxyClient, PromptBehavior promptBehavior = PromptBehavior.Auto)
        {
            CrmServiceClient crmService = new CrmServiceClient(crmUserId, crmPassword, domain, homeRealm, hostName, port, orgName, useSsl, useUniqueInstance, orgDetail, user, clientId, redirectUri, tokenCachePath, externalOrgWebProxyClient, promptBehavior);

            return GetOrgaizationService(crmService);
        }

        private static IOrganizationService GetOrgaizationService(CrmServiceClient crmService)
        {
            if (!crmService.IsReady || (crmService.OrganizationWebProxyClient == null &&
                                        crmService.OrganizationServiceProxy == null))
                throw new Exception("Unable to connect to CRM");

            return crmService.OrganizationWebProxyClient ?? (IOrganizationService)crmService.OrganizationServiceProxy;
        }
    }
}
