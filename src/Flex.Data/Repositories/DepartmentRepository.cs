using AutoMapper;
using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Domain.System;
using Flex.Core.Extensions;
using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;
using Flex.Data.Infrastructures;
using System.Linq.Dynamic.Core;

namespace Flex.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department,int>, IDepartmentRepository
    {
        private readonly IMapper _mapper;

        public DepartmentRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
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
            var result = await query.ToPageResultAsync<Department, DepartmentDto>(request,_mapper);

            return result;
        }
    }
}