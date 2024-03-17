using Book4H2Ten.Host.BaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Book4H2Ten.Services.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Book4H2Ten.Services.Users;
using Book4H2Ten.Services.Emails.Dtos;


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

        [SwaggerOperation(Summary = "Refresh token")]
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<AuthResponseDto> RefreshToken(RefreshTokenDto requestDto)
            => await _userService.RefreshToken(requestDto);

        [SwaggerOperation(Summary = "Logout")]
        [HttpDelete("log-out")]
        public async Task Logout(RefreshTokenDto requestDto)
            => await _userService.Logout(requestDto);

        [HttpPost("verify-email")]
        public  IActionResult VerifyEmail(VerifyEmailRequestDtos verifyDtos)
        {
            _userService.VerifyEmail(verifyDtos.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }
    }
}
