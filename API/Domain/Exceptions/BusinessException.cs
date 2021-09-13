using System;
using System.Runtime.Serialization;

namespace API.Domain.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public BusinessException()
            : this("Error on executing the business logic layer.") { }

        public BusinessException(string message)
            : base(message) { }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}