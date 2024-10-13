using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flex.Core.Domain.Identity;
using System.Reflection;
using Flex.Core.Domain.System;

#region Document
/// <summary>
/// Tools > Nuget Package Manager > Package Manager Console > Default Project: Data
/// Add-Migration Example
/// Update-Database
/// Remove-Migration
/// Script-Migration
/// Drop-Database
/// </summary>
#endregion

namespace Flex.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSet
        public virtual DbSet<Department> Departments { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}