using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Flex.Core.Domain.Identity;

namespace Flex.Data.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");

            #region Constraints
            //builder.HasOne<User>()
            //   .WithMany()
            //   .HasForeignKey(ut => ut.UserId)
            //   .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}