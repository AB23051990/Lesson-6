using System.Collections.Concurrent;

namespace HW.Models
{    public class InvalidPropertyException : Exception
    {
        public InvalidPropertyException()
        {
        }

        public InvalidPropertyException(string propertyName, object? property)
            : base($"Property {propertyName} is not set. Actual value was: {property}")
        {

        }
    }
}
