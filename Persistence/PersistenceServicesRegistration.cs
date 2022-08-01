using System;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppContent");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IDoctorVisitsRepository, DoctorVisitsRepository>();
            services.AddScoped<IHealthRecordsRepository, HealthRecordsRepository>();
            services.AddScoped<ISleepRecordsRepository, SleepRecordsRepository>();
            services.AddScoped<ITreatmentsRepository, TreatmentsRepository>();

            return services;
        }
    }
}
