using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasData(
                new AppUserRole
                {
                    RoleId =ConfigurationIds.AdminRoleId,
                    UserId = ConfigurationIds.AdminId
                },
                new AppUserRole
                {
                    RoleId = ConfigurationIds.UserRoleId,
                    UserId = ConfigurationIds.UserId
                }
            );
        }
    }
}
