
using DomainLayer.Errors;

namespace DomainLayer.Common
{
    public class Response<T>
    {
        public bool IsSuccess { get; private set; }
        public ServiceError? ServiceError { get; private set; }
        public T? Value { get; private set; }

        private Response() { }

        public static Response<T> Success(T value) => new Response<T>
        {
            IsSuccess = true,
            Value = value
        };

        public static Response<T> Failure(ServiceError error) => new Response<T>
        {
            IsSuccess = false,
            ServiceError = error
        };
    }
}
