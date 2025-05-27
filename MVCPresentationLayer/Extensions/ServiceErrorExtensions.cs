using DomainLayer.Errors;
using Microsoft.AspNetCore.Http;

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
