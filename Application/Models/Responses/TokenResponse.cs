using System;
namespace Application.Models.Responses
{
    public class TokenResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
