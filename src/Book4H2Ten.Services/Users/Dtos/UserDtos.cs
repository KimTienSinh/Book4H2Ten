using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Book4H2Ten.Core.Enums.EnumLibrary;

namespace Book4H2Ten.Services.Users.Dtos
{
    public class SignUpRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirtDate { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public UserGender Gender { get; set; }
    }
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class SigninRequestDto
    {
        public string EmailOrPhoneNumber { get; set; }

        public string Password { get; set; }
    }

    public class RefreshTokenDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
