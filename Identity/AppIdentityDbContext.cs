using System;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasQueryFilter(a => a.IsDeleted == false);

            builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);

            base.OnModelCreating(builder);
        }

    }
}
