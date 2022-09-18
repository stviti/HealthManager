using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class AuditableDbContext : DbContext
    {

        private readonly ICurrentUser _currentUser;

        public AuditableDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BasePrivateEntity>()
                .HasQueryFilter(a => a.CreatedBy == _currentUser.UserId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in base.ChangeTracker.Entries<BasePrivateEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = _currentUser.UserId;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _currentUser.UserId;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
