using System;
using Car.BLL.Dto;

namespace Car.BLL.Exceptions
{
    public class ApplicationCustomException : Exception
    {
        public int StatusCode { get; set; }

        public Severity Severity { get; set; }

        public ApplicationCustomException(string message)
            : base(message)
        {
        }
    }
}
