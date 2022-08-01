using Application.Constants.Identity;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get => _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid).Value;
        }
    }
}
