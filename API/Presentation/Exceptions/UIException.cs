using System;
using System.Runtime.Serialization;

namespace API.Presentation.Exceptions
{
    [Serializable]
    public class UIException : Exception
    {
        public UIException()
            : this("Erro on processing the User Interface") { }

        public UIException(string message)
            : base(message) { }

        protected UIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}