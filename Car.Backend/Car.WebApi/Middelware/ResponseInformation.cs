namespace Car.WebApi.Middelware
{
    public class ResponseInformation
    {
        public string ResponseMessage { get; set; } = string.Empty;

        public string LogMessage { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public string ContentType { get; set; } = string.Empty;
    }
}
