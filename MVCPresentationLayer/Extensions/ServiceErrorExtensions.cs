using DomainLayer.Errors;

namespace MVCPresentationLayer.Extensions
{
    public static class ServiceErrorExtensions
    {
         public static object ToHttpResponse(this ServiceError error)
        {
            return new 
            {
                error.ErrorCode,
                error.Message,
                error.StatusCode,
            };
        }
    }
}
