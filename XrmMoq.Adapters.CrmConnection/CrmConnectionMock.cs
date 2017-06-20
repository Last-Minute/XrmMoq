using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Moq;
using System;

/// <summary>
/// Wrapper class for the CrmConnectionAdapter to make mocking requests easier.
/// Using the adapter in place of the actual OrganizationService allows CRM requests to be mocked.
/// </summary>
public class CrmConnectionMock
{
    private readonly Mock<CrmConnectionAdapter> _crmConnectionAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrmConnectionMock"/> class (Microsoft.Xrm.Client.CrmConnection) for use in testing.
    /// </summary>
    public CrmConnectionMock()
    {
        _crmConnectionAdapter = new Mock<CrmConnectionAdapter>();
    }

    public CrmConnectionAdapter CrmConnection2011Adapter => _crmConnectionAdapter.Object;

    #region Methods
    /// <summary>
    /// Sets the mock EntityCollection values for RetrieveMultiple operations in the order they are to be executed.
    /// </summary>
    /// <param name="results">The EntityCollection values.</param>
    public void SetMockRetrieveMultiples(params EntityCollection[] results)
    {
        MockSequence sequence = new MockSequence();
        foreach (EntityCollection result in results)
        {
            _crmConnectionAdapter.InSequence(sequence).Setup(t => t.RetrieveMultiple(It.IsAny<QueryBase>())).Returns(result);
        }
    }

    /// <summary>
    /// Sets the mock Entity values for Retrieve operations in the order they are to be executed.
    /// </summary>
    /// <param name="results">The Entity values.</param>
    public void SetMockRetrieves(params Entity[] results)
    {
        MockSequence sequence = new MockSequence();
        foreach (Entity result in results)
        {
            _crmConnectionAdapter.InSequence(sequence).Setup(t => t.Retrieve(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<ColumnSet>())).Returns(result);
        }
    }

    /// <summary>
    /// Sets the mock Guid values for Create operations in the order they are to be executed.
    /// </summary>
    /// <param name="results">The Guid Values.</param>
    public void SetMockCreates(params Guid[] results)
    {
        MockSequence sequence = new MockSequence();
        foreach (Guid result in results)
        {
            _crmConnectionAdapter.InSequence(sequence).Setup(t => t.Create(It.IsAny<Entity>())).Returns(result);
        }
    }

    /// <summary>
    /// Sets the mock OrganizationResponse values for Execute operations in the order they are to be executed.
    /// </summary>
    /// <param name="results">The OrganizationResponse values.</param>
    public void SetMockExecutes(params OrganizationResponse[] results)
    {
        MockSequence sequence = new MockSequence();
        foreach (OrganizationResponse result in results)
        {
            _crmConnectionAdapter.InSequence(sequence).Setup(t => t.Execute(It.IsAny<OrganizationRequest>()))
                .Returns(result);
        }
    }
    #endregion
}