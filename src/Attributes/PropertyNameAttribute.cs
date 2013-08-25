using System;

namespace MappingObjectsFramework
{
    /// <summary>
    /// In order to map the properties between the two objects, this attribute mark the name of the property to searsh for
    /// </summary>
    public class MapPropertyNameAttribute : Attribute
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the property
        /// </summary>
        public Type Type { get; set; }
    }
}