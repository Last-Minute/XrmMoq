using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Moq;
using System;
using System.Linq;
using XrmMoq.Helpers;

namespace XrmMoq
{
    /// <summary>
    /// Used to execute a plug-in offline using simulated settings.
    /// </summary>
    public class CrmPluginMock
    {
        private readonly Mock<IOrganizationService> _serviceMock;
        private readonly Mock<IOrganizationServiceFactory> _factoryMock;
        private readonly Mock<IPluginExecutionContext> _pluginContextMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private Entity _targetEntity;
        private EntityReference _targetEntityReference;

        public CrmPluginMock()
        {
            _serviceMock = new Mock<IOrganizationService>();
            _factoryMock = new Mock<IOrganizationServiceFactory>();
            Mock<ITracingService> tracingServiceMock = new Mock<ITracingService>();
            _pluginContextMock = new Mock<IPluginExecutionContext>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            PluginExecutionContext = new FakePluginExecutionContext();
            tracingServiceMock = Trace.CreateTracingService(tracingServiceMock);
            FakeTracingService = tracingServiceMock.Object;
        }

        /// <summary>
        /// Fake PluginExecutionContext.
        /// </summary>
        /// <value>The plugin execution context.</value>
        public FakePluginExecutionContext PluginExecutionContext { get; set; }

        /// <summary>
        /// Fake Plug-in PreImage.
        /// </summary>
        /// <value>The plug-in pre image.</value>
        public PreImage PluginPreImage { get; set; }

        /// <summary>
        /// Fake Plug-in PostImage.
        /// </summary>
        /// <value>The plug-in post image.</value>
        public PostImage PluginPostImage { get; set; }

        /// <summary>
        /// Fake plug-in Unsecure Configuration.
        /// </summary>
        /// <value>The unsecure configuration.</value>
        public string UnsecureConfiguration { get; set; }

        /// <summary>
        /// Fake plug-in Secure Configuration.
        /// </summary>
        /// <value>The secure configuration.</value>
        public string SecureConfiguration { get; set; }

        /// <summary>
        /// Fake CRM OrganizationService.
        /// </summary>
        /// <value>The fake organization service.</value>
        public IOrganizationService FakeOrganizationService => _serviceMock.Object;

        /// <summary>
        /// Real CRM OrganizationService.
        /// </summary>
        /// <value>The live organization service.</value>
        public IOrganizationService LiveOrganizationService { get; set; }

        /// <summary>
        /// Fake CRM TracingService.
        /// </summary>
        /// <value>The fake tracing service.</value>
        public ITracingService FakeTracingService { get; set; }

        public IServiceProvider FakeServiceProvider => _serviceProviderMock.Object;

        /// <summary>
        /// Sets the mock EntityCollection values for RetrieveMultiple operations in the order they are to be executed.
        /// </summary>
        /// <param name="results">The EntityCollection values.</param>
        public void SetMockRetrieveMultiples(params EntityCollection[] results)
        {
            MockSequence sequence = new MockSequence();
            foreach (EntityCollection result in results)
            {
                _serviceMock.InSequence(sequence).Setup(t => t.RetrieveMultiple(It.IsAny<QueryBase>())).Returns(result);
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
                _serviceMock.InSequence(sequence).Setup(t => t.Retrieve(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<ColumnSet>())).Returns(result);
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
                _serviceMock.InSequence(sequence).Setup(t => t.Create(It.IsAny<Entity>())).Returns(result);
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
                _serviceMock.InSequence(sequence).Setup(t => t.Execute(It.IsAny<OrganizationRequest>())).Returns(result);
            }
        }

        /// <summary>
        /// Sets the Target Entity for the PluginExecutionContext. 
        /// </summary>
        /// <param name="target">The target entity.</param>
        public void SetTarget(ref Entity target)
        {
            _targetEntity = target;
        }

