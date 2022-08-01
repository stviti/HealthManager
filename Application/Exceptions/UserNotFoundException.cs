using System;
using System.Net;

namespace Application.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException() : base("User not found.", HttpStatusCode.NotFound) { }

    }
}
