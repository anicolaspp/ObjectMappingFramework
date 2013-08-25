using System;

namespace MappingObjectsFramework
{
    public class InvalidMapException : Exception
    {
        public InvalidMapException(string msg)
            : base(msg)
        {

        }

        public InvalidMapException(string msg, Exception innerException)
            : base(msg, innerException)
        {

        }
    }
}