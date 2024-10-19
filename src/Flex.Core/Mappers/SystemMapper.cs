using AutoMapper;
using Flex.Core.Domain.System;
using Flex.Core.Models.System.Department;

namespace Flex.Core.Mappers
{
    public class SystemMapper : Profile
    {
        public SystemMapper()
        {
            // Department
            CreateMap<Department, DepartmentDto>();
        }
    }
}
