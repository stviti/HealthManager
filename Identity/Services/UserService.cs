using System;
using System.Threading.Tasks;
using Application.Contracts.Identity;
using Application.DTOs.Account;
using Application.Exceptions;
using Application.Models.Responses;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<TokenResponse> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new UserNotFoundException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);

            if (result.IsLockedOut)
                throw new BadRequestException("User is locked out.");

            if (result.RequiresTwoFactor)
                throw new BadRequestException("User requires Two-Factor Authentication.");

            if (result.IsNotAllowed)
                throw new BadRequestException("User is not allowed to sign in.");

            if (result.Succeeded == false)
                throw new Exception(result.ToString());

            return await _tokenService.GenerateToken(user);
        }

        public async Task<TokenResponse> Register(RegisterDto dto)
        {
            if (await _userManager.FindByNameAsync(dto.UserName) != null)
                throw new Exception("User name is already in use.");

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                throw new Exception("Email is already in use.");

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.UserName,
                NormalizedEmail = dto.UserName.ToLower(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded == false)
                throw new Exception("Something went wrong.");

            return await _tokenService.GenerateToken(user);
        }
    }
}
