using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

/// <summary>
/// Adapter used in CRM development requiring a CRM connection using Microsoft.Xrm.Client.CrmConnection.
/// Using the adapter in place of the actual OrganizationService allows CRM requests to be mocked.
/// </summary>
public class CrmConnectionAdapter : ICrmOrganizationService, IOrganizationService, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrmConnectionAdapter"/> class (Microsoft.Xrm.Client.CrmConnection) for use in development.
    /// </summary>
    /// <param name="organizationService">An existing OrganizationService object.</param>
    public CrmConnectionAdapter(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CrmConnectionAdapter"/> class (Microsoft.Xrm.Client.CrmConnection) for use in testing.
    /// </summary>
    public CrmConnectionAdapter()
    {

    }

    private readonly OrganizationService _organizationService;

    #region Methods
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
    /// Creates a record.
    /// </summary>
    /// <param name="entity">An entity instance that contains the properties to set in the newly created record.</param>
    /// <returns>Guid.</returns>
    public virtual Guid Create(Entity entity)
    {
        return _organizationService.Create(entity);
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
    /// Deletes a link between records.
    /// </summary>
    /// <param name="entityName">The logical name of the entity specified in the entityId parameter.</param>
    /// <param name="entityId">The ID of the record from which the related records will be disassociated.</param>
    /// <param name="relationship">The name of the relationship to be used to remove the link.</param>
    /// <param name="relatedEntities">A collection of entity references (references to records) to be disassociated.</param>
    public virtual void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
    {
    }

    /// <summary>
    /// Executes a message in the form of a request, and returns a response.
    /// </summary>
    /// <param name="request">A request instance that defines the action to be performed.</param>
    /// <returns>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
    public virtual OrganizationResponse Execute(OrganizationRequest request)
    {
        return _organizationService.Execute(request);
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
        return _organizationService.Retrieve(entityName, id, columnSet);
    }

    /// <summary>
    /// Retrieves a collection of records.
    /// </summary>
    /// <param name="query">A query that determines the set of records to retrieve.</param>
    /// <returns>The collection of entities returned from the query.</returns>
    public virtual EntityCollection RetrieveMultiple(QueryBase query)
    {
        return _organizationService.RetrieveMultiple(query);
    }

    /// <summary>
    /// Updates an existing record.
    /// </summary>
    /// <param name="entity">An entity instance that has one or more properties set to be updated in the record.</param>
    public virtual void Update(Entity entity)
    {
    }

    public virtual void Dispose()
    {
        _organizationService.Dispose();
    }
    #endregion
}

internal interface ICrmOrganizationService
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