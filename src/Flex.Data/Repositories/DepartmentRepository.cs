using System.Linq.Dynamic.Core;
using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Domain.System;
using Flex.Core.Extensions;
using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;
using Flex.Data.Infrastructures;

namespace Flex.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department,int>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PageResult<DepartmentDto>> GetPaging(GetDepartmentPagedRequest request)
        {
            var query = _dbContext.Departments.AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(b => b.Name.ToLower().Contains(request.Name));
            }

            // Paging
            var result = await query.ToPageResultAsync<Department, DepartmentDto>(request);

            return result;
        }
    }
}