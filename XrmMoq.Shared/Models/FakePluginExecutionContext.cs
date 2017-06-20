using Microsoft.Xrm.Sdk;
using System;

namespace XrmMoq
{
    /// <summary>
    /// Fake PluginExecutionContext used for testing or debugging.
    /// </summary>
    /// <seealso cref="Microsoft.Xrm.Sdk.IPluginExecutionContext" />
    public class FakePluginExecutionContext : IPluginExecutionContext
    {
        public FakePluginExecutionContext()
        {
            InputParameters = new ParameterCollection();
            OutputParameters = new ParameterCollection();
            SharedVariables = new ParameterCollection();
            PreEntityImages = new EntityImageCollection();
            PostEntityImages = new EntityImageCollection();
        }

        /// <summary>
        /// Gets the mode of plug-in execution.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }
        /// <summary>
        /// Gets a value indicating if the plug-in is executing in the sandbox mode or not.
        /// </summary>
        /// <value>The isolation mode.</value>
        public int IsolationMode { get; set; } = 2; //Default to Sandbox
        /// <summary>
        /// Gets the current depth of execution in the call stack.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth { get; set; } = 1; //Default to 1
        /// <summary>
        /// Gets the name of the Web service message that is being processed by the event execution pipeline.
        /// </summary>
        /// <value>The name of the message.</value>
        public string MessageName { get; set; }
        /// <summary>
        /// Gets the name of the primary entity for which the pipeline is processing events.
        /// </summary>
        /// <value>The name of the primary entity.</value>
        public string PrimaryEntityName { get; set; }
        /// <summary>
        /// Gets the GUID of the request being processed by the event execution pipeline.
        /// </summary>
        /// <value>The request identifier.</value>
        public Guid? RequestId { get; set; }
        /// <summary>
        /// Gets the name of the secondary entity that has a relationship with the primary entity.
        /// </summary>
        /// <value>The name of the secondary entity.</value>
        public string SecondaryEntityName { get; set; }
        /// <summary>
        /// Gets the parameters of the request message that triggered the event that caused the plug-in to execute.
        /// </summary>
        /// <value>The input parameters.</value>
        public ParameterCollection InputParameters { get; set; }
        /// <summary>
        /// Gets the parameters of the response message after the core platform operation has completed.
        /// </summary>
        /// <value>The output parameters.</value>
        public ParameterCollection OutputParameters { get; set; }
        /// <summary>
        /// Gets the custom properties that are shared between plug-ins.
        /// </summary>
        /// <value>The shared variables.</value>
        public ParameterCollection SharedVariables { get; set; }
        /// <summary>
        /// Gets the GUID of the system user for whom the plug-in invokes web service methods on behalf of.
        /// </summary>
        /// <value>The user identifier.</value>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets the GUID of the system user account under which the current pipeline is executing.
        /// </summary>
        /// <value>The initiating user identifier.</value>
        public Guid InitiatingUserId { get; set; }
        /// <summary>
        /// Gets the GUIDGUID of the business unit that the user making the request, also known as the calling user, belongs to.
        /// </summary>
        /// <value>The business unit identifier.</value>
        public Guid BusinessUnitId { get; set; }
        /// <summary>
        /// Gets the GUID of the organization that the entity belongs to and the plug-in executes under.
        /// </summary>
        /// <value>The organization identifier.</value>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// Gets the unique name of the organization that the entity currently being processed belongs to and the plug-in executes under.
        /// </summary>
        /// <value>The name of the organization.</value>
        public string OrganizationName { get; set; }
        /// <summary>
        /// Gets the GUID of the primary entity for which the pipeline is processing events.
        /// </summary>
        /// <value>The primary entity identifier.</value>
        public Guid PrimaryEntityId { get; set; }
        /// <summary>
        /// Gets the properties of the primary entity before the core platform operation has begins.
        /// </summary>
        /// <value>The pre entity images.</value>
        public EntityImageCollection PreEntityImages { get; set; }
        /// <summary>
        /// Gets the properties of the primary entity after the core platform operation has been completed.
        /// </summary>
        /// <value>The post entity images.</value>
        public EntityImageCollection PostEntityImages { get; set; }
        /// <summary>
        /// Gets the properties of the primary entity after the core platform operation has been completed.
        /// </summary>
        /// <value>The owning extension.</value>
        public EntityReference OwningExtension { get; set; }
        /// <summary>
        /// Gets the GUID for tracking plug-in or custom workflow activity execution.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public Guid CorrelationId { get; set; }
        /// <summary>
        /// Gets whether the plug-in is executing from the Microsoft Dynamics 365 for Microsoft Office Outlook with Offline Access client while it is offline.
        /// </summary>
        /// <value><c>true</c> if this instance is executing offline; otherwise, <c>false</c>.</value>
        public bool IsExecutingOffline { get; set; }
        /// <summary>
        /// Gets a value indicating if the plug-in is executing as a result of the Microsoft Dynamics 365 for Microsoft Office Outlook with Offline Access client transitioning from offline to online and synchronizing with the Microsoft Dynamics 365 server.
        /// </summary>
        /// <value>True if this instance is offline playback; otherwise, false.</value>
        public bool IsOfflinePlayback { get; set; }
        /// <summary>
        /// Gets a value indicating if the plug-in is executing within the database transaction.
        /// </summary>
        /// <value>True if this instance is in transaction; otherwise, false.</value>
        public bool IsInTransaction { get; set; }
        /// <summary>
        /// Gets the GUID of the related <see langword="System Job" />.
        /// </summary>
        /// <value>The operation identifier.</value>
        public Guid OperationId { get; set; }
        /// <summary>
        /// Gets the date and time that the related <see langword="System Job" /> was created.
        /// </summary>
        /// <value>The operation created on.</value>
        public DateTime OperationCreatedOn { get; set; }
        /// <summary>
        /// Gets the stage in the execution pipeline that a synchronous plug-in is registered for.
        /// </summary>
        /// <value>The stage.</value>
        public int Stage { get; set; }
        /// <summary>
        /// Gets the execution context from the parent pipeline operation.
        /// </summary>
        /// <value>The parent context.</value>
        public IPluginExecutionContext ParentContext { get; set; }
    }
}