using AutoMapper;
using Flex.Core.Domain.Identity;
using Flex.Core.Models.Identity;

namespace Flex.Core.Mappers
{
    public class IdentityMapper : Profile
    {
        public IdentityMapper()
        {
            CreateMap<User, UserInfo>();
        }
    }
}
