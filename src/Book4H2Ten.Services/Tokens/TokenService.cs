using Book4H2Ten.Core.Errors;
using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Resources;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<UserToken> _userTokenRepository;

        public TokenService(
            IConfiguration configuration, IRepository<UserToken> userTokenRepository)
        {
            _configuration = configuration;
            _userTokenRepository = userTokenRepository;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GenerateToken(User user)
        {
            var claims = CreateJwtClaims(user);
            var now = DateTime.UtcNow;
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtBearer:Issuer"],
                audience: _configuration["Authentication:JwtBearer:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(int.Parse(_configuration["Authentication:JwtBearer:Expiration"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public DateTime AccessTokenExpiryTime()
        {
            return DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:JwtBearer:Expiration"]));
        }

        public DateTime RefreshTokenExpiryTime()
        {
            return DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:JwtBearer:RefreshTokenExpiration"]));
        }

        public Guid? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var validUserToken = _userTokenRepository.GetQuery(x => x.AccessToken == token).FirstOrDefault();
            if (validUserToken == null)
            {
                throw new BadRequestException("Token wrong!");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]);
            try
            {
                tokenHandler.ValidateToken(validUserToken.AccessToken, new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Authentication:JwtBearer:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = _configuration["Authentication:JwtBearer:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Book4H2Ten:GuidId").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public Guid? UserIdFromExpiredToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Authentication:JwtBearer:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = _configuration["Authentication:JwtBearer:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "Book4H2Ten:GuidId").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        private List<Claim> CreateJwtClaims(User userEntity)
        {
            var claim = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            if (userEntity != null)
            {
                claim.AddRange(new List<Claim>()
                {

                    new Claim("Book4H2Ten:Email", userEntity.Email),
                    new Claim("Book4H2Ten:PhoneNumber", userEntity.PhoneNumber),
                    new Claim("Book4H2Ten:Id", userEntity.Id.ToString()),
                    new Claim("Book4H2Ten:GuidId", userEntity.GuidId.ToString()),
                    new Claim("Book4H2Ten:Gender",userEntity.Gender.ToString())
                });
                string tesst = "123";
            }
            string tesst223 = "123";
            return claim;
        }
    }
}
