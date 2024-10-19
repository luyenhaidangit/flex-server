using Flex.Core.Contracts.Services;
using Flex.Core.Models.Common;
using Flex.Core.Models.System.Department;
using Microsoft.AspNetCore.Mvc;

namespace Flex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService _departmentService {  get; set; }

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> GetPaging([FromQuery] GetDepartmentPagedRequest request)
        {
            var result = await _departmentService.GetPaging(request);

            return Ok(Result.Success("Lấy danh sách thành công!", result));
        }
    }
}
