using System.Collections;
using System.Collections.Generic;

namespace MappingObjectsFramework
{
    public abstract class ConverterResolver : IEnumerable<Converter>
    {
        private List<Converter> _converters; 
        public bool UseDefaultConverter { get; protected set; }
        

        protected ConverterResolver(bool useDefaultConverters)
        {
            UseDefaultConverter = useDefaultConverters;
            _converters = new List<Converter>();
        }

        public abstract K Convert<T, K>(T value);

        public bool Add(Converter converter)
        {
            if (_converters.Contains(converter))
                return false;

            _converters.Add(converter);
            return true;
        }

        public bool Remove(Converter converter)
        {
            return _converters.Remove(converter);
        }

        public IEnumerator<Converter> GetEnumerator()
        {
            return _converters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}