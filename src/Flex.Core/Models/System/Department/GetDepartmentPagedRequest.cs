using Flex.Core.Models.Common;

namespace Flex.Core.Models.System.Department
{
    public class GetDepartmentPagedRequest : PagedRequest
    {
        public string? Name { get; set; }
    }
}
