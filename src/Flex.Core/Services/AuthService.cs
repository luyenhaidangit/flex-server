using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Flex.Core.Contracts.Services;
using Flex.Core.Domain.Identity;
using Flex.Core.Models.Identity;
using Flex.Core.Shared.Constants.Identity;
using Flex.Core.Shared.Options;

namespace Flex.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Jwt _jwtSettings;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IOptions<Jwt> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            // Validate
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new ArgumentException("Tên người dùng không tồn tại trong hệ thống!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe,true);
            if (!result.Succeeded)
            {
                throw new ArgumentException("Tên tài khoản hoặc mật khẩu không chính xác!");
            }

            // Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimType.UserName, user.UserName),
                new Claim(ClaimType.Email, user.Email),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);

            if (userClaims != null)
            {
                claims.AddRange(userClaims);
            }

            //Create token
            var token = _tokenService.CreateToken(claims);
            var refreshToken = _tokenService.CreateRefreshToken();

            var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);

            return new LoginResponse(token,refreshToken);
        }
    }
}
