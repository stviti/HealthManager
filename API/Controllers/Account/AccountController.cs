using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Identity;
using Application.DTOs.Account;
using Application.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Account
{
    [Route("api/")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenResponse>> Login(LoginDto model, CancellationToken cancellationToken)
        {
            var response = await _userService.Login(model);
            return Ok(response);
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenResponse>> Register(RegisterDto model, CancellationToken cancellationToken)
        {
            var response = await _userService.Register(model);
            return Ok(response);
        }
    }
}
