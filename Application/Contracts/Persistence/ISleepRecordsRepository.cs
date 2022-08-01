using Domain.Entities.SleepRecord;

namespace Application.Contracts.Persistence
{
    public interface ISleepRecordsRepository : IGenericRepository<SleepRecordEntity>
    {
    }
}
