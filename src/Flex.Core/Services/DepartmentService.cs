using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Contracts.Services;
using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;

namespace Flex.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository { get; set; }

        public DepartmentService(IDepartmentRepository departmentRepository) 
        {
            this._departmentRepository = departmentRepository;
        }

        public async Task<PageResult<DepartmentDto>> GetPaging(GetDepartmentPagedRequest request)
        {
            var result = await _departmentRepository.GetPaging(request);

            return result;
        }
    }
}
