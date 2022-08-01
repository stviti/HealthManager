using Application.Contracts.Persistence;
using Domain.Entities.DoctorVisit;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class DoctorVisitsRepository : GenericRepository<DoctorVisitEntity>, IDoctorVisitsRepository
    {
        public DoctorVisitsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
