using System;

namespace MappingObjectsFramework
{
    public abstract class Converter 
    {
        public Type From { get; protected set; }
        public Type To { get; protected set; }

        protected Converter(Type from, Type to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to == null)
            {
                throw new ArgumentNullException("to");
            }

            From = from;
            To = to;
        }

        public object Convert(object value)
        {
            Type valueType = value.GetType();

            if (valueType != From)
            {
                throw new InvalidTypeException(string.Format("Expecting argument type: {0}, received type: {1}", From.Name, valueType.Name));
            }

            object result = ConvertFunction(value);

            Type resulType = result.GetType();

            if (resulType != To)
            {
                throw new InvalidTypeException(string.Format("Expecting evaluation function ConvertFunction type: {0}, evaluation result type: {1}", To.Name, resulType.Name));
            }

            return result;
        }

        protected abstract object ConvertFunction(object value);

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;

            Converter other = (Converter) obj;

            return From == other.From || To == other.To;
        }
    }
}