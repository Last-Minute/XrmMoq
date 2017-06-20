namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper method to create the Isolation Mode for an assembly from the enum value.
    /// </summary>
    public static class CreateIsolationMode
    {
        /// <summary>
        /// Gets the specified Isolation Mode value.
        /// </summary>
        /// <param name="isolationMode">IsolationMode enum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(IsolationMode isolationMode)
        {
            return (int)isolationMode;
        }
    }
}