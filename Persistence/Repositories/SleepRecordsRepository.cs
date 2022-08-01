using Application.Contracts.Persistence;
using Domain.Entities.SleepRecord;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class SleepRecordsRepository : GenericRepository<SleepRecordEntity>, ISleepRecordsRepository
    {
        public SleepRecordsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
