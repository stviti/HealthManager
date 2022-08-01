using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.") { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner) { }

        public ValidationException(IDictionary<string, string[]> errorsDictionary)
            : base()
        {
            Errors = errorsDictionary;
            
        }
        public override string Message
        {
            get
            {
                if (Errors.Count > 0)
                {
                    var _message = "";
                    foreach (var err in Errors)
                    {
                        _message += String.Join(" ", err.Value) + " ";
                    }
                    return _message;
                }
                else
                {
                    return base.Message;
                }
            }
        }
        private IDictionary<string, string[]> Errors { get; }

    }
}
