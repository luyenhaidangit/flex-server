using Flex.Core.Domain.System;
using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;

namespace Flex.Core.Contracts.Data.Repositories
{
    public interface IDepartmentRepository : IRepository<Department,int>
    {
        Task<PageResult<DepartmentDto>> GetPaging(GetDepartmentPagedRequest request);
    }
}
