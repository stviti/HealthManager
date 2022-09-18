using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Application.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.", HttpStatusCode.BadRequest)
        {
        }

        //public ValidationException(IDictionary<string, string[]> errorsDictionary)
        //    : base(JsonSerializer.Serialize(errorsDictionary), HttpStatusCode.BadRequest)
        //{
        //}

        public ValidationException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
