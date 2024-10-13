using Flex.Core.Contracts.Data.Repositories;
using Flex.Core.Domain.System;
using Flex.Core.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Flex.Core.Services
{
    public class DepartmentService
    {
        private IDepartmentRepository _departmentRepository { get; set; }

        public DepartmentService(IDepartmentRepository departmentRepository) 
        {
            this._departmentRepository = departmentRepository;
        }
    }
}
