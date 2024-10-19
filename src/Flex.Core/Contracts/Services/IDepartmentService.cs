using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;

namespace Flex.Core.Contracts.Services
{
    public interface IDepartmentService
    {
        Task<PageResult<DepartmentDto>> GetPaging(GetDepartmentPagedRequest request);
    }
}
