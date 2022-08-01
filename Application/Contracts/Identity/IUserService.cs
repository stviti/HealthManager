using System.Threading.Tasks;
using Application.DTOs.Account;
using Application.Models.Responses;

namespace Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<TokenResponse> Login(LoginDto dto);
        Task<TokenResponse> Register(RegisterDto dto);
    }
}
