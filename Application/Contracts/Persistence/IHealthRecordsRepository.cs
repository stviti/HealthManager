using Domain.Entities.HealthRecord;

namespace Application.Contracts.Persistence
{
    public interface IHealthRecordsRepository : IGenericRepository<HealthRecordEntity>
    {
    }
}
