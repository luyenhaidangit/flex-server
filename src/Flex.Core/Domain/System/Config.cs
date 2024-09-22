using Flex.Core.Shared.Abstracts;

namespace Flex.Core.Domain.System
{
    public class Config : EntityBase
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }
    }
}
