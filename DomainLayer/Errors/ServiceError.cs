

namespace DomainLayer.Errors
{
    public class ServiceError
    {
        public required string ErrorCode { get; set; }
        public string? Message { get; set; }
        public required int StatusCode { get; set; }

        public Exception? Exception { get; set; }


        public object ToHttpResponse()
        {
            return new
            {
                ErrorCode,
                Message,
                StatusCode,
            };
        }
    }
}
