using Flex.Core.Domain.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flex.Data.Configurations
{
    public class DepartmentConfiguration
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            #region Constraints
            builder.HasKey(x => x.Id);
            #endregion
        }
    }
}
