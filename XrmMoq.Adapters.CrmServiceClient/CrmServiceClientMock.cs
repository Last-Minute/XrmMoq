using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Organization;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Moq;
using System;
using AuthenticationType = Microsoft.Xrm.Tooling.Connector.AuthenticationType;

/// <summary>
/// Wrapper class for the CrmServiceClientAdapter to make mocking requests easier.
/// Using the adapter in place of the actual CrmServiceClient allows CRM requests to be mocked.
/// </summary>
public class CrmServiceClientMock
{
    private readonly Mock<CrmServiceClientAdapter> _crmServiceClientAdapter;
    private string _lastCrmError;
    private OrganizationWebProxyClient _organizationWebProxyClient;
    private OrganizationServiceProxy _organizationServiceProxy;
    private AuthenticationType _activeAuthenticationType;
    private bool _isReady;
    private string _authority;
    private bool _isBatchOperationsAvailable;
    private Exception _lastCrmException;
    private string _oAuthUserId;
    private Uri _crmConnectOrgUriActual;
    private string _connectedOrgFriendlyName;
    private string _connectedOrgUniqueName;
    private EndpointCollection _connectedOrgPublishedEndpoints;
    private object _connectionLockObject;
    private Version _connectedOrgVersion;
    private Guid _callerId;
    private string _sdkVersionProperty;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrmServiceClientMock"/> class for use in testing.
    /// </summary>
    public CrmServiceClientMock()
    {
        _crmServiceClientAdapter = new Mock<CrmServiceClientAdapter>();
    }

    public CrmServiceClientAdapter CrmServiceClientAdapter => _crmServiceClientAdapter.Object;

    #region Properties
    /// <summary>
    /// Returns the Last String Error that was created by the CRM Connection.
    /// </summary>
    /// <value>Last String Error.</value>
    public string LastCrmError
    {
        get => _lastCrmError;
        set
        {
            _lastCrmError = value;
            _crmServiceClientAdapter.Setup(t => t.LastCrmError).Returns(_lastCrmError);
        }
    }

    /// <summary>
    /// Exposed OrganizationWebProxyClient for consumers.
    /// </summary>
    /// <value>OrganizationWebProxyClient.</value>
    public OrganizationWebProxyClient OrganizationWebProxyClient
    {
        get => _organizationWebProxyClient;
        set
        {
            _organizationWebProxyClient = value;
            _crmServiceClientAdapter.Setup(t => t.OrganizationWebProxyClient).Returns(_organizationWebProxyClient);
        }
    }

    /// <summary>
    /// Exposed OrganizationServiceProxy for consumers.
    /// </summary>
    /// <value>OrganizationServiceProxy.</value>
    public OrganizationServiceProxy OrganizationServiceProxy
    {
        get => _organizationServiceProxy;
        set
        {
            _organizationServiceProxy = value;
            _crmServiceClientAdapter.Setup(t => t.OrganizationServiceProxy).Returns(_organizationServiceProxy);
        }
    }

    /// <summary>
    /// Authentication Type to use.
    /// </summary>
    /// <value>Authentication Type.</value>
    public AuthenticationType ActiveAuthenticationType
    {
        get => _activeAuthenticationType;
        set
        {
            _activeAuthenticationType = value;
            _crmServiceClientAdapter.Setup(t => t.ActiveAuthenticationType).Returns(_activeAuthenticationType);
        }
    }

    /// <summary>
    /// If true the service is ready to accept requests.
    /// </summary>
    /// <value>Is Service Ready.</value>
    public bool IsReady
    {
        get => _isReady;
        set
        {
            _isReady = value;
            _crmServiceClientAdapter.Setup(t => t.IsReady).Returns(_isReady);
        }
    }

    /// <summary>
    /// OAuth Authority.
    /// </summary>
    /// <value>OAuth Authority.</value>
    public string Authority
    {
        get => _authority;
        set
        {
            _authority = value;
            _crmServiceClientAdapter.Setup(t => t.Authority).Returns(_authority);
        }
    }

    /// <summary>
    /// If true then Batch Operations are available.
    /// </summary>
    /// <value>Is Batch Operations Available.</value>
    public bool IsBatchOperationsAvailable
    {
        get => _isBatchOperationsAvailable;
        set
        {
            _isBatchOperationsAvailable = value;
            _crmServiceClientAdapter.Setup(t => t.IsBatchOperationsAvailable).Returns(_isBatchOperationsAvailable);
        }
    }

    /// <summary>
    /// Returns the Last Exception from CRM.
    /// </summary>
    /// <value>:ast CRM exception.</value>
    public Exception LastCrmException
    {
        get => _lastCrmException;
        set
        {
            _lastCrmException = value;
            _crmServiceClientAdapter.Setup(t => t.LastCrmException).Returns(_lastCrmException);
        }
    }

