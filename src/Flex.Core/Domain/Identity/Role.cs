using Microsoft.AspNetCore.Identity;

namespace Flex.Core.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}
