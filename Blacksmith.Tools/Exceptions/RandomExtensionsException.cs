using System;
using System.Runtime.Serialization;

namespace Blacksmith.Tools.Exceptions
{
    public class RandomExtensionsException : Exception
    {
        public RandomExtensionsException(string message) : base(message)
        {
        }

        protected RandomExtensionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}