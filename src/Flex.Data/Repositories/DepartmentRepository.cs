using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Domain.System;
using Flex.Data.Infrastructures;

namespace Flex.Data.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department,int>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}