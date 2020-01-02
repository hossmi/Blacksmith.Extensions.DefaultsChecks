using System;
using System.Runtime.Serialization;

namespace Blacksmith.Exceptions
{
    [Serializable]
    public class NullResultFromDelegateMethodException : ArgumentNullException
    {
        public NullResultFromDelegateMethodException(string paramName) : base(paramName)
        {
        }

        protected NullResultFromDelegateMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}