    /// <summary>
    /// Logged in Office365 UserId using OAuth.
    /// </summary>
    /// <value>Logged in Office365 UserId.</value>
    public string OAuthUserId
    {
        get => _oAuthUserId;
        set
        {
            _oAuthUserId = value;
            _crmServiceClientAdapter.Setup(t => t.OAuthUserId).Returns(_oAuthUserId);
        }
    }

    /// <summary>
    /// Returns the Actual URI used to connect to CRM. this URI could be influenced by user defined variables.
    /// </summary>
    /// <value>Actual CRM URI.</value>
    public Uri CrmConnectOrgUriActual
    {
        get => _crmConnectOrgUriActual;
        set
        {
            _crmConnectOrgUriActual = value;
            _crmServiceClientAdapter.Setup(t => t.CrmConnectOrgUriActual).Returns(_crmConnectOrgUriActual);
        }
    }

    /// <summary>
    /// Returns the friendly name of the connected org.
    /// </summary>
    /// <value>Friendly org. name.</value>
    public string ConnectedOrgFriendlyName
    {
        get => _connectedOrgFriendlyName;
        set
        {
            _connectedOrgFriendlyName = value;
            _crmServiceClientAdapter.Setup(t => t.ConnectedOrgFriendlyName).Returns(_connectedOrgFriendlyName);
        }
    }

    /// <summary>
    /// Returns the unique name for the org that has been connected.
    /// </summary>
    /// <value>Unique org. name.</value>
    public string ConnectedOrgUniqueName
    {
        get => _connectedOrgUniqueName;
        set
        {
            _connectedOrgUniqueName = value;
            _crmServiceClientAdapter.Setup(t => t.ConnectedOrgUniqueName).Returns(_connectedOrgUniqueName);
        }
    }

    /// <summary>
    /// Returns the endpoint collection for the connected org.
    /// </summary>
    /// <value>Published endpoints.</value>
    public EndpointCollection ConnectedOrgPublishedEndpoints
    {
        get => _connectedOrgPublishedEndpoints;
        set
        {
            _connectedOrgPublishedEndpoints = value;
            _crmServiceClientAdapter.Setup(t => t.ConnectedOrgPublishedEndpoints).Returns(_connectedOrgPublishedEndpoints);
        }
    }

    /// <summary>
    /// This is the connection lock object that is used to control connection access for various threads. This should be used if you are using the CRM queries via Linq to lock the connection.
    /// </summary>
    /// <value>Connection lock object.</value>
    public object ConnectionLockObject
    {
        get => _connectionLockObject;
        set
        {
            _connectionLockObject = value;
            _crmServiceClientAdapter.Setup(t => t.ConnectionLockObject).Returns(_connectionLockObject);
        }
    }

    /// <summary>
    /// Returns the Version Number of the connected CRM organization. If access before the Organization is connected, value returned will be null or 0.0.
    /// </summary>
    /// <value>Connected org version.</value>
    public Version ConnectedOrgVersion
    {
        get => _connectedOrgVersion;
        set
        {
            _connectedOrgVersion = value;
            _crmServiceClientAdapter.Setup(t => t.ConnectedOrgVersion).Returns(_connectedOrgVersion);
        }
    }

    /// <summary>
    /// Gets or Sets the current caller ID.
    /// </summary>
    /// <value>Caller identifier.</value>
    public Guid CallerId
    {
        get => _callerId;
        set
        {
            _callerId = value;
            _crmServiceClientAdapter.Setup(t => t.CallerId).Returns(_callerId);
        }
    }

    /// <summary>
    /// Get the Client SDK version property.
    /// </summary>
    /// <value>SDK version property.</value>
    public string SdkVersionProperty
    {
        get => _sdkVersionProperty;
        set
        {
            _sdkVersionProperty = value;
            _crmServiceClientAdapter.Setup(t => t.SdkVersionProperty).Returns(_sdkVersionProperty);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Sets the mock EntityCollection values for RetrieveMultiple operations in the order they are to be executed.
    /// </summary>
    /// <param name="results">The EntityCollection values.</param>
    public void SetMockRetrieveMultiples(params object[] results)
    {
        MockSequence sequence = new MockSequence();
        foreach (EntityCollection result in results)
        {
            _crmServiceClientAdapter.InSequence(sequence).Setup(t => t.RetrieveMultiple(It.IsAny<QueryBase>())).Returns(result);
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
            _crmServiceClientAdapter.InSequence(sequence).Setup(t => t.Retrieve(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<ColumnSet>())).Returns(result);
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
            _crmServiceClientAdapter.InSequence(sequence).Setup(t => t.Create(It.IsAny<Entity>())).Returns(result);
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
            _crmServiceClientAdapter.InSequence(sequence).Setup(t => t.Execute(It.IsAny<OrganizationRequest>())).Returns(result);
        }
    }
    #endregion
}