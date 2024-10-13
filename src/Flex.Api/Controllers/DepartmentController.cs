using AutoMapper;
using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Contracts.Data.Services;
using Flex.Core.Contracts.Services;
using Flex.Core.Domain.Identity;
using Flex.Core.Domain.System;
using Flex.Core.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
