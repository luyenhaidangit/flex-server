using Flex.Core.Contracts.Shared;

namespace Flex.Core.Shared.Options
{
    public class Jwt : ISettingOption
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
