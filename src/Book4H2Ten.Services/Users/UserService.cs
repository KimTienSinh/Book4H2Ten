using Book4H2Ten.Core.Enums;
using Book4H2Ten.Core.Errors;
using Book4H2Ten.Core.Helpers;
using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Emails;
using Book4H2Ten.Services.Tokens;
using Book4H2Ten.Services.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Users
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IRepository<UserToken> _userTokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;
        public UserService(IRepository<User> repository,
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService,
            IRepository<UserToken> userTokenRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository,
            IOptions<AppSettings> appSettings,
            IEmailService emailService) : base(repository, httpContextAccessor)
        {
            _tokenService = tokenService;
            _userTokenRepository = userTokenRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public User GetUserById(Guid id)
        {
            var user = _repository.GetByIdAsync(id).Result;
            return user;
        }

        public async Task<AuthResponseDto> Signup(SignUpRequestDto signupDto, string origin)
        {
            var isExistPhoneNumberOrEmail = await _repository.GetQuery().AnyAsync(x => x.Email == signupDto.Email || x.PhoneNumber == signupDto.PhoneNumber);
            if (isExistPhoneNumberOrEmail)
            {
                throw new ConflictException("Phone Or Email Existed! Try again");
            }

            if (!signupDto.Password.Equals(signupDto.ConfirmPassword))
            {
                throw new BadRequestException("Wrong Password! Please check again");
            }

            var user = new User
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(signupDto.Password),
                PhoneNumber = signupDto.PhoneNumber,
                RoleName = EnumLibrary.RoleName.USER,
                /*FirstName = signupDto.FirstName,
                LastName = signupDto.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PhoneNumber = signupDto.PhoneNumber,
                */
            };
            user.VerificationToken = _tokenService.GenerateRefreshToken();

            // send email
            sendVerificationEmail(user, origin);

            await _repository.AddAsync(user);

            /*var role = new Role
            {
                RoleName = EnumLibrary.RoleName.USER,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _roleRepository.AddAsync(role);*/

           /* var userRole = new UserRole
            {
                UserId = user.GuidId,
                RoleId = role.GuidId
            };
            await _userRoleRepository.AddAsync(userRole);*/

            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var accessTokenExpiryTime = _tokenService.AccessTokenExpiryTime();
            var refreshTokenExpiryTime = _tokenService.RefreshTokenExpiryTime();
            var userToken = new UserToken
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                AccessTokenExpiredTime = accessTokenExpiryTime,
                RefreshToken = refreshToken,
                RefreshTokenExpiredTime = refreshTokenExpiryTime,
            };
            await _userTokenRepository.AddAsync(userToken);

            return new AuthResponseDto
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthResponseDto> Signin(SigninRequestDto signupDto)
        {
            var user = await _repository.GetQuery(x => x.Email.ToLower() == signupDto.EmailOrPhoneNumber.ToLower() || x.PhoneNumber == signupDto.EmailOrPhoneNumber).FirstOrDefaultAsync();
            if (user == null || !BCrypt.Net.BCrypt.Verify(signupDto.Password, user.Password))
            {
                throw new BadRequestException("Wrong Password or Email or UserName! Please check again");
            }
            if (user.IsVerified == false)
            {
                throw new BadRequestException("Account not verify. Please check verify");
            }

            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var accessTokenExpiryTime = _tokenService.AccessTokenExpiryTime();
            var refreshTokenExpiryTime = _tokenService.RefreshTokenExpiryTime();

            var userToken = new UserToken
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                AccessTokenExpiredTime = accessTokenExpiryTime,
                RefreshToken = refreshToken,
                RefreshTokenExpiredTime = refreshTokenExpiryTime,
            };
            await _userTokenRepository.AddAsync(userToken);

            return new AuthResponseDto
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthResponseDto> RefreshToken(RefreshTokenDto requestDto)
        {
            var userIdInToken = _tokenService.UserIdFromExpiredToken(requestDto.AccessToken);
            if (userIdInToken == null)
            {
                throw new BadRequestException("User Id not found");
            }

            var validUserToken = await _userTokenRepository.GetQuery(x => x.UserId == userIdInToken.Value &&
            x.AccessToken == requestDto.AccessToken &&
            x.RefreshToken == requestDto.RefreshToken &&
            x.RefreshTokenExpiredTime > CurrentDate).FirstOrDefaultAsync();
            if (validUserToken == null)
            {
                throw new BadRequestException("User Id not found or wrong token");
            }
            await _userTokenRepository.DeleteAsync(validUserToken);

            var user = await _repository.GetByIdAsync(userIdInToken.Value);

            var accessToken = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var accessTokenExpiryTime = _tokenService.AccessTokenExpiryTime();
            var refreshTokenExpiryTime = _tokenService.RefreshTokenExpiryTime();

            var userToken = new UserToken
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                AccessTokenExpiredTime = accessTokenExpiryTime,
                RefreshToken = refreshToken,
                RefreshTokenExpiredTime = refreshTokenExpiryTime,
            };
            await _userTokenRepository.AddAsync(userToken);

            return new AuthResponseDto
            {
                UserId = user.GuidId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task Logout(RefreshTokenDto requestDto)
        {
            var validUserToken = await _userTokenRepository.GetQuery(x => x.AccessToken == requestDto.AccessToken && x.RefreshToken == requestDto.RefreshToken).FirstOrDefaultAsync();
            if (validUserToken == null)
            {
                throw new BadRequestException("Not Found UserToken");
            }

            await _userTokenRepository.DeleteAsync(validUserToken);
        }

        /*public void ForgotPassword(ForgotPasswordRequestDtos model, string origin)
        {
            var account = _userTokenRepository.GetQuery(x => x.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            // create reset token that expires after 1 day
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            _context.Accounts.Update(account);
            _context.SaveChanges();

            // send email
            sendPasswordResetEmail(account, origin);
        }*/

        private void sendVerificationEmail(User account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        public void VerifyEmail(string token)
        {
            var account = _repository.GetQuery(x => x.VerificationToken == token).FirstOrDefault();

            if (account == null) throw new VerifyException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;
            account.IsVerified = true;
            _repository.UpdateAsync(account);
        }
    }
}
