using Microsoft.Xrm.Sdk;

namespace XrmMoq
{
    /// <summary>
    /// Represents the Pre Image registered in the plug-in registration tool.
    /// </summary>
    public class PreImage
    {
        public PreImage(string name, Entity entity)
        {
            Name = name;
            Entity = entity;
        }

        /// <summary>
        /// Gets or sets the name of the Pre Image.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity of the Pre Image.
        /// </summary>
        /// <value>The entity.</value>
        public Entity Entity { get; set; }
    }
}
