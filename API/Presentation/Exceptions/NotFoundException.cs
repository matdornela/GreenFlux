using System;
using System.Runtime.Serialization;

namespace API.Presentation.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : this("Not Found") { }

        public NotFoundException(string message)
            : base(message) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}