using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Flex.Core.Domain.Identity;

namespace Flex.Data.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");

            #region Constraints
            //builder.Property(ut => ut.LoginProvider).HasMaxLength(256);

            //builder.HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(ut => ut.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}