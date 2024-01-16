using Book4H2Ten.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Tokens
{
    public interface ITokenService
    {
        DateTime AccessTokenExpiryTime();
        string GenerateRefreshToken();
        string GenerateToken(User user);
        DateTime RefreshTokenExpiryTime();
        Guid? UserIdFromExpiredToken(string token);
        Guid? ValidateToken(string token);
    }
}
