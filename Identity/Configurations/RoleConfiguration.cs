using Application.Constants.Identity;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
                new AppRole
                {
                    Id = ConfigurationIds.AdminRoleId,
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin.ToUpper()
                },
                new AppRole
                {
                    Id = ConfigurationIds.UserRoleId,
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            );
        }


    }
}
