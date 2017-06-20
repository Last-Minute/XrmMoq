namespace XrmMoq.Helpers
{
    /// <summary>
    /// Helper method to create the Message Name for a plug-in from the enum value.
    /// </summary>
    public static class CreateMessageName
    {
        /// <summary>
        /// Gets an out of the box Message Name value.
        /// </summary>
        /// <param name="messageName">MessageName enum.</param>
        /// <returns>System.String.</returns>
        public static string GetExisting(MessageName messageName)
        {
            return messageName.ToString();
        }

        /// <summary>
        /// Gets a custom Message Name value.
        /// </summary>
        /// <param name="messageName">Custom message name.</param>
        /// <returns>System.String.</returns>
        public static string GetCustom(string messageName)
        {
            return messageName;
        }
    }
}