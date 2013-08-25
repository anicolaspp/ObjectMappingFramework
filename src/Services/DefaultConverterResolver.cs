using System;

namespace MappingObjectsFramework
{
    public class DefaultConverterResolver : ConverterResolver
    {
        public DefaultConverterResolver() : base(true)
        {
        }

        public override K Convert<T, K>(T value)
        {
            if (typeof(T) != typeof(K))
            {
                throw new InvalidMapException(string.Format("The argument type {0} and the result type {1} should match for DefaultConverterResolver", typeof (T), typeof (K)));
            }

            Converter converter = new DefaultConverter(typeof (T));

            K result = (K)converter.Convert(value);

            return result;
        }
    }
}