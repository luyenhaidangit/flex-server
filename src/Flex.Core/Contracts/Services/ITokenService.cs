using System.Security.Claims;

namespace Flex.Core.Contracts.Services
{
    public interface ITokenService
    {
        string CreateToken(IEnumerable<Claim> claims);
        string CreateRefreshToken();
    }
}
