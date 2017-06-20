using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Organization;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;
using AuthenticationType = Microsoft.Xrm.Tooling.Connector.AuthenticationType;

/// <summary>
/// Adapter used in CRM development requiring a CRM connection using Microsoft.Xrm.Tooling.Connector.CrmServiceClient.
/// Using the adapter in place of the actual CrmServiceClient allows CRM requests to be mocked.
/// </summary>
public class CrmServiceClientAdapter : ICrmServiceClient, IOrganizationService, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrmServiceClientAdapter"/> class for use in development.
    /// </summary>
    /// <param name="crmServiceClient">An existing CrmServiceClient object.</param>
    public CrmServiceClientAdapter(CrmServiceClient crmServiceClient)
    {
        _crmServiceClient = crmServiceClient;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrmServiceClientAdapter"/> class for use in testing.
    /// </summary>
    public CrmServiceClientAdapter()
    {
    }

    private readonly CrmServiceClient _crmServiceClient;

    #region Properties
    /// <summary>
    /// Returns the Last String Error that was created by the CRM Connection.
    /// </summary>
    /// <value>Last String Error.</value>
    public virtual string LastCrmError { get; set; }

    /// <summary>
    /// Exposed OrganizationWebProxyClient for consumers.
    /// </summary>
    /// <value>OrganizationWebProxyClient.</value>
    public virtual OrganizationWebProxyClient OrganizationWebProxyClient { get; set; }

    /// <summary>
    /// Exposed OrganizationServiceProxy for consumers.
    /// </summary>
    /// <value>OrganizationServiceProxy.</value>
    public virtual OrganizationServiceProxy OrganizationServiceProxy { get; set; }

    /// <summary>
    /// Authentication Type to use.
    /// </summary>
    /// <value>Authentication Type.</value>
    public virtual AuthenticationType ActiveAuthenticationType { get; set; }

    /// <summary>
    /// If true the service is ready to accept requests.
    /// </summary>
    /// <value>Is Service Ready.</value>
    public virtual bool IsReady { get; set; }

    /// <summary>
    /// OAuth Authority.
    /// </summary>
    /// <value>OAuth Authority.</value>
    public virtual string Authority { get; set; }

    /// <summary>
    /// If true then Batch Operations are available.
    /// </summary>
    /// <value>Is Batch Operations Available.</value>
    public virtual bool IsBatchOperationsAvailable { get; set; }

    /// <summary>
    /// Returns the Last Exception from CRM.
    /// </summary>
    /// <value>:ast CRM exception.</value>
    public virtual Exception LastCrmException { get; set; }

    /// <summary>
    /// Logged in Office365 UserId using OAuth.
    /// </summary>
    /// <value>Logged in Office365 UserId.</value>
    public virtual string OAuthUserId { get; set; }

    /// <summary>
    /// Returns the Actual URI used to connect to CRM. this URI could be influenced by user defined variables.
    /// </summary>
    /// <value>Actual CRM URI.</value>
    public virtual Uri CrmConnectOrgUriActual { get; set; }

    /// <summary>
    /// Returns the friendly name of the connected org.
    /// </summary>
    /// <value>Friendly org. name.</value>
    public virtual string ConnectedOrgFriendlyName { get; set; }

    /// <summary>
    /// Returns the unique name for the org that has been connected.
    /// </summary>
    /// <value>Unique org. name.</value>
    public virtual string ConnectedOrgUniqueName { get; set; }

    /// <summary>
    /// Returns the endpoint collection for the connected org.
    /// </summary>
    /// <value>Published endpoints.</value>
    public virtual EndpointCollection ConnectedOrgPublishedEndpoints { get; set; }

    /// <summary>
    /// This is the connection lock object that is used to control connection access for various threads. This should be used if you are using the CRM queries via Linq to lock the connection.
    /// </summary>
    /// <value>Connection lock object.</value>
    public virtual object ConnectionLockObject { get; set; }

    /// <summary>
    /// Returns the Version Number of the connected CRM organization. If access before the Organization is connected, value returned will be null or 0.0.
    /// </summary>
    /// <value>Connected org version.</value>
    public virtual Version ConnectedOrgVersion { get; set; }

    /// <summary>
    /// Gets or Sets the current caller ID.
    /// </summary>
    /// <value>Caller identifier.</value>
    public virtual Guid CallerId { get; set; }

    /// <summary>
    /// Get the Client SDK version property.
    /// </summary>
    /// <value>SDK version property.</value>
    public virtual string SdkVersionProperty { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Creates a record.
    /// </summary>
    /// <param name="entity">An entity instance that contains the properties to set in the newly created record.</param>
    /// <returns>Guid.</returns>
    public virtual Guid Create(Entity entity)
    {
        return _crmServiceClient.Create(entity);
    }

    /// <summary>
    /// Retrieves a record.
    /// </summary>
    /// <param name="entityName">The logical name of the entity that is specified in the entityId parameter.</param>
    /// <param name="id">The ID of the record that you want to retrieve.</param>
    /// <param name="columnSet">A query that specifies the set of columns, or attributes, to retrieve.</param>
    /// <returns>The requested entity.</returns>
    public virtual Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
    {
        return _crmServiceClient.Retrieve(entityName, id, columnSet);
    }

    /// <summary>
    /// Updates an existing record.
    /// </summary>
    /// <param name="entity">An entity instance that has one or more properties set to be updated in the record.</param>
    public virtual void Update(Entity entity)
    {
    }

    /// <summary>
    /// Deletes a record.
    /// </summary>
    /// <param name="entityName">The logical name of the entity specified in the entityId parameter.</param>
    /// <param name="id">The ID of the record to delete.</param>
    public virtual void Delete(string entityName, Guid id)
    {
    }

    /// <summary>
    /// Executes a message in the form of a request, and returns a response.
    /// </summary>
    /// <param name="request">A request instance that defines the action to be performed.</param>
    /// <returns>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
    public virtual OrganizationResponse Execute(OrganizationRequest request)
    {
        return _crmServiceClient.Execute(request);
    }

    /// <summary>
    /// Creates a link between records. 
    /// </summary>
    /// <param name="entityName">The logical name of the entity specified in the entityId parameter.</param>
    /// <param name="entityId">The ID of the record to which the related records will be associated.</param>
    /// <param name="relationship">The name of the relationship to be used to create the link.</param>
    /// <param name="relatedEntities">A collection of entity references (references to records) to be associated.</param>
    public virtual void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
    {
    }

    /// <summary>
    /// Deletes a link between records.
    /// </summary>
    /// <param name="entityName">The logical name of the entity specified in the entityId parameter.</param>
    /// <param name="entityId">The ID of the record from which the related records will be disassociated.</param>
    /// <param name="relationship">The name of the relationship to be used to remove the link.</param>
    /// <param name="relatedEntities">A collection of entity references (references to records) to be disassociated.</param>
    public virtual void Disassociate(string entityName, Guid entityId, Relationship relationship,
        EntityReferenceCollection relatedEntities)
    {
    }

    /// <summary>
    /// Retrieves a collection of records.
    /// </summary>
    /// <param name="query">A query that determines the set of records to retrieve.</param>
    /// <returns>The collection of entities returned from the query.</returns>
    public virtual EntityCollection RetrieveMultiple(QueryBase query)
    {
        return _crmServiceClient.RetrieveMultiple(query);
    }

    public void Dispose()
    {
        _crmServiceClient.Dispose();
    }
    #endregion
}

internal interface ICrmServiceClient
{
    Guid Create(Entity entity);
    Entity Retrieve(string entityName, Guid id, ColumnSet columnSet);
    void Update(Entity entity);
    void Delete(string entityName, Guid id);
    OrganizationResponse Execute(OrganizationRequest request);
    void Associate(string entityName, Guid entityId, Relationship relationship,
        EntityReferenceCollection relatedEntities);
    void Disassociate(string entityName, Guid entityId, Relationship relationship,
        EntityReferenceCollection relatedEntities);
    EntityCollection RetrieveMultiple(QueryBase query);
}