using Application.Contracts.Persistence;
using Domain.Entities.Treatment;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class TreatmentsRepository : GenericRepository<TreatmentEntity>, ITreatmentsRepository
    {
        public TreatmentsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
