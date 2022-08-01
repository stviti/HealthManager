using Application.Contracts.Persistence;
using Domain.Entities.HealthRecord;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class HealthRecordsRepository : GenericRepository<HealthRecordEntity>, IHealthRecordsRepository
    {
        public HealthRecordsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}
