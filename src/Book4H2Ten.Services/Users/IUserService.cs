using Book4H2Ten.Services.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book4H2Ten.Services.Users.Dtos;
using Book4H2Ten.Entities;

namespace Book4H2Ten.Services.Users
{
    public interface IUserService
    {
        //User GetUserById(Guid id);
        //Task<List<TagUserDto>> TagUserAsync(string? searchKey);
        Task Logout(RefreshTokenDto requestDto);
        //Task<MyProfileResponseDto> MyProfile();
        //Task<AuthResponseDto> RefreshToken(RefreshTokenDto requestDto);
        Task<AuthResponseDto> Signin(SigninRequestDto signupDto);
        Task<AuthResponseDto> Signup(SignUpRequestDto signupDto);

    }
}
