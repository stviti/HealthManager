using System;
using System.Net;

namespace Application.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

        public AppException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, Exception inner) : base(message, inner) { }
    }
}
