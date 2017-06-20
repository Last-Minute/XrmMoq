namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper method to create the Plug-in Mode for an assembly from the enum value.
    /// </summary>
    public static class CreatePluginMode
    {
        /// <summary>
        /// Gets the specified PLug-in Mode value.
        /// </summary>
        /// <param name="pluginMode">PluginMode enum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(PluginMode pluginMode)
        {
            return (int)pluginMode;
        }
    }
}