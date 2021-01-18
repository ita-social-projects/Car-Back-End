using System;
using System.Runtime.Serialization;
using Car.Domain.Dto;

namespace Car.Domain.Exceptions
{
    [Serializable]
    public class DefaultApplicationException : Exception
    {
        public int StatusCode { get; set; }

        public Severity Severity { get; set; }

        public DefaultApplicationException(string message)
            : base(message)
        {
        }

        public DefaultApplicationException()
        {
        }

        protected DefaultApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Error", StatusCode);
        }
    }
}
