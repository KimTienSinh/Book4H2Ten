using Book4H2Ten.Host.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Book4H2Ten.Services.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Book4H2Ten.Services.Users;


namespace Book4H2Ten.Host.Controllers
{ 
    public class AuthenticateController : WebBaseController
    {
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [SwaggerOperation(Summary = "Signup")]
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<AuthResponseDto> Signup(SignUpRequestDto signupDto)
        {
            return await _userService.Signup(signupDto, Request.Headers["origin"]);
        }

        [SwaggerOperation(Summary = "Signin")]
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<AuthResponseDto> Signin([FromBody] SigninRequestDto signinDto)
            => await _userService.Signin(signinDto);

        [SwaggerOperation(Summary = "Logout")]
        [HttpDelete("log-out")]
        public async Task Logout(RefreshTokenDto requestDto)
            => await _userService.Logout(requestDto);
    }
}
