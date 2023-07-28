using GeekWorld.Application.Contracts.Dtos.Requests.Users;
using GeekWorld.Application.Contracts.Dtos.Responses.Users;
using GeekWorld.Application.Contracts.Services;
using GeekWorld.Application.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAuthorize.WebAPI.Security;

namespace GeekWorld.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(IAuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponse>> SignUp([FromBody] CreateUserRequest request)
        {
            UserResponse response = await _authService.SignUp(request);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginUserRequest request)
        {
            UserResponse response = await _authService.Login(request);

            if (!response.IsValid())
            {
                return BadRequest(new Errors(response.Notifications));
            }
            var token = _tokenService.GenerateToken(response);

            return Ok(new
            {
                response,
                token
            });
        }
    }
}
