using System;

namespace MappingObjectsFramework
{
    public class DefaultConverter : Converter
    {
        public DefaultConverter(Type type) : base(type, type)
        {
        }

        protected override object ConvertFunction(object value)
        {
            return value;
        }
    }
}