        /// <summary>
        /// Sets the Target EntityReference for the PluginExecutionContext.
        /// </summary>
        /// <param name="target">The target.</param>
        public void SetTarget(EntityReference target)
        {
            _targetEntityReference = target;
        }

        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="Exception">Error invoking plug-in</exception>
        public void Execute<T>()
        {
            IOrganizationService service = LiveOrganizationService ?? _serviceMock.Object;

            _factoryMock.Setup(t => t.CreateOrganizationService(It.IsAny<Guid>())).Returns(service);
            IOrganizationServiceFactory factory = _factoryMock.Object;

            CreatePluginContext();

            IPluginExecutionContext pluginContext = _pluginContextMock.Object;

            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(ITracingService)))).Returns(FakeTracingService);
            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IOrganizationServiceFactory)))).Returns(factory);
            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IPluginExecutionContext)))).Returns(pluginContext);

            IServiceProvider serviceProvider = _serviceProviderMock.Object;

            IPlugin testClass = CreateTestPlugin<T>();

            if (testClass != null)
                testClass.Execute(serviceProvider);
            else
                throw new Exception("Error invoking plug-in");
        }

        /// <summary>
        /// Executes a method in the plug-in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The method parameters.</param>
        /// <returns>The method return value.</returns>
        public object ExecuteMethod<T>(string methodName, params object[] parameters)
        {
            object testClass = CreateTestClass<T>();

            var method = typeof(T).GetMethod(methodName);
            return method.Invoke(testClass, parameters);
        }

        /// <summary>
        /// Creates the plug-in context.
        /// </summary>
        private void CreatePluginContext()
        {
            _pluginContextMock.Setup(t => t.Mode).Returns(PluginExecutionContext.Mode);
            _pluginContextMock.Setup(t => t.IsolationMode).Returns(PluginExecutionContext.IsolationMode);
            _pluginContextMock.Setup(t => t.Depth).Returns(PluginExecutionContext.Depth);
            _pluginContextMock.Setup(t => t.MessageName).Returns(PluginExecutionContext.MessageName);
            _pluginContextMock.Setup(t => t.PrimaryEntityName).Returns(PluginExecutionContext.PrimaryEntityName);
            _pluginContextMock.Setup(t => t.RequestId).Returns(PluginExecutionContext.RequestId);
            _pluginContextMock.Setup(t => t.SecondaryEntityName).Returns(PluginExecutionContext.SecondaryEntityName);

            if (_targetEntity != null && _targetEntityReference != null)
                throw new Exception("Can only set either the Entity or the EntityReference as the Target");

            if (!PluginExecutionContext.InputParameters.ContainsKey("Target") && _targetEntity != null)
                PluginExecutionContext.InputParameters.Add("Target", _targetEntity);
            if (!PluginExecutionContext.InputParameters.ContainsKey("Target") && _targetEntityReference != null)
                PluginExecutionContext.InputParameters.Add("Target", _targetEntityReference);
            _pluginContextMock.Setup(t => t.InputParameters).Returns(PluginExecutionContext.InputParameters);

            _pluginContextMock.Setup(t => t.OutputParameters).Returns(PluginExecutionContext.OutputParameters);
            _pluginContextMock.Setup(t => t.SharedVariables).Returns(PluginExecutionContext.SharedVariables);
            _pluginContextMock.Setup(t => t.UserId).Returns(PluginExecutionContext.UserId);
            _pluginContextMock.Setup(t => t.InitiatingUserId).Returns(PluginExecutionContext.InitiatingUserId);
            _pluginContextMock.Setup(t => t.BusinessUnitId).Returns(PluginExecutionContext.BusinessUnitId);
            _pluginContextMock.Setup(t => t.OrganizationId).Returns(PluginExecutionContext.OrganizationId);
            _pluginContextMock.Setup(t => t.OrganizationName).Returns(PluginExecutionContext.OrganizationName);
            _pluginContextMock.Setup(t => t.PrimaryEntityId).Returns(PluginExecutionContext.PrimaryEntityId);

            if (!PluginExecutionContext.PreEntityImages.Any() && PluginPreImage != null)
                PluginExecutionContext.PreEntityImages.Add(PluginPreImage.Name, PluginPreImage.Entity);
            _pluginContextMock.Setup(t => t.PreEntityImages).Returns(PluginExecutionContext.PreEntityImages);
            if (!PluginExecutionContext.PostEntityImages.Any() && PluginPostImage != null)
                PluginExecutionContext.PostEntityImages.Add(PluginPostImage.Name, PluginPostImage.Entity);
            _pluginContextMock.Setup(t => t.PostEntityImages).Returns(PluginExecutionContext.PostEntityImages);

            _pluginContextMock.Setup(t => t.OwningExtension).Returns(PluginExecutionContext.OwningExtension);
            _pluginContextMock.Setup(t => t.CorrelationId).Returns(PluginExecutionContext.CorrelationId);
            _pluginContextMock.Setup(t => t.IsExecutingOffline).Returns(PluginExecutionContext.IsExecutingOffline);
            _pluginContextMock.Setup(t => t.IsOfflinePlayback).Returns(PluginExecutionContext.IsOfflinePlayback);
            _pluginContextMock.Setup(t => t.IsInTransaction).Returns(PluginExecutionContext.IsInTransaction);
            _pluginContextMock.Setup(t => t.OperationId).Returns(PluginExecutionContext.OperationId);
            _pluginContextMock.Setup(t => t.OperationCreatedOn).Returns(PluginExecutionContext.OperationCreatedOn);
            _pluginContextMock.Setup(t => t.Stage).Returns(PluginExecutionContext.Stage);
            _pluginContextMock.Setup(t => t.ParentContext).Returns(PluginExecutionContext.ParentContext);
        }

        /// <summary>
        /// Creates the IPlugin test class based on the plug-in's constructor for testing the plug-in end to end.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IPlugin.</returns>
        private IPlugin CreateTestPlugin<T>()
        {
            Type type = typeof(T);
            var constructors = type.GetConstructors();
            var parameters = constructors[0].GetParameters();

            IPlugin testClass;
            switch (parameters.Length)
            {
                case 1:
                    testClass = Activator.CreateInstance(typeof(T), UnsecureConfiguration) as IPlugin;
                    break;
                case 2:
                    testClass = Activator.CreateInstance(typeof(T), UnsecureConfiguration, SecureConfiguration) as IPlugin;
                    break;
                default:
                    testClass = Activator.CreateInstance(typeof(T)) as IPlugin;
                    break;
            }
            return testClass;
        }

        /// <summary>
        /// Creates the test class based on the plug-in's constructor for testing a single plug-in method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>New class instance T.</returns>
        private T CreateTestClass<T>()
        {
            Type type = typeof(T);
            var constructors = type.GetConstructors();
            var parameters = constructors[0].GetParameters();

            T testClass;
            switch (parameters.Length)
            {
                case 1:
                    testClass = (T)Activator.CreateInstance(typeof(T), UnsecureConfiguration);
                    break;
                case 2:
                    testClass = (T)Activator.CreateInstance(typeof(T), UnsecureConfiguration, SecureConfiguration);
                    break;
                default:
                    testClass = (T)Activator.CreateInstance(typeof(T));
                    break;
            }
            return testClass;
        }
    }
}