using System;
using System.Collections.Generic;

namespace MappingObjectsFramework
{
    public class StandartConverterResolver : ConverterResolver
    {
        public StandartConverterResolver(bool useDefaultConverters) : base(useDefaultConverters)
        {
        }

        public StandartConverterResolver(bool useDefaultConverters, IEnumerable<Converter> converters) : base(useDefaultConverters)
        {
            if (converters == null)
            {
                throw new ArgumentNullException("converters");
            }

            foreach (var converter in converters)
            {
                Add(converter);
            }
        }

        public override K Convert<T, K>(T value)
        {
            Type inputType = typeof(T);
            Type outputType = typeof(K);

            Converter converter = null;

            foreach (var itemConverter in this)
            {
                if (itemConverter.From == inputType && itemConverter.To == outputType)
                {
                    converter = itemConverter;
                    break;
                }
            }

            if (converter != null)
                return (K) converter.Convert(value);

            if (UseDefaultConverter)
            {
                if (inputType != outputType)
                {
                    throw new InvalidMapException(string.Format("No mapping function was found to map {0} to {1} and can no use defaul mapping with different types", inputType.Name, outputType.Name));
                }

                DefaultConverter defaultConverter = new DefaultConverter(inputType);
                return (K) defaultConverter.Convert(value);
            }
            
            //no mapping function
            throw new InvalidMapException(string.Format("No mapping function was found to map {0} to {1}.", inputType.Name, outputType.Name));
        }
    }
}