using System;

namespace MappingObjectsFramework
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string msg) : base(msg)
        {

        }
    }
}