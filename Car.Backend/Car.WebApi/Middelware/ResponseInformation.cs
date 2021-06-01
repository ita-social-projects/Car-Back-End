namespace Car.WebApi.Middelware
{
    public class ResponseInformation
    {
        public string ResponseMessage { get; set; }

        public string LogMessage { get; set; }

        public int StatusCode { get; set; }

        public string ContentType { get; set; }
    }
}
