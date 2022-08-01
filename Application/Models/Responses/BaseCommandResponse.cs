using System;
using System.Collections.Generic;

namespace Application.Models.Responses
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
    }
}
