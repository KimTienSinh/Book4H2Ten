using Book4H2Ten.Core.Enums;
using Book4H2Ten.Core.Errors;
using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Tokens;
using Book4H2Ten.Services.Users.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
        public UserService(IRepository<User> repository,
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService,
            IRepository<UserToken> userTokenRepository,
            IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository) : base(repository, httpContextAccessor)
        {
            _tokenService = tokenService;
            _userTokenRepository = userTokenRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public User GetUserById(Guid id)
        {
            var user = _repository.GetByIdAsync(id).Result;
            return user;
        }

        public async Task<AuthResponseDto> Signup(SignUpRequestDto signupDto)
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
                /*FirstName = signupDto.FirstName,
                LastName = signupDto.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PhoneNumber = signupDto.PhoneNumber,
                Gender = signupDto.Gender*/
            };
            await _repository.AddAsync(user);

            var role = new Role
            {
                RoleName = EnumLibrary.RoleName.USER,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _roleRepository.AddAsync(role);

            var userRole = new UserRole
            {
                UserId = user.GuidId,
                RoleId = role.GuidId
            };
            await _userRoleRepository.AddAsync(userRole);

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
    }
}
