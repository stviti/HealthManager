using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Constants.Identity;
using Application.Contracts.Identity;
using Application.Models.Configuration;
using Application.Models.Responses;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;

        public TokenService(UserManager<AppUser> userManager, IOptions<JwtConfiguration> jwtOptions)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtOptions.Value;
        }

        public async Task<TokenResponse> GenerateToken(IAppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync((AppUser)user);
            var roles = await _userManager.GetRolesAsync((AppUser)user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.DurationInMinutes),
                signingCredentials: signingCredentials);

            TokenResponse response = new TokenResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
            return response;
        }
    }
}
