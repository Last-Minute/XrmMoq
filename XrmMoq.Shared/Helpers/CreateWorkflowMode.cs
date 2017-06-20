namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper method to create the Workflow Mode for a workflow from the enum value.
    /// </summary>
    public static class CreateWorkflowMode
    {
        /// <summary>
        /// Gets the specified Workflow Mode value.
        /// </summary>
        /// <param name="workflowMode">WorkflowMode enum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(WorkflowMode workflowMode)
        {
            return (int)workflowMode;
        }
    }
}