using System;
using System.Net;

namespace Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found", HttpStatusCode.NotFound) { }

    }
}
