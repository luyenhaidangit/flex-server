using Microsoft.AspNetCore.Identity;

namespace Flex.Core.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}