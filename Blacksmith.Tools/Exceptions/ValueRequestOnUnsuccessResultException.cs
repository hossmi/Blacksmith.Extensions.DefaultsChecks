using Blacksmith.Validations.Exceptions;
using System.Runtime.Serialization;

namespace Blacksmith.Tools
{
    public class ValueRequestOnUnsuccessResultException : DomainException
    {
        public ValueRequestOnUnsuccessResultException() : base()
        {
        }

        protected ValueRequestOnUnsuccessResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}