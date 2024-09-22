using Flex.Core.Domain.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Flex.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            #region Constraints
            //builder.HasOne<User>()
            // .WithMany()
            // .HasForeignKey(ur => ur.UserId)
            // .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne<Role>()
            //    .WithMany()
            //    .HasForeignKey(ur => ur.RoleId)
            //    .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
