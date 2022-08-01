using System;
using System.Threading.Tasks;
using Application.Models.Responses;

namespace Application.Contracts.Identity
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateToken(IAppUser user);
    }
}
