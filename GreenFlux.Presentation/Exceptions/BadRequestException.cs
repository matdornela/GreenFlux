using System;
using System.Runtime.Serialization;

namespace GreenFlux.Presentation.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
            : this("Bad Request") { }

        public BadRequestException(string message)
            : base(message) { }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}