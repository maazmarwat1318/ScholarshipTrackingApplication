using DomainLayer.Errors;

namespace WebAPI.Extensions
{
    public static class ServiceErrorExtensions
    {
         public static ErrorResponse ToHttpResponse(this ServiceError error)
        {
            return new ErrorResponse
            {
                ErrorCode = error.ErrorCode,
                Message = error.Message,
                StatusCode = error.StatusCode,
            };
        }
    }
}
