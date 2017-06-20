namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper method to create the Stage for an assembly from the enum value.
    /// </summary>
    public static class CreateStage
    {
        /// <summary>
        /// Gets the specified Stage value.
        /// </summary>
        /// <param name="stage">Stage enum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(Stage stage)
        {
            return (int)stage;
        }
    }
}