using Blacksmith.Tools.Extensions.Strings;
using System;
using System.Runtime.Serialization;

namespace Blacksmith.Models
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
        public BusinessException(string message, params object[] args) : base(message.f(args))
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static implicit operator BusinessException(string message)
        {
            return new BusinessException(message);
        }
    }
}