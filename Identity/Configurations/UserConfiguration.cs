using System;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            builder.HasData(
                    new AppUser
                    {
                        Id = ConfigurationIds.AdminId,
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        FirstName = "Admin",
                        LastName = "Administrator",
                        UserName = "admin@localhost.com",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Password@123"),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    },
                    new AppUser
                    {
                        Id = ConfigurationIds.UserId,
                        Email = "user@localhost.com",
                        NormalizedEmail = "USER@LOCALHOST.COM",
                        FirstName = "System",
                        LastName = "User",
                        UserName = "user@localhost.com",
                        NormalizedUserName = "USER@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Password@123"),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    }
                );


        }
    }
}
