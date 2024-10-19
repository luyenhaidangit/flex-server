using Flex.Core.Shared.Abstracts;

namespace Flex.Core.Models.System.Department
{
    public class DepartmentDto : EntityBase
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public int Status { get; set; }

        public string? Description { get; set; }
    }
}
