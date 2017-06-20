using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Moq;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using XrmMoq.Helpers;

namespace XrmMoq
{
    /// <summary>
    /// Used to execute a workflow offline using simulated settings.
    /// </summary>
    public class CrmWorkflowMock
    {
        private readonly Mock<IOrganizationService> _serviceMock;
        private readonly Mock<IOrganizationServiceFactory> _factoryMock;
        private readonly Mock<IWorkflowContext> _workflowContextMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;

        public CrmWorkflowMock()
        {
            _serviceMock = new Mock<IOrganizationService>();
            _factoryMock = new Mock<IOrganizationServiceFactory>();
            Mock<ITracingService> tracingServiceMock = new Mock<ITracingService>();
            _workflowContextMock = new Mock<IWorkflowContext>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            WorkflowContext = new FakeWorkflowContext();
            tracingServiceMock = Trace.CreateTracingService(tracingServiceMock);
            FakeTracingService = tracingServiceMock.Object;
        }

        /// <summary>
        /// Fake WorkflowContext.
        /// </summary>
        /// <value>The workflow context.</value>
        public FakeWorkflowContext WorkflowContext { get; set; }

        /// <summary>
        /// Fake Plug-in PreImage.
        /// </summary>
        /// <value>The plugin pre image.</value>
        public PreImage PluginPreImage { get; set; }

        /// <summary>
        /// Fake Plug-in PostImage.
        /// </summary>
        /// <value>The plugin post image.</value>
        public PostImage PluginPostImage { get; set; }

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
        /// Executes the workflow.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IDictionary&lt;System.String, System.Object&gt;.</returns>
        /// <exception cref="System.Exception">Error invoking workflow</exception>
        public IDictionary<string, object> Execute<T>()
        {
            IOrganizationService service = LiveOrganizationService ?? _serviceMock.Object;

            _factoryMock.Setup(t => t.CreateOrganizationService(It.IsAny<Guid>())).Returns(service);
            IOrganizationServiceFactory factory = _factoryMock.Object;

            CreateWorkflowContext();

            IWorkflowContext workflowContext = _workflowContextMock.Object;

            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(ITracingService)))).Returns(FakeTracingService);
            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IOrganizationServiceFactory)))).Returns(factory);
            _serviceProviderMock.Setup(t => t.GetService(It.Is<Type>(i => i == typeof(IPluginExecutionContext)))).Returns(workflowContext);

            CodeActivity testClass = Activator.CreateInstance(typeof(T)) as CodeActivity;

            if (testClass == null)
                throw new Exception("Error invoking workflow");

            WorkflowInvoker invoker = new WorkflowInvoker(testClass);
            invoker.Extensions.Add(() => FakeTracingService);
            invoker.Extensions.Add(() => workflowContext);
            invoker.Extensions.Add(() => factory);

            return invoker.Invoke(workflowContext.InputParameters.ToDictionary(t => t.Key, t => t.Value));
        }

        /// <summary>
        /// Executes a method in the workflow.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The method parameters.</param>
        /// <returns>The method return value.</returns>
        public object ExecuteMethod<T>(string methodName, params object[] parameters)
        {
            object testClass = Activator.CreateInstance(typeof(T));

            var method = typeof(T).GetMethod(methodName);
            return method.Invoke(testClass, parameters);
        }

        /// <summary>
        /// Creates the WorkflowContext.
        /// </summary>
        private void CreateWorkflowContext()
        {
            _workflowContextMock.Setup(t => t.Mode).Returns(WorkflowContext.Mode);
            _workflowContextMock.Setup(t => t.IsolationMode).Returns(WorkflowContext.IsolationMode);
            _workflowContextMock.Setup(t => t.Depth).Returns(WorkflowContext.Depth);
            _workflowContextMock.Setup(t => t.MessageName).Returns(WorkflowContext.MessageName);
            _workflowContextMock.Setup(t => t.PrimaryEntityName).Returns(WorkflowContext.PrimaryEntityName);
            _workflowContextMock.Setup(t => t.RequestId).Returns(WorkflowContext.RequestId);
            _workflowContextMock.Setup(t => t.SecondaryEntityName).Returns(WorkflowContext.SecondaryEntityName);
            _workflowContextMock.Setup(t => t.InputParameters).Returns(WorkflowContext.InputParameters);
            _workflowContextMock.Setup(t => t.OutputParameters).Returns(WorkflowContext.OutputParameters);
            _workflowContextMock.Setup(t => t.SharedVariables).Returns(WorkflowContext.SharedVariables);
            _workflowContextMock.Setup(t => t.UserId).Returns(WorkflowContext.UserId);
            _workflowContextMock.Setup(t => t.InitiatingUserId).Returns(WorkflowContext.InitiatingUserId);
            _workflowContextMock.Setup(t => t.BusinessUnitId).Returns(WorkflowContext.BusinessUnitId);
            _workflowContextMock.Setup(t => t.OrganizationId).Returns(WorkflowContext.OrganizationId);
            _workflowContextMock.Setup(t => t.OrganizationName).Returns(WorkflowContext.OrganizationName);
            _workflowContextMock.Setup(t => t.PrimaryEntityId).Returns(WorkflowContext.PrimaryEntityId);

            if (!WorkflowContext.PreEntityImages.Any() && PluginPreImage != null)
                WorkflowContext.PreEntityImages.Add(PluginPreImage.Name, PluginPreImage.Entity);
            _workflowContextMock.Setup(t => t.PreEntityImages).Returns(WorkflowContext.PreEntityImages);
            if (!WorkflowContext.PostEntityImages.Any() && PluginPostImage != null)
                WorkflowContext.PostEntityImages.Add(PluginPostImage.Name, PluginPostImage.Entity);
            _workflowContextMock.Setup(t => t.PostEntityImages).Returns(WorkflowContext.PostEntityImages);

            _workflowContextMock.Setup(t => t.OwningExtension).Returns(WorkflowContext.OwningExtension);
            _workflowContextMock.Setup(t => t.CorrelationId).Returns(WorkflowContext.CorrelationId);
            _workflowContextMock.Setup(t => t.IsExecutingOffline).Returns(WorkflowContext.IsExecutingOffline);
            _workflowContextMock.Setup(t => t.IsOfflinePlayback).Returns(WorkflowContext.IsOfflinePlayback);
            _workflowContextMock.Setup(t => t.IsInTransaction).Returns(WorkflowContext.IsInTransaction);
            _workflowContextMock.Setup(t => t.OperationId).Returns(WorkflowContext.OperationId);
            _workflowContextMock.Setup(t => t.OperationCreatedOn).Returns(WorkflowContext.OperationCreatedOn);
            _workflowContextMock.Setup(t => t.ParentContext).Returns(WorkflowContext.ParentContext);
        }
    }
}