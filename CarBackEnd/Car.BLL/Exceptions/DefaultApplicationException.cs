using System;
using Car.BLL.Dto;

namespace Car.BLL.Exceptions
{
    [Serializable]
    public class DefaultApplicationException : Exception
    {
        public int StatusCode { get; set; }

        public Severity Severity { get; set; }

        public DefaultApplicationException()
        {
        }

        public DefaultApplicationException(string message) 
            : base(message)
        {
        }
    }
}
