using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Flex.Core.Domain.Identity;
using Flex.Core.Shared.Options;
using System.Security.Cryptography;
using Flex.Core.Contracts.Services;

namespace Flex.Core.Services
{
    #region Document
    /// <summary>
    /// SecurityAlgorithms.HmacSha256
    /// </summary>
    #endregion

    public class TokenService : ITokenService
    {
        private readonly Jwt _jwtSettings;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(IOptions<Jwt> jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSettings = jwtSettings.Value;
            _tokenHandler = jwtSecurityTokenHandler;
        }

        #region Rule
        private void ValidateRuleToken(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("Tên người dùng không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Email không được để trống.");
            }
        }
        #endregion

        #region Main
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256
            );
            var expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenValidityInSeconds);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: expires,
                claims: claims,
                signingCredentials: creds
            );

            var tokenString = _tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        #endregion
    }
}
