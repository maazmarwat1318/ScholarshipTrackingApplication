

namespace DomainLayer.Errors
{
    public class ErrorResponse
    {
        public required string ErrorCode { get; set; }
        public string? Message { get; set; }
        public required int StatusCode { get; set; }
    }
}
