using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Errors.AuthenticationErrors;

namespace DomainLayer.Errors
{
    public static class CommonErrors
    {
        public static readonly string ServerErrorCode = "ServerError";
        public static readonly string BadRequestErrorCode = "BadRequestError";

        public static ServiceError ServerError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = ServerErrorCode, StatusCode = 500, Exception = e, Message = message ?? "Server error occured." };
        }

        public static ServiceError BadRequestError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = BadRequestErrorCode, StatusCode = 400, Exception = e, Message = message ?? "Bad Request Error." };
        }

    }
